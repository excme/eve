using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;
using System;
using Microsoft.EntityFrameworkCore;
using eveDirect.Databases.Contexts;
using eveDirect.Services.Jobs.Migrations;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    /// <summary>
    /// Обработка события после добавление или изменения у персонажа записи в истории корпораций
    /// </summary>
    public class CharacterAfterUpdatedCorporationHistoryItemIntegrationEventHandler : _DefaultEventHandler<CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent>, IIntegrationEventHandler<CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent>
    {
        DbContextOptions<PublicContext> _options { get; }
        public CharacterAfterUpdatedCorporationHistoryItemIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            DbContextOptions<PublicContext> options,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon, logFactory, eventBus) {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public Task Handle(CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Character_Id}");

                if (@event.Character_Id > 0)
                {
                    var job = new CharactersAllianceHistorу_Job(_repoPublicCommon, _options, _logFactory.CreateLogger<CharactersAllianceHistorу_Job>(), _eventBus);
                    job.SimpleCharacter(@event.Character_Id);
                }
            }

            return Task.CompletedTask;
        }
    }
}
