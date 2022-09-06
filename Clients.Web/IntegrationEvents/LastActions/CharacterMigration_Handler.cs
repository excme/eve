using eveDirect.Clients.Web.Models;
using eveDirect.Clients.Web.Services;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using eveDirect.Shared.EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Clients.Web.IntegrationEvents.LastActions
{
    public class CharacterMigration_Handler : IIntegrationEventHandler<CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent>
    {
        readonly ILogger<CharacterMigration_Handler> _logger;
        readonly ILastActionsService _lastActionsService;
        readonly DbContextOptions<PublicContext> _options;
        public CharacterMigration_Handler(ILogger<CharacterMigration_Handler> logger, ILastActionsService lastActionsService, DbContextOptions<PublicContext> options)
        {
            _logger = logger;
            _lastActionsService = lastActionsService;
            _options = options;
        }

        public Task Handle(CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent @event)
        {
            // Только если была добавлена только последняя запись
            if (@event.New_Created)
            {
                using PublicContext dbContext = new PublicContext(_options);
                var @char = dbContext.EveOnline_Characters
                    .Select(x => new {
                        x.character_id,
                        x.name,
                    })
                    .FirstOrDefault(x => x.character_id == @event.Character_Id);

                if (@char != null)
                {

                    LastActionModel item = new LastActionModel()
                    {
                        dt = @event.OnDate,
                        item_id = @char.character_id,
                        type = ELastActionType.char_Migration
                    };

                    _lastActionsService.Add(item);
                }
            }

            return Task.CompletedTask;
        }
    }
}
