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
    /// Last Actions. 
    /// </summary>
    public class CorpMigration_Handler : IIntegrationEventHandler<CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent>
    {
        readonly ILogger<CorpMigration_Handler> _logger;
        readonly ILastActionsService _lastActionsService;
        readonly DbContextOptions<PublicContext> _options;
        public CorpMigration_Handler(ILogger<CorpMigration_Handler> logger, ILastActionsService lastActionsService, DbContextOptions<PublicContext> options)
        {
            _logger = logger;
            _lastActionsService = lastActionsService;
            _options = options;
        }

        public Task Handle(CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent @event)
        {
            // Только если была добавлена только последняя запись
            if (@event.New_Created)
            {
                using PublicContext dbContext = new PublicContext(_options);
                var corp = dbContext.EveOnline_Corporations
                    .Select(x => new {
                        x.corporation_id,
                        x.name,
                    })
                    .FirstOrDefault(x => x.corporation_id == @event.Corporation_id);

                if (corp != null)
                {
                    var item = new LastActionModel()
                    {
                        dt = @event.OnDate,
                        item_id = corp.corporation_id,
                        type = ELastActionType.corp_Migration,
                    };

                    _lastActionsService.Add(item);
                }
            }

            return Task.CompletedTask;
        }
    }
}
