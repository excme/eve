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
    /// <summary>
    /// Last Action. Новорожденный персонаж
    /// </summary>
    public class CharacterNewborn_Handler : IIntegrationEventHandler<CharacterNewUpdatedPublicInfoIntegrationEvent>
    {
        readonly ILogger<CharacterNewborn_Handler> _logger;
        readonly ILastActionsService _lastActionsService;
        readonly DbContextOptions<PublicContext> _options;
        public CharacterNewborn_Handler(ILogger<CharacterNewborn_Handler> logger, ILastActionsService lastActionsService, DbContextOptions<PublicContext> options)
        {
            _logger = logger;
            _lastActionsService = lastActionsService;
            _options = options;
        }

        public Task Handle(CharacterNewUpdatedPublicInfoIntegrationEvent @event)
        {
            // Только если персонаж новорожденный
            if (@event.New_Created)
            {
                using PublicContext dbContext = new PublicContext(_options);

                var @char = dbContext.EveOnline_Characters
                    .Select(x => new {
                        x.character_id,
                        x.name,
                        x.birthday
                    })
                    .FirstOrDefault(x => x.character_id == @event.Character_id);

                if (@char != null)
                {
                    LastActionModel item = new LastActionModel()
                    {
                        dt = @char.birthday,
                        item_id = @char.character_id,
                        type = ELastActionType.char_New
                    };

                    _lastActionsService.Add(item);
                }
            }

            return Task.CompletedTask;
        }
    }
}
