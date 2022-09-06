using System.Threading.Tasks;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Repo.PublicReadOnly.Models.Events;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Api.Private.IntegrationEvents
{
    /// <summary>
    /// Необходимо обновить character_id range
    /// </summary>
    public class CharacterUpdateRangesIntegrationEventHandler : IIntegrationEventHandler<Character_RangeNeedUpdate_IntegrationEvent>
    {
        private readonly ILogger<CharacterUpdateRangesIntegrationEventHandler> _logger;
        private readonly IReadOnly _publicReadOnly;
        private readonly IEventBus _eventBus;
        public CharacterUpdateRangesIntegrationEventHandler(
            IReadOnly publicReadOnly,
            ILogger<CharacterUpdateRangesIntegrationEventHandler> logger,
            IEventBus eventBus){
            _publicReadOnly = publicReadOnly;
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task Handle(Character_RangeNeedUpdate_IntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation($"Обработка события: {@event}");

                // Обновление character_id range
                await _publicReadOnly.Characters_CalcIdRanges();

                _eventBus.Publish(
                    new Character_RangeUpdated_IntegrationEvent()
                );
            }
        }
    }
}
