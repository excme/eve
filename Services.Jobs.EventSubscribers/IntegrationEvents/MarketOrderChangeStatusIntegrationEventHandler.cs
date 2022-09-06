using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Repo.ReadWrite;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    public class MarketOrderAfterDisableStatusIntegrationEventHandler : _DefaultEventHandler<MarketOrderAfterDisableStatusIntegrationEventHandler>, IIntegrationEventHandler<MarketOrderAfterDisableStatusIntegrationEvent>
    {
        public MarketOrderAfterDisableStatusIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(MarketOrderAfterDisableStatusIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Order_id}");

                if (@event.Order_id > 0)
                {

                }

                return Task.CompletedTask;
            }
        }
    }
}
