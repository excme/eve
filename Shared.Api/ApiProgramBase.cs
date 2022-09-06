//using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace eveDirect.Shared.Api
{
    //public class ApiProgramBase
    //{
    //    public static IHost CreateApiHost<TStartUp>(string appName, string[] args)
    //        where TStartUp : class
    //    {
    //        var configuration = ProgramGeneric.GetConfigurationAndLogger(appName, args);

    //        try
    //        {
    //            Log.Information($"Конфигурировнаие web-хоста ({appName})");
    //            IHost host = CreateApiHostBuilder<TStartUp>(configuration, args);
    //            Log.Information($"Конфигурировнаие web-хоста завершено");

    //            return host;
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.Fatal(ex, "Программа неожиданно прервана ({ApplicationContext})!", appName);
    //            return null;
    //        }
    //        finally
    //        {
    //            //Log.CloseAndFlush();
    //        }
    //    }

    //    private static IHost CreateApiHostBuilder<TStartUp>(IConfiguration configuration, string[] args) where TStartUp : class =>
    //        Host.CreateDefaultBuilder(args)
    //            //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    //            .ConfigureHostConfiguration(configHost => {
    //                configHost.AddConfiguration(configuration);
    //            })
    //            //.CaptureStartupErrors(false)
    //            .ConfigureWebHostDefaults(webBuilder =>
    //            {
    //                webBuilder
    //                // Для активации GRPC. Полностью переписывает работу с портами микросервиса. После раскомментирования обязателно првоерь вручную работу микросервиса
    //                //.ConfigureKestrel(options =>
    //                //{
    //                //    var ports = GetDefinedPorts(configuration);
    //                //    options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
    //                //    {
    //                //        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    //                //    });
    //                //    options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
    //                //    {
    //                //        listenOptions.Protocols = HttpProtocols.Http2;
    //                //    });
    //                //})
    //                .UseStartup<TStartUp>();
    //            })
    //            //.UseApplicationInsights()
    //            //.UseContentRoot(Directory.GetCurrentDirectory())
    //            //.UseWebRoot("Pics")
    //            .UseSerilog()
    //            .Build();

    //    private static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
    //    {
    //        var grpcPort = config.GetValue("GRPC_PORT", 81);
    //        var port = config.GetValue("PORT", 80);
    //        return (port, grpcPort);
    //    }
    //}
}
