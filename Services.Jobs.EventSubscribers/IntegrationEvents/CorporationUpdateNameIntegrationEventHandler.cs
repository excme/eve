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
    /// Обработка события, после обновления названия корпорации
    /// </summary>
    public class CorporationUpdateNameIntegrationEventHandler : _DefaultEventHandler<CorporationUpdateNameIntegrationEventHandler>, IIntegrationEventHandler<CorporationAfterUpdatedNameIntegrationEvent>
    {
        public DbContextOptions<PublicContext> _dbContextOptions { get; }
        public CorporationUpdateNameIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            IEventBus eventBus,
            ILoggerFactory logFactory,
            DbContextOptions<PublicContext> dbContextOptions
            ) : base(repoPublicCommon, logFactory, eventBus)
        {
            _dbContextOptions = dbContextOptions;
        }
        public Task Handle(CorporationAfterUpdatedNameIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Corporation_id}");

                if (@event.Corporation_id > 0)
                {
                    using var _context = new PublicContext(_dbContextOptions);
                    var corp = _context.EveOnline_Corporations
                        .Select(x => new { x.corporation_id, x.name })
                        .FirstOrDefault(x => x.corporation_id == @event.Corporation_id);

                    if (corp != null)
                    {

                        // Добавление в поиск
                        var search_item = _context.SearchItems
                            .FirstOrDefault(x => x.type == ESearchItemType.corporation && x.item_id == @event.Corporation_id);
                        if (search_item != null)
                        {
                            search_item.title = corp.name;
                            _context.SearchItems.Update(search_item);
                        }
                        else
                        {
                            search_item = new SearchItem()
                            {
                                type = ESearchItemType.corporation,
                                item_id = @event.Corporation_id,
                                title = corp.name
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
