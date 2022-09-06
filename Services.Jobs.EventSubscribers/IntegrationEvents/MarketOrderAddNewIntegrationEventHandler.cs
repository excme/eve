using eveDirect.Services.EsiConnector.Jobs;
using eveDirect.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    public class MarketOrderAfterAddIntegrationEventHandler : _DefaultEventHandler<MarketOrderAfterAddIntegrationEventHandler>, IIntegrationEventHandler<MarketOrderAfterAddIntegrationEvent>
    {
        public MarketOrderAfterAddIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(MarketOrderAfterAddIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Order_id}");

                if (@event.Order_id > 0)
                {
                    
                }

            }

            return Task.CompletedTask;
        }
    }
}
