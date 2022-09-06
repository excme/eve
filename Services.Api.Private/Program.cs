using eveDirect.Databases.Contexts;
using eveDirect.Shared.WebHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eveDirect.Api.Private
{
    public class Program 
    {
        public static string AppName = "ApiCommonPrivate";
        public static void Main(string[] args)
        {
            var hostBuilder = ProgramGeneric.CreateWebHost<Startup>(AppName, args);
            var host = hostBuilder.Build();

            IWebHostEnvironment env = host.Services.GetRequiredService<IWebHostEnvironment>();

            if (!env.IsDevelopment())
            {
                host.DbMigration<PublicContext>();
            }

            // ������ ����������
            host.RunHost();
        }
    }
}
