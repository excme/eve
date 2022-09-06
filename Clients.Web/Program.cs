using eveDirect.Databases;
using eveDirect.Databases.Contexts;
using eveDirect.Shared.WebHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eveDirect.Clients.Web
{
    public class Program
    {
        public static string AppName = "Web";
        public static void Main(string[] args)
        {
            var hostBuilder = ProgramGeneric.CreateWebHost<Startup>(AppName, args);
            IHost host = hostBuilder.Build();
            IWebHostEnvironment env = host.Services.GetRequiredService<IWebHostEnvironment>();

            if (env.IsProduction())
            {
                host.DbMigration<PublicContext>();
            }

            // Запуск приложения
            host.RunHost();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
