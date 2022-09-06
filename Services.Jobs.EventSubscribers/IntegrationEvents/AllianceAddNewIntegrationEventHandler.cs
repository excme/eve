using eveDirect.Services.EsiConnector.Jobs;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    /// <summary>
    /// Новый альянс. Обработка события
    /// </summary>
    public class AllianceAfterAddIntegrationEventHandler : _DefaultEventHandler<AllianceAfterAddIntegrationEventHandler>, IIntegrationEventHandler<AllianceAfterAddIntegrationEvent>
    {
        public AllianceAfterAddIntegrationEventHandler(
            IReadWrite repoPublicCommon, 
            IEventBus eventBus,
            ILoggerFactory logFactory
            ): base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(AllianceAfterAddIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Alliance_id}");

                if (@event.Alliance_id > 0)
                {
                    // Обновление публичной информации альянса
                    AlliancesPublicInformation alliancesPublicInformation = new AlliancesPublicInformation(_repoPublicCommon, _logFactory.CreateLogger<AlliancesPublicInformation>());
                    alliancesPublicInformation.SimpleAlliance(@event.Alliance_id, @event.New_Created);

                    // Запрос внутренних корпораций
                    var alliancesGetListCorporations = new AlliancesGetListCorporations(_repoPublicCommon, _logFactory.CreateLogger<AlliancesGetListCorporations>());
                    alliancesGetListCorporations.SimpleAlliance(@event.Alliance_id);
                }

            }

            return Task.CompletedTask;
        }
    }
}
