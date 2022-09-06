using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Repo.PublicReadOnly;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using eveDirect.Repo.PublicReadOnly.Models.Events;

namespace eveDirect.Api.Private.IntegrationEvents
{
    /// <summary>
    /// Обработчик, когда необходимо обновить диапозон ид альянсов
    /// </summary>
    public class AllianceUpdateRangesIntegrationEventHandler : IIntegrationEventHandler<Alliance_RangeNeedUpdate_IntegrationEvent>
    {
        private readonly ILogger<AllianceUpdateRangesIntegrationEventHandler> _logger;
        private readonly IReadOnly _publicReadOnly;
        private readonly IEventBus _eventBus;
        public AllianceUpdateRangesIntegrationEventHandler(
            IReadOnly publicReadOnly,
            ILogger<AllianceUpdateRangesIntegrationEventHandler> logger,
            IEventBus eventBus)
        {
            _publicReadOnly = publicReadOnly;
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task Handle(Alliance_RangeNeedUpdate_IntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation($"Обработка события: {@event}");

                // Обновление range
                await _publicReadOnly.Alliances_CalcIdRanges();

                // Уведомление
                _eventBus.Publish(new Alliance_RangeUpdated_IntegrationEvent());
            }
        }
    }
}
