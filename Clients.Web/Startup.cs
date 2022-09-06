using System.IO;
using eveDirect.Clients.Web.Hubs;
using eveDirect.Clients.Web.IntegrationEvents.LastActions;
using eveDirect.Clients.Web.Services;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Shared.WebHost;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace eveDirect.Clients.Web
{
    public class Startup
    {
        IConfiguration configuration { get; }
        IWebHostEnvironment environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connString = environment.IsProduction()
                ? configuration["ConnectionStrings:PublicDb"]
                //: configuration["ConnectionStrings:PublicDb-Test"];
                : configuration["ConnectionStrings:PublicDb"];

            // MVC
            IMvcBuilder mvcBuilder = services
                .AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            if (environment.IsDevelopment())
                mvcBuilder.AddRazorRuntimeCompilation();

            // Подключения к БД
            services.AddDbOptions<PublicContext>(connString);

            // Сервис проверки существования id
            services.AddSingleton<ICheckExistService, CheckExistService>();

            // Загрузка кэшированных ид в Production
            services.AddHostedService<StartupService>();

            // RabbitMQ
            services.ConnectToRabbitMQ(configuration, Program.AppName, environment);
            // RabbitMQ. Прослушиватели сообщений. Last Actions
            services.AddTransient<AllianceNew_Handler>();
            services.AddTransient<CharacterMigration_Handler>();
            services.AddTransient<CharacterNewborn_Handler>();
            services.AddTransient<CorpMigration_Handler>();
            services.AddTransient<CorpNew_Handler>();
            //services.AddTransient<KillmailNew_Handler>();
            services.AddTransient<MarketContract_Handler>();
            services.AddTransient<MarketOrderNew_Handler>();
            //services.AddTransient<WarNew_Handler>();

            // SignalR
            services.AddSignalR(hubOptions =>
            {
                if (environment.IsDevelopment())
                    hubOptions.EnableDetailedErrors = true;

                hubOptions.KeepAliveInterval = System.TimeSpan.FromMinutes(1);
            });
            services.AddSingleton<ILastActionsService, LastActionsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventBus eventBus)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                // Использование node_modules
                //app.UseStaticFiles();
                app.UseFileServer(new FileServerOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "typescripts")
                ),
                    RequestPath = "/typescripts",
                    EnableDirectoryBrowsing = false
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // SignalR
                endpoints.MapHub<LastActionsHub>("/la");
            });

            // RabbitMQ. Last Actions
            // Запрос добавления
            eventBus.Subscribe<AllianceAfterUpdatedNameIntegrationEvent, AllianceNew_Handler>();
            eventBus.Subscribe<CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent, CharacterMigration_Handler>();
            eventBus.Subscribe<CharacterNewUpdatedPublicInfoIntegrationEvent, CharacterNewborn_Handler>();
            eventBus.Subscribe<CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent, CorpMigration_Handler>();
            eventBus.Subscribe<CorporationAfterUpdatedNameIntegrationEvent, CorpNew_Handler>();
            //eventBus.Subscribe<KillmailNew_Handler>();
            eventBus.Subscribe<ContractAddNewIntegrationEvent, MarketContract_Handler>();
            eventBus.Subscribe<MarketOrderCountAddedIntegrationEvent, MarketOrderNew_Handler>();
            //eventBus.Subscribe<WarNew_Handler>();
            eventBus.SubscribesCompleted();
        }
    }
}
