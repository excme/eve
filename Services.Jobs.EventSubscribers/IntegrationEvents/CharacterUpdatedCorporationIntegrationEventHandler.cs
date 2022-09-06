using eveDirect.Services.EsiConnector.Jobs;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    public class CharacterAfterUpdatedCorporationIntegrationEventHandler : _DefaultEventHandler<CharacterAfterUpdatedCorporationIntegrationEventHandler>, IIntegrationEventHandler<CharacterAfterUpdatedCorporationIntegrationEvent>
    {
        public CharacterAfterUpdatedCorporationIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(CharacterAfterUpdatedCorporationIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event} {@event.Character_id}");

                if (@event.Character_id > 0)
                {
                    // Обновление истории корпораций
                    var characterCorpHistory = new CharacterCorpHistory(_repoPublicCommon, _logFactory.CreateLogger<CharacterCorpHistory>());
                    characterCorpHistory.SimpleCharacter(@event.Character_id);
                }
            }

            return Task.CompletedTask;
        }
    }
}
