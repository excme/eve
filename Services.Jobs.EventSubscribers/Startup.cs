using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using eveDirect.Repo.ReadWrite;
using eveDirect.Databases.Contexts;
using eveDirect.Shared.WebHost;
using eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents;

namespace eveDirect.Jobs.EventSubscribers
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

        public void ConfigureServices(IServiceCollection services)
        {
            // Подключение к БД
            string connString = environment.IsProduction() 
                ? configuration["ConnectionStrings:PublicDb"] 
                : configuration["ConnectionStrings:PublicDb-Test"];

            services.AddDbOptions<PublicContext>(connString);

            // Сервис - Репозиторий
            services.AddTransient<IReadWrite, ReadWriteRepo>();

            // Подключение к RabbitMQ
            services.ConnectToRabbitMQ(configuration, Program.AppName, environment);

            // Прослушиватели сообщений
            services.AddTransient<CharacterAfterAddIntegrationEventHandler>();
            services.AddTransient<CharacterNeedUpdateAffilationIntegrationEventHandler>();
            services.AddTransient<CharacterAfterUpdatedCorporationIntegrationEventHandler>();
            services.AddTransient<CharacterAfterUpdatedAllianceIntegrationEventHandler>();
            services.AddTransient<CharacterAfterUpdatedFactionIntegrationEventHandler>();
            services.AddTransient<CharacterAfterUpdatedNameIntegrationEventHandler>();

            services.AddTransient<AllianceAfterAddIntegrationEventHandler>();

            services.AddTransient<CorporationAfterAddIntegrationEventHandler>();
            services.AddTransient<CorporationNeedUpdateAllianceHistoryIntegrationEventHandler>();
            services.AddTransient<CorporationNeedUpdatePublicInfoIntegrationEventHandler>();
            services.AddTransient<CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEventHandler>();

            services.AddTransient<MarketOrderAfterAddIntegrationEventHandler>();
            services.AddTransient<MarketOrderAfterDisableStatusIntegrationEventHandler>();
            services.AddTransient<UniverseStructureAfterAddIntegrationEventHandler>();
            services.AddTransient<KillmailAddNewIntegrationEventHandler>();
            services.AddTransient<CharacterNeedUpdateCorporationHistoryIntehrationEventHandler>();

            services.AddOptions();
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventBus eventBus)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Character. Public
            // Запрос добавления
            eventBus.Subscribe<CharacterAfterAddIntegrationEvent, CharacterAfterAddIntegrationEventHandler>();
            // Запрос обновления affilation
            eventBus.Subscribe<CharacterNeedUpdateAffilationIntegrationEvent, CharacterNeedUpdateAffilationIntegrationEventHandler>();
            // После обновления corporation_id
            eventBus.Subscribe<CharacterAfterUpdatedCorporationIntegrationEvent, CharacterAfterUpdatedCorporationIntegrationEventHandler>();
            // После обновления alliance_id
            eventBus.Subscribe<CharacterAfterUpdatedAllianceIntegrationEvent, CharacterAfterUpdatedAllianceIntegrationEventHandler>();
            // После обновления faction_id
            eventBus.Subscribe<CharacterAfterUpdatedFactionIntegrationEvent, CharacterAfterUpdatedFactionIntegrationEventHandler>();
            // После обновления name
            eventBus.Subscribe<CharacterAfterUpdatedNameIntegrationEvent, CharacterAfterUpdatedNameIntegrationEventHandler>();
            // Запрос обновления corpHistory
            eventBus.Subscribe<CharacterNeedUpdateCorporationHistoryIntehrationEvent, CharacterNeedUpdateCorporationHistoryIntehrationEventHandler>();

            // Alliance. Public
            // Запрос добавления
            eventBus.Subscribe<AllianceAfterAddIntegrationEvent, AllianceAfterAddIntegrationEventHandler>();
            // После обновления name
            eventBus.Subscribe<AllianceAfterUpdatedNameIntegrationEvent, AllianceAfterUpdatedNameIntegrationEventHandler>();

            // Corporation. Public
            // Запрос добавления
            eventBus.Subscribe<CorporationAfterAddIntegrationEvent, CorporationAfterAddIntegrationEventHandler>();
            // Запрос обновления истории альянсов у корпорации
            eventBus.Subscribe<CorporationNeedUpdatePublicInfoIntegrationEvent, CorporationNeedUpdatePublicInfoIntegrationEventHandler>();
            // Запрос обновления истории альянса
            eventBus.Subscribe<CorporationNeedUpdateAllianceHistoryIntegrationEvent, CorporationNeedUpdateAllianceHistoryIntegrationEventHandler>();
            // После обновления name
            eventBus.Subscribe<CorporationAfterUpdatedNameIntegrationEvent, CorporationUpdateNameIntegrationEventHandler>();
            // После появления в записи истории альянсов времени выхода из альянса 
            eventBus.Subscribe<CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent, CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEventHandler>();


            // Market. Public
            // После добавления
            eventBus.Subscribe<MarketOrderAfterAddIntegrationEvent, MarketOrderAfterAddIntegrationEventHandler>();
            // После изменения статуса
            eventBus.Subscribe<MarketOrderAfterDisableStatusIntegrationEvent, MarketOrderAfterDisableStatusIntegrationEventHandler>();

            // Universe. Public
            // После досбавления новой структуры
            eventBus.Subscribe<UniverseStructureAfterAddIntergrationEvent, UniverseStructureAfterAddIntegrationEventHandler>();

            // ����� killmail_id + hash
            //eventBus.Subscribe<KillmailAddNewIntegrationEvent, KillmailAddNewIntegrationEventHandler>();

            eventBus.SubscribesCompleted();
        }
    }
}
