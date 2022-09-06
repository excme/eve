using eveDirect.Services.EsiConnector.Jobs;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    public class CorporationNeedUpdateAllianceHistoryIntegrationEventHandler : _DefaultEventHandler<CorporationNeedUpdateAllianceHistoryIntegrationEvent>, IIntegrationEventHandler<CorporationNeedUpdateAllianceHistoryIntegrationEvent>
    {
        public CorporationNeedUpdateAllianceHistoryIntegrationEventHandler(
           IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(CorporationNeedUpdateAllianceHistoryIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Corporation_id}");

                if (@event.Corporation_id > 0)
                {
                    // Обновление истории альнсов
                    var corpAlliance = new CorporationAllianceHistories(_repoPublicCommon, _logFactory.CreateLogger<CorporationAllianceHistories>());
                    corpAlliance.SimpleCorporation(@event.Corporation_id);
                }
            }
            return Task.CompletedTask;
        }
    }
}
