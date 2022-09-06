using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using Winton.Extensions.Configuration.Consul;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using eveDirect.Shared.ConfigurationHelper;

namespace eveDirect.Shared.WebHost
{
    /// <summary>
    /// Базовый класс для всех class.Program
    /// </summary>
    public static class ProgramGeneric
    {
        public static string AppName { get; set; }
        static IHostEnvironment environment;
        public static IHostBuilder CreateWebHost<TStartUp>(string appName, string[] args)
            where TStartUp : class
        {
            AppName = appName;
            var configuration = GetConfigurationAndLogger();

            try
            {
                Log.Information($"Конфигурировнаие web-хоста ({AppName})");
                var host = BuildWebHost<TStartUp>(configuration, args);
                Log.Information($"Конфигурировнаие web-хоста завершено");

                return host;
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Программа неожиданно прервана ({ApplicationContext})!", AppName);

                return null;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IConfiguration GetConfigurationAndLogger()
        {
            // Получение конфигурации
            var configuration = GetConfiguration();

            // Добавление имени сервиса для apm
            configuration.GetSection("ElasticApm").GetSection("ServiceName").Value = $"{AppName} {Environment.MachineName}";

            // Запуск логгера
            Log.Logger = CreateSerilogLogger(configuration);

            return configuration;
        }
        public static void DbMigration<TDbContext>(this IHost host)
            where TDbContext : DbContext
        {
            Log.Information($"Миграции баз данных {nameof(TDbContext)}");
            host.MigrateDbContext<TDbContext>((context, services) =>{ });
        }
        public static void RunHost(this IHost host)
        {
            try
            {
                Log.Information($"Запуск {AppName}");
                host.Run();
                Log.Information("Запуск завершен");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Программа неожиданно прервана {AppName} !!!");
            }
        }
        private static IHostBuilder BuildWebHost<TStartUp>(IConfiguration configuration, string[] args)
            where TStartUp : class
        {
            return Host.CreateDefaultBuilder(args)
                //.CaptureStartupErrors(false)
                .ConfigureHostConfiguration(configHost => {
                    configHost.AddConfiguration(configuration);
                })
                .ConfigureAppConfiguration((hostingContext, config) => {
                    environment = hostingContext.HostingEnvironment;
                })
                .ConfigureServices(services => {
                    if (environment.IsProduction())
                    {
                        var redisConn = ConnectionMultiplexer.Connect($"{configuration["Redis:Address"]},allowAdmin=true,abortConnect=false,password={configuration["Redis:Password"]}");
                        services.AddDataProtection().PersistKeysToStackExchangeRedis(redisConn, "DataProtection-Keys")
                        .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
                        {
                            EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                            ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                        });
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartUp>();
                })
                //.UseSerilog(CreateSerilogLogger(configuration))
                .UseSerilog(Log.Logger)
                ;
        }

        private static ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            //Serilog.Debugging.SelfLog.Enable(Console.Error);

            var environment = configuration["ASPNETCORE_ENVIRONMENT"];
            var elasticSearchServerUrl = configuration["Elastic:Search"];

            var cfg = new LoggerConfiguration()
                // Записываем все info в проекте и все предупреждения и ошибки в движке
                .MinimumLevel.Warning()
                .MinimumLevel.Override("eveDirect", Serilog.Events.LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information)

                .Enrich.WithProperty("Env", environment)
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()

                .WriteTo.Console(theme: SystemConsoleTheme.Literate)
                .WriteTo.Debug();

            if (!string.IsNullOrWhiteSpace(elasticSearchServerUrl))
            {
                // Настройки ElasticSearch
                var config = new ElasticsearchSinkOptions(new Uri($"{elasticSearchServerUrl}"))
                {
                    AutoRegisterTemplate = true,
                    //IndexFormat = $"{AppName}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                    IndexFormat = $"eveDirect-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                    OverwriteTemplate = true,
                    DetectElasticsearchVersion = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    NumberOfReplicas = 1,
                    NumberOfShards = 2,
                    RegisterTemplateFailure = RegisterTemplateRecovery.FailSink,
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback,
                    //FailureCallback = e => { Console.WriteLine("Unable to submit event " + e.MessageTemplate); },
                };

                // Авторизация ElasticSearch
                if (!string.IsNullOrWhiteSpace(configuration["Elastic:SearchUserName"]))
                   config.ModifyConnectionSettings = x => x.BasicAuthentication(configuration["Elastic:SearchUserName"], configuration["Elastic:SearchPassword"]);

                cfg = cfg.WriteTo.Elasticsearch(config);
            }

            return cfg.CreateLogger();
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder();

            // Consul
            builder.AddConsul("eveDirect",
                options =>
                    options.ConsulConfigurationOptions = cco =>
                        cco.Address = new Uri(ConfigurationStatic.ConsulAddress)
            );

            builder.AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
