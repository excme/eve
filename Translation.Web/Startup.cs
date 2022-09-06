using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using eveDirect.Translation.DbContext;
using eveDirect.Shared.WebHost;

namespace eveDirect.Translation.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration; 
            Environment = environment;
        }

        IConfiguration Configuration { get; }
        IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration["ConnectionStrings:TranDb"]));

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddMvc();

            // Подключение к брокеру сообщений
            services.ConnectToRabbitMQ(Configuration, Program.AppName, Environment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseBrowserLink();

            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseElasticApm(Configuration,
            //    new HttpDiagnosticsSubscriber(),  /* Enable tracing of outgoing HTTP requests */
            //    new EfCoreDiagnosticsSubscriber()); /* Enable tracing of database calls through EF Core*/
        }
    }
}
