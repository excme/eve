using eveDirect.Repo.ReadWrite.IntegrationEvents;
using eveDirect.Repo.PublicReadOnly;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Api.Public.IntegrationEvents
{
    public class Character_MigrationsCount_Handler : IIntegrationEventHandler<CharacterCorpHistoryItemsCountIntegrationEvent>
    {
        IReadOnly _publicReadOnly { get; }
        private ILogger<Character_MigrationsCount_Handler> Logger { get; }
        public Character_MigrationsCount_Handler(
            ILogger<Character_MigrationsCount_Handler> logger,
            IReadOnly publicReadOnly)
        {
            _publicReadOnly = publicReadOnly;
            Logger = logger;
        }

        public Task Handle(CharacterCorpHistoryItemsCountIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                //Logger.LogInformation($"Обработка события {@event}: {@event.Total_Count}");

                // Обновление общего числа миграций персонажей между корпорациями
                //_publicReadOnly.Characters_MigrationsRoot_UpdateTotalCount(@event.Total_Count);
            }

            return Task.CompletedTask;
        }
    }
}
