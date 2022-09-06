using Serilog.Context;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.PublicReadOnly.Models.Events;
using eveDirect.Clients.Web.Services;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Clients.Web.IntegrationEvents
{
    /// <summary>
    /// Обновление локалльного кэша corporation_ids
    /// </summary>
    public class CorporationIdsRangesUpdatedIntegrationEventHandler : IIntegrationEventHandler<Corporation_RangeUpdated_IntegrationEvent>
    {
        private readonly ICheckExistService _checkExistService;
        private readonly ILogger<CorporationIdsRangesUpdatedIntegrationEventHandler> _logger;
        public CorporationIdsRangesUpdatedIntegrationEventHandler(
            ICheckExistService checkExistService,
            ILogger<CorporationIdsRangesUpdatedIntegrationEventHandler> logger
            )
        {
            _checkExistService = checkExistService;
            _logger = logger;
        }

        public async Task Handle(Corporation_RangeUpdated_IntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation($"Обработка события - {@event}");

                await _checkExistService.CorporationsIdsRanges_Update();
            }
        }
    }
}
