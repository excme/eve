using System;
using eveDirect.Identity.API.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Identity.API.Certificate;
using eveDirect.ConfigurationHelper;
using eveDirect.Databases.PublicCommon.Models;

namespace eveDirect.Services.Api.Identity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionString"];

            // Application Insignts
            //CustomWebHostExtensions.RegisterAppInsights(services, Configuration);

            // Подключение к БД
            //services.AddCustomDbContext<PublicCommonContext, Startup>(Configuration["ConnectionStrings:IdentityDb"]);
            
            //services.AddIdentity<Account, AccountRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            //services.Configure<AppSettings>(Configuration);

            // Хранение ключей
            //https://docs.microsoft.com/ru-ru/aspnet/core/security/data-protection/implementation/key-storage-providers?view=aspnetcore-3.1&tabs=visual-studio#redis
            if (Configuration.GetValue<string>("IsClusterEnv") == bool.TrueString)
            {
                services.AddDataProtection(opts =>
                {
                    opts.ApplicationDiscriminator = "eveDirect.identity";
                });
                //.PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(Configuration["DPConnectionString"]), "DataProtection-Keys");
            }

            // Health Checks
            //services.AddHealthChecks()
            //    .AddCheck("self", () => HealthCheckResult.Healthy())
            //    .AddNpgSql(connectionString,
            //        name: "IdentityDB-check",
            //        tags: new string[] { "IdentityDB" });

            services.AddTransient<ILoginService<Account>, EFLoginService>();
            services.AddTransient<IRedirectService, RedirectService>();

            // IdentityServer
            services.AddIdentityServer(x =>
            {
                x.IssuerUri = "null";
                x.Authentication.CookieLifetime = TimeSpan.FromHours(2);
            })
            .AddSigningCredential(Certificate.Get())
            .AddAspNetIdentity<Account>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder => builder = ContextStatic.DbContextOptions(connectionString);
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder => builder = ContextStatic.DbContextOptions(connectionString);
            })
            .Services.AddTransient<IProfileService, ProfileService>();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers();
            //services.AddControllersWithViews();
            //services.AddRazorPages();

            //var container = new ContainerBuilder();
            //container.Populate(services);

            //return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            app.UseStaticFiles();

            // Make work identity server redirections in Edge and lastest versions of browers. WARN: Not valid in a production environment.
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "script-src 'unsafe-inline'");
                await next();
            });

            app.UseForwardedHeaders();
            // Adds IdentityServer
            app.UseIdentityServer();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                //endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                //{
                //    Predicate = _ => true,
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});
                //endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                //{
                //    Predicate = r => r.Name.Contains("self")
                //});
            });
        }
    }
}
