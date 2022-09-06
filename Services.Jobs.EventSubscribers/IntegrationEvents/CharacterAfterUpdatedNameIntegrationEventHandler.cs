using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Databases.Contexts.Public.Models;
using Microsoft.EntityFrameworkCore;
using eveDirect.Databases.Contexts;
using System.Linq;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    /// <summary>
    /// Обработка события, после обновления имени персонажа
    /// </summary>
    public class CharacterAfterUpdatedNameIntegrationEventHandler : _DefaultEventHandler<CharacterAfterUpdatedNameIntegrationEventHandler>, IIntegrationEventHandler<CharacterAfterUpdatedNameIntegrationEvent>
    {
        public DbContextOptions<PublicContext> _dbContextOptions { get; }
        public CharacterAfterUpdatedNameIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory,
            DbContextOptions<PublicContext> dbContextOptions
            ) : base(repoPublicCommon, logFactory, eventBus)
        {
            _dbContextOptions = dbContextOptions;
        }
        public Task Handle(CharacterAfterUpdatedNameIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Character_id}");

                if (@event.Character_id > 0)
                {
                    using var _context = new PublicContext(_dbContextOptions);
                    var character = _context.EveOnline_Characters
                        .Select(x => new { x.character_id, x.name })
                        .FirstOrDefault(x => x.character_id == @event.Character_id);

                    if (character != null)
                    {

                        // Добавление в поиск
                        var search_item = _context.SearchItems
                            .FirstOrDefault(x => x.type == ESearchItemType.character && x.item_id == @event.Character_id);
                        if (search_item != null)
                        {
                            search_item.title = character.name;
                            _context.SearchItems.Update(search_item);
                        }
                        else
                        {
                            search_item = new SearchItem()
                            {
                                type = ESearchItemType.character,
                                item_id = @event.Character_id,
                                title = character.name
                            };
                            _context.SearchItems.Add(search_item);
                        }

                        _context.SaveChanges();
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
