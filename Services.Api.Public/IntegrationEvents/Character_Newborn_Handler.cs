using eveDirect.Repo.PublicReadOnly;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Repo.ReadWrite.IntegrationEvents;

namespace eveDirect.Api.Public.IntegrationEvents
{
    public class Character_Newborn_Handler : IIntegrationEventHandler<CharacterNewUpdatedPublicInfoIntegrationEvent>
    {
        IReadOnly _publicReadOnly { get; }
        private ILogger<Character_Newborn_Handler> Logger { get; }
        public Character_Newborn_Handler(
            ILogger<Character_Newborn_Handler> logger, 
            IReadOnly publicReadOnly)
        {
            _publicReadOnly = publicReadOnly;
            Logger = logger;
        }

        public async Task Handle(CharacterNewUpdatedPublicInfoIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                Logger.LogInformation($"Обработка события {@event}: {@event.Character_id}");

                // Добавление новорожденного в кэшированный список
                await _publicReadOnly.CharacterNewbornItems_Add(@event.Character_id);
            }
        }
    }
}
