using Serilog.Context;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.PublicReadOnly.Models.Events;
using eveDirect.Clients.Web.Services;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Clients.Web.IntegrationEvents
{
    /// <summary>
    /// Обновление локалльного кэша alliance_ids
    /// </summary>
    public class AllianceIdsRangesUpdatedIntegrationEventHandler : IIntegrationEventHandler<Alliance_RangeUpdated_IntegrationEvent>
    {
        private readonly ICheckExistService _checkExistService;
        private readonly ILogger<AllianceIdsRangesUpdatedIntegrationEventHandler> _logger;
        public AllianceIdsRangesUpdatedIntegrationEventHandler(
            ICheckExistService checkExistService,
            ILogger<AllianceIdsRangesUpdatedIntegrationEventHandler> logger
            )
        {
            _checkExistService = checkExistService;
            _logger = logger;
        }

        public async Task Handle(Alliance_RangeUpdated_IntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation($"Обработка события - {@event}");

                await _checkExistService.AlliancesIdsRanges_Update();
            }
        }
    }
}
