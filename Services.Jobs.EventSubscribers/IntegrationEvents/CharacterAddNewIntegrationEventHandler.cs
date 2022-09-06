using eveDirect.Services.EsiConnector.Jobs;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    public class CharacterAfterAddIntegrationEventHandler : _DefaultEventHandler<CharacterAfterAddIntegrationEventHandler>, IIntegrationEventHandler<CharacterAfterAddIntegrationEvent>
    {
        public CharacterAfterAddIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) { }

        public Task Handle(CharacterAfterAddIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Character_id}");

                if (@event.Character_id > 0)
                {
                    // Обновление публичной информации корпорации
                    var characterPublicInformation = new CharacterPublicInformationJob(_repoPublicCommon, _logFactory.CreateLogger<CharacterPublicInformationJob>());
                    bool success = characterPublicInformation.SimpleCharacter(@event.Character_id);
                    // Уведомлние, что в базе заполнена информация о новом персонаже
                    if(success)
                        _eventBus.Publish(new CharacterNewUpdatedPublicInfoIntegrationEvent(@event.Character_id, @event.New_Created));

                    // Обновление истории корпораций
                    var characterCorp = new CharacterCorpHistory(_repoPublicCommon, _logFactory.CreateLogger<CharacterCorpHistory>());
                    characterCorp.SimpleCharacter(@event.Character_id);
                }
            }

            return Task.CompletedTask;
        }
    }
}
