using eveDirect.Databases;
using Microsoft.EntityFrameworkCore;
using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Services.EsiConnector.Jobs;
using Microsoft.Extensions.Logging;

using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    /// <summary>
    /// Прослушиватель события "новый killmail"
    /// </summary>
    public class KillmailAddNewIntegrationEventHandler : _DefaultEventHandler<KillmailAddNewIntegrationEventHandler>, IIntegrationEventHandler<KillmailAddNewIntegrationEvent>
    {
        public KillmailAddNewIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(KillmailAddNewIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event} {@event.Killmail_Id}");

                if (@event.Killmail_Id > 0)
                {
                    var killmail_info = _repoPublicCommon.Killmail_Get(@event.Killmail_Id);
                    if(killmail_info != null)
                    {
                        // Получение данных из коннектора и обработка данных
                        KillmailsInfos killmailsInfos = new KillmailsInfos(_repoPublicCommon, _logFactory.CreateLogger<KillmailsInfos>());
                        killmailsInfos.Simple(killmail_info);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
