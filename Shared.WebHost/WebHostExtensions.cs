using eveDirect.Shared.ConfigurationHelper;
using eveDirect.Shared.EventBus;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Shared.EventBus.EventBusRabbitMQ;
using eveDirect.Shared.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Linq;

namespace eveDirect.Shared.WebHost
{
    /// <summary>
    /// Класс, который првоеряет и выполняет миграции баз данных в контейнерах
    /// </summary>
    public static class CustomWebHostExtensions
    {
        //public static bool IsInKubernetes(this IHost webHost)
        //{
        //    IConfiguration cfg = webHost.Services.GetService<IConfiguration>();
        //    return IsInKubernetes(cfg);
        //}

        //public static bool IsInKubernetes(IConfiguration cfg)
        //{
        //    var orchestratorType = cfg.GetValue<string>("OrchestratorType");
        //    return orchestratorType?.ToUpper() == "K8S";
        //}

        public static IHost MigrateDbContext<TContext>(this IHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            //var underK8s = webHost.IsInKubernetes();
            //var dockerEnvValue = Environment.GetEnvironmentVariable("OrchestratorType");
            //var underK8s = !string.IsNullOrEmpty(dockerEnvValue) || dockerEnvValue == "K8S" ? true : false;

            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                ILogger<TContext> logger = services.GetRequiredService<ILogger<TContext>>();

                var contextOptions = services.GetService<DbContextOptions<TContext>>();
                var context = (TContext)Activator.CreateInstance(typeof(TContext), new object[] { contextOptions });
                // Увеличение таймаута для миграции огромных таблиц
                var cur_tm = context.Database.GetCommandTimeout();
                context.Database.SetCommandTimeout(TimeSpan.FromMinutes(30));

                try
                {
                    logger.LogInformation($"Миграция бд с контекстом {typeof(TContext).Name}");

                    //if (underK8s)
                    //{
                        InvokeSeeder(seeder, context, services, logger);
                        logger.LogInformation($"Завершение миграции бд с контекстом {typeof(TContext).Name}");
                    //}
                    //else
                    //{
                        // Подхватывание исключений из базы данных
                        // DbUpdateException - исключение из EntityFramework.Exceptions
                        // В зависимости от движка, у этой библиотеки свой провайдер
                        //var retry = Policy.Handle<DbUpdateException>()
                        //     .WaitAndRetry(new TimeSpan[]
                        //     {
                        //     TimeSpan.FromSeconds(3),
                        //     TimeSpan.FromSeconds(5),
                        //     TimeSpan.FromSeconds(8),
                        //     });

                        // если контейнер sql-сервера не создан на запущенном docker compose
                        // эта миграция завершится исключением "ошибка сети". Настройки повторения для DbContext применяются только
                        // для временных исключений
                        // Учитывай что это НЕ применяется когда запущены некоторые оркестраторы. (Позволь оркестратору пересоздать упавший сервис)
                        //retry.Execute(() => InvokeSeeder(seeder, context, services, logger));
                    //}
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Произошла ошибка при миграции бд с контекстом {typeof(TContext).Name}");
                    //if (underK8s)
                    //{
                        throw;          // Rethrow under k8s because we rely on k8s to re-run the pod
                    //}
                }
            }

            return webHost;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services, ILogger<TContext> logger)
            where TContext : DbContext
        {
            var pendingMigrations = context.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                logger.LogInformation($"Есть ({pendingMigrations.Count()}) миграции необходимые для применения.");
                context.Database.Migrate();
            }

            seeder(context, services);
        }

        public static void ConnectToRabbitMQ(this IServiceCollection services, IConfiguration configuration, string subscriptionClientName, IWebHostEnvironment environment)
        {
            //services.AddDbOptions<RabbitMQDatabaseDb>(configuration["ConnectionStrings:RabbitMqDb-Test"]);
            //services.AddSingleton<IRabbitMQRepo, RabbitMQRepo>();

            var retryCount = 5;
            if (!string.IsNullOrEmpty(configuration["EventBus:RetryCount"]))
            {
                retryCount = int.Parse(configuration["EventBus:RetryCount"]);
            }

            var brokerName = "evedirect";
            var suffix = environment.IsDevelopment() ? "dev" : "prod";
            if (suffix.Length > 0)
                brokerName = $"{brokerName}_{suffix}";

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBus:Address"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBus:Port"]))
                {
                    factory.Port = configuration["EventBus:Port"].ToInt32();
                }

                if (!string.IsNullOrEmpty(configuration["EventBus:UserName"]))
                {
                    factory.UserName = configuration["EventBus:UserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBus:Password"]))
                {
                    factory.Password = configuration["EventBus:Password"];
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            if (subscriptionClientName == null)
                throw new ArgumentNullException(nameof(subscriptionClientName));

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubcriptionsManager, brokerName, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        }

        /// <summary>
        /// Подписка на событие
        /// </summary>
        //public static void ConfigureEventBus<TEvent, THandler>(IApplicationBuilder app)
        //    where TEvent : IntegrationEvent
        //    where THandler : IIntegrationEventHandler<TEvent>
        //{
        //    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        //    eventBus.Subscribe<TEvent, THandler>();
        //}
        /// <summary>
        /// Подписка на событие
        /// </summary>
        //public static void ConfigureEventBus<TEvent, THandler>(IApplicationBuilder app)
        //    where TEvent : IntegrationEvent
        //    where THandler : IIntegrationEventHandler<TEvent>
        //{
        //    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        //    eventBus.Subscribe(typeof(TEvent), typeof(THandler));
        //}
        public static IHealthChecksBuilder AddSelfHealthCheck(this IServiceCollection services)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            return hcBuilder;
        }
        public static void AddDbOptions<TContext>(this IServiceCollection services, string connString)
            where TContext : DbContext
        {
            if(connString != null)
                services.AddSingleton(new DbContextOptionsBuilder<TContext>()
                    .UseNpgsql(connString, npgsqlOptionsAction: sqlOptions =>
                    {
                        //sqlOptions.MigrationsAssembly(typeof(TStartUp).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(20), errorCodesToAdd: null);
                    }).Options);
        }
        //public static void ConnectToRabbitMQ(this IServiceCollection services, string assemblyName, IConfiguration configuration)
        //{
            //services.AddRabbitMQEventBus(() => $"amqp://{configuration["EventBus:UserName"]}:{configuration["EventBus:PassWord"]}@{configuration["EventBus:Connection"]}:5672", eventBusOptionAction: eventBusOption =>
            //{
            //    //eventBusOption.ClientProvidedAssembly(assemblyName);
            //    //eventBusOption.EnableRetryOnFailure(true, configuration["EventBus:RetryCount"].ToInt32(), TimeSpan.FromSeconds(30));
            //    //eventBusOption.RetryOnFailure(TimeSpan.FromMilliseconds(100));
            //    //eventBusOption.MessageTTL(2000);
            //    //eventBusOption.DeadLetterExchangeConfig(config =>
            //    //{
            //    //    config.Enabled = true;
            //    //    config.ExchangeNameSuffix = "-prod";
            //    //});
            //    eventBusOption.ClientProvidedAssembly(assemblyName);
            //    eventBusOption.EnableRetryOnFailure(true, 5000, TimeSpan.FromSeconds(30));
            //    eventBusOption.RetryOnFailure(TimeSpan.FromSeconds(1));
            //});
        //}
    }
    public static class BaseHostStartup{
        
        public static void AddCustomDbContext<DbClass, TStartup>(this IServiceCollection services, string connectionStr)
            where DbClass : DbContext
            where TStartup : class
        {
            //services.AddEntityFrameworkNpgsql().AddDbContext<DbClass>(
            //    optionsAction => {
            //        optionsAction.UseNpgsql(connectionStr, npgsqlOptionsAction =>
            //        {
            //            npgsqlOptionsAction.MigrationsAssembly(typeof(TStartup).GetTypeInfo().Assembly.GetName().Name);
            //            npgsqlOptionsAction.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            //        });
            //    }//, ServiceLifetime.Transient  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            //);

            services.AddDbContext<DbClass>(options =>
            {
                options = ContextStatic.DbContextOptions(connectionStr);
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Transient);

        }
    }
}
