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
    /// Новая корпорация. Обработка события
    /// </summary>
    public class CorporationAfterAddIntegrationEventHandler : _DefaultEventHandler<CorporationAfterAddIntegrationEventHandler>, IIntegrationEventHandler<CorporationAfterAddIntegrationEvent>
    {
        public CorporationAfterAddIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(CorporationAfterAddIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Corporation_id}");

                if (@event.Corporation_id > 0)
                {
                    // Обновление публичной информации корпорации
                    var corpPublicInformation = new CorporationPublicInformation(_repoPublicCommon, _logFactory.CreateLogger<CorporationPublicInformation>());
                    corpPublicInformation.SimpleCorporation(@event.Corporation_id);
                }
            }
            return Task.CompletedTask;
        }
    }
}
