namespace eveDirect.Translation.Web
{
    using eveDirect.Shared.WebHost;
    using eveDirect.Translation.DbContext;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static string AppName = "TranslationApp";
        public static void Main(string[] args)
        {
            var hostBuilder = ProgramGeneric.CreateWebHost<Startup>(AppName, args);
            var host = hostBuilder.Build();

            IWebHostEnvironment env = host.Services.GetRequiredService<IWebHostEnvironment>();

            if (!env.IsDevelopment())
            {
                host.DbMigration<ApplicationDbContext>();
            }

            // Запуск приложения
            host.RunHost();
        }
    }
}
