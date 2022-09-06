using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using eveDirect.Repo.PublicReadOnly;
using Microsoft.Extensions.Hosting;
using eveDirect.Translation.DbContext.IntegrationEvents;
using eveDirect.Translation.DbContext;
using eveDirect.Shared.WebHost;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using eveDirect.Databases.Contexts;
using eveDirect.Api.Public.Services;
using eveDirect.Api.Public.IntegrationEvents;
using eveDirect.Shared.EventBus.EventBusRabbitMQ;
using Moq;

namespace eveDirect.Api.Public
{
    public class Startup : ApiStartupBase
    {
        IWebHostEnvironment environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, Program.AppName) {
            this.environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connString = environment.IsProduction()
                ? configuration["ConnectionStrings:PublicDb"]
                //: configuration["ConnectionStrings:PublicDb-Test"];
                : configuration["ConnectionStrings:PublicDb"];

            // Публичные данные евы
            services.AddDbOptions<PublicContext>(connString);
            services.AddDbOptions<ApplicationDbContext>(configuration["ConnectionStrings:TranDb"]);

            // Подключение Репозиториев
            services.AddSingleton<IReadOnly, ReadOnly>();
            services.AddSingleton<ILanguageService, LanguageService>();

            ApiConfigureServices(services);

            // Выполнение при старте
            services.AddHostedService<StartupService>();

            // Подключение к брокеру сообщений
            services.ConnectToRabbitMQ(configuration, Program.AppName, environment);

            if (environment.IsProduction())
            {
                // Запуск обработчиков событий
                services.AddTransient<Character_Newborn_Handler>();
                //services.AddTransient<Character_MigrationsCount_Handler>();
                services.AddTransient<LanguageAfterUpdatedVersionIntegrationEventHandler>();
            }

            if (configuration["UseCors"] != null && configuration["UseCors"] == "true")
            {
                services.AddCors(); // добавляем сервисы CORS
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventBus eventBus)
        {
            ApiConfigure(app, env);
            if (environment.IsProduction())
            {
                // Новый персонаж
                eventBus.Subscribe<CharacterNewUpdatedPublicInfoIntegrationEvent, Character_Newborn_Handler>();

                // Обновление числа миграций персонажей между корпорациями
                //eventBus.Subscribe<CharacterCorpHistoryItemsCountIntegrationEvent, Character_MigrationsCount_Handler>();

                eventBus.Subscribe<LanguageAfterUpdatedVersionIntegrationEvent, LanguageAfterUpdatedVersionIntegrationEventHandler>();
            }

            if (configuration["UseCors"] != null && configuration["UseCors"] == "true")
            {
                // подключаем CORS
                app.UseCors(builder => builder.AllowAnyOrigin());
            }
        }
    }
}
