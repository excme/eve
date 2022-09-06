using Microsoft.Extensions.Logging;
using eveDirect.Repo.PublicReadOnly;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Repo.PublicReadOnly.Models.Events;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Api.Private.IntegrationEvents
{
    /// <summary>
    /// Необходимо обновить corporation_id range
    /// </summary>
    public class CorporationUpdateRangesIntegrationEventHandler : IIntegrationEventHandler<Corporation_RangeNeedUpdate_IntegrationEvent>
    {
        private readonly ILogger<CorporationUpdateRangesIntegrationEventHandler> _logger;
        private readonly IReadOnly _publicReadOnly;
        private readonly IEventBus _eventBus;
        public CorporationUpdateRangesIntegrationEventHandler(
            IReadOnly publicReadOnly,
            ILogger<CorporationUpdateRangesIntegrationEventHandler> logger,
            IEventBus eventBus
            ){
            _publicReadOnly = publicReadOnly;
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task Handle(Corporation_RangeNeedUpdate_IntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation($"Обработка события - {@event}");

                // Обновление corporation_id range
                await _publicReadOnly.Corporations_CalcIdRanges();

                _eventBus.Publish(
                    new Corporation_RangeUpdated_IntegrationEvent()
                );
            }
        }
    }
}
