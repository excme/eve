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
    /// Обработка запроса обновления истории корпорации у перснонажа
    /// </summary>
    public class CharacterNeedUpdateCorporationHistoryIntehrationEventHandler : _DefaultEventHandler<CharacterNeedUpdateCorporationHistoryIntehrationEvent>, IIntegrationEventHandler<CharacterNeedUpdateCorporationHistoryIntehrationEvent>
    {
        public CharacterNeedUpdateCorporationHistoryIntehrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(CharacterNeedUpdateCorporationHistoryIntehrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Character_Id}");

                if (@event.Character_Id > 0)
                {
                    var job = new CharacterCorpHistory(_repoPublicCommon, _logFactory.CreateLogger<CharacterCorpHistory>());
                    job.SimpleCharacter(@event.Character_Id);
                }
            }

            return Task.CompletedTask;
        }
    }
}
