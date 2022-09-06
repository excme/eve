using eveDirect.Databases.Contexts;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Shared.Api;
using eveDirect.Shared.WebHost;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eveDirect.Api.Private
{
    public class Startup : ApiStartupBase
    {
        public Startup(IConfiguration configuration) : base(configuration, Program.AppName) { }
        public void ConfigureServices(IServiceCollection services)
        {
            // ��������� ������ ���
            services.AddDbOptions<PublicContext>(configuration["ConnectionStrings:PublicDb"]);
            // �������
            //services.AddDbOptions<ApplicationDbContext>(configuration["ConnectionStrings:TranDb"]);

            // ����������� ������������
            services.AddSingleton(typeof(IReadOnly), typeof(ReadOnly));

            ApiConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ApiConfigure(app, env);
        }
    }
}
