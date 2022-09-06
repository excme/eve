using eveDirect.Services.EsiConnector.Jobs;
using eveDirect.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    /// <summary>
    /// Необходимо обновить членство персонажа. Обработка события
    /// </summary>
    public class CharacterNeedUpdateAffilationIntegrationEventHandler : _DefaultEventHandler<AllianceAfterAddIntegrationEventHandler>, IIntegrationEventHandler<CharacterNeedUpdateAffilationIntegrationEvent>
    {
        public CharacterNeedUpdateAffilationIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(CharacterNeedUpdateAffilationIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Character_id}");

                if (@event.Character_id > 0)
                {
                    // Обновление публичной информации альянса
                    var job = new CharacterAffiliation(_repoPublicCommon, _logFactory.CreateLogger<CharacterAffiliation>());
                    job.SimpleTask(new List<int>() { @event.Character_id });
                }

            }

            return Task.CompletedTask;
        }
    }
}
