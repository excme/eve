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
    public class CharacterAfterUpdatedAllianceIntegrationEventHandler : _DefaultEventHandler<CharacterAfterUpdatedAllianceIntegrationEvent>, IIntegrationEventHandler<CharacterAfterUpdatedAllianceIntegrationEvent>
    {
        public CharacterAfterUpdatedAllianceIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(CharacterAfterUpdatedAllianceIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Character_id}");

                if (@event.Character_id > 0)
                {
                    
                }

                return Task.CompletedTask;
            }
        }
    }
}
