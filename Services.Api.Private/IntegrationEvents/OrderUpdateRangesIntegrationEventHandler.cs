using Microsoft.Extensions.Logging;
using eveDirect.Repo.PublicReadOnly;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Repo.PublicReadOnly.Models.Events;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Api.Private.IntegrationEvents
{
    /// <summary>
    /// Необходимо обновить order_id range
    /// </summary>
    public class OrderUpdateRangesIntegrationEventHandler : IIntegrationEventHandler<Order_RangeNeedUpdate_IntegrationEvent>
    {
        private readonly ILogger<CorporationUpdateRangesIntegrationEventHandler> _logger;
        private readonly IReadOnly _publicReadOnly;
        private readonly IEventBus _eventBus;
        public OrderUpdateRangesIntegrationEventHandler(
            IReadOnly publicReadOnly,
            ILogger<CorporationUpdateRangesIntegrationEventHandler> logger,
            IEventBus eventBus
        )
        {
            _publicReadOnly = publicReadOnly;
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task Handle(Order_RangeNeedUpdate_IntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation($"Обработка события - ({@event})");

                // Обновление id range
                await _publicReadOnly.Orders_CalcIdRanges();

                _eventBus.Publish(
                    new Order_RangeUpdated_IntegrationEvent()
                );
            }
        }
    }
}
