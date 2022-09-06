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
    /// Обработка события, после обновления названия альянса
    /// </summary>
    public class AllianceAfterUpdatedNameIntegrationEventHandler: _DefaultEventHandler<AllianceAfterUpdatedNameIntegrationEventHandler>, IIntegrationEventHandler<AllianceAfterUpdatedNameIntegrationEvent>
    {
        public DbContextOptions<PublicContext> _dbContextOptions { get; }

        public AllianceAfterUpdatedNameIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory,
            DbContextOptions<PublicContext> dbContextOptions
            ) : base(repoPublicCommon, logFactory, eventBus) {
            _dbContextOptions = dbContextOptions;
        }

        public Task Handle(AllianceAfterUpdatedNameIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Alliance_id}");

                if (@event.Alliance_id > 0)
                {
                    using var _context = new PublicContext(_dbContextOptions);
                    var ally = _context.EveOnline_Alliances
                        .Select(x => new { x.alliance_id, x.name })
                        .FirstOrDefault(x => x.alliance_id == @event.Alliance_id);

                    if (ally != null) {

                        // Добавление в поиск
                        var search_item = _context.SearchItems
                            .FirstOrDefault(x => x.type == ESearchItemType.alliance && x.item_id == @event.Alliance_id);
                        if (search_item != null)
                        {
                            search_item.title = ally.name;
                            _context.SearchItems.Update(search_item);
                        }
                        else
                        {
                            search_item = new SearchItem()
                            {
                                type = ESearchItemType.alliance,
                                item_id = @event.Alliance_id,
                                title = ally.name
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
