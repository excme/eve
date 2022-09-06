using eveDirect.Databases.Contexts;
using eveDirect.Shared.WebHost;
using eveDirect.Translation.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eveDirect.Api.Public
{
    public class Program 
    {
        public static string AppName = "ApiCommon";

        public static void Main(string[] args)
        {
            var hostBuilder = ProgramGeneric.CreateWebHost<Startup>(AppName, args);
            var host = hostBuilder.Build();
            IWebHostEnvironment env = host.Services.GetRequiredService<IWebHostEnvironment>();

            if (env.IsProduction())
            {
                host.DbMigration<PublicContext>();
                host.DbMigration<ApplicationDbContext>();
            }

            // Запуск приложения
            host.RunHost();
        }
    }
}
