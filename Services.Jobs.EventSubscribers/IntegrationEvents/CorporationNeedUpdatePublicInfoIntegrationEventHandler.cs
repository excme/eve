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
    /// Запрос обновления у корпорации истории альянса. Обработка события
    /// </summary>
    public class CorporationNeedUpdatePublicInfoIntegrationEventHandler : _DefaultEventHandler<CorporationNeedUpdatePublicInfoIntegrationEvent>, IIntegrationEventHandler<CorporationNeedUpdatePublicInfoIntegrationEvent>
    {
        public CorporationNeedUpdatePublicInfoIntegrationEventHandler(
           IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(CorporationNeedUpdatePublicInfoIntegrationEvent @event)
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
