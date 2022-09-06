using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Repo.PublicReadOnly.Models.Events;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace eveDirect.Api.Private.IntegrationEvents
{
    /// <summary>
    /// Необходимо обновить contract_id range
    /// </summary>
    public class ContractUpdateRangesIntegrationEventHandler : IIntegrationEventHandler<Contract_RangeNeedUpdate_IntegrationEvent>
    {
        private readonly ILogger<CharacterUpdateRangesIntegrationEventHandler> _logger;
        private readonly IReadOnly _publicReadOnly;
        private readonly IEventBus _eventBus;
        public ContractUpdateRangesIntegrationEventHandler(
            IReadOnly publicReadOnly,
            ILogger<CharacterUpdateRangesIntegrationEventHandler> logger,
            IEventBus eventBus)
        {
            _publicReadOnly = publicReadOnly;
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task Handle(Contract_RangeNeedUpdate_IntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation($"Обработка события: {@event}");

                // Обновление character_id range
                await _publicReadOnly.Contracts_CalcIdRanges();

                _eventBus.Publish(
                    new Contract_RangeUpdated_IntegrationEvent()
                );
            }
        }
    }
}
