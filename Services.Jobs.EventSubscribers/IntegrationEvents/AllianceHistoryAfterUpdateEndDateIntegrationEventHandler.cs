using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Threading.Tasks;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    /// <summary>
    /// Обработка события. После установки даты окончания членства корпорации в альянсе
    /// </summary>
    public class CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEventHandler : _DefaultEventHandler<CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent>,
        IIntegrationEventHandler<CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent>
    {
        public CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Record_id}");

                if (@event.Record_id > 0)
                {
                    // Получение этой записи из БД
                    var allianceHistory_Item = _repoPublicCommon.Corporation_AllianceHistoryItem(@event.Record_id);

                    // Завершение участие персонажей в этом альянсе
                    _repoPublicCommon.Characters_UpdateEndAllianceMembering(allianceHistory_Item);
                }
            }

            return Task.CompletedTask;
        }
    }
}
