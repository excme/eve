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
    /// Сбор Last Actions - Новая корпорация
    /// </summary>
    public class CorpNew_Handler : IIntegrationEventHandler<CorporationAfterUpdatedNameIntegrationEvent>
    {
        readonly ILogger<CorpNew_Handler> _logger;
        readonly ILastActionsService _lastActionsService;
        readonly DbContextOptions<PublicContext> _options;
        public CorpNew_Handler(ILogger<CorpNew_Handler> logger, ILastActionsService lastActionsService, DbContextOptions<PublicContext> options)
        {
            _logger = logger;
            _lastActionsService = lastActionsService;
            _options = options;
        }

        public Task Handle(CorporationAfterUpdatedNameIntegrationEvent @event)
        {
            // Только если корпорация новосозданная
            if (@event.New_created)
            {
                using PublicContext dbContext = new PublicContext(_options);

                var corp = dbContext.EveOnline_Corporations
                    .Select(x => new { 
                        x.corporation_id,
                        x.name,
                        x.date_founded
                    })
                    .FirstOrDefault(x => x.corporation_id == @event.Corporation_id);

                if (corp != null) {

                    LastActionModel item = new LastActionModel()
                    {
                        dt = corp.date_founded,
                        item_id = corp.corporation_id,
                        type = ELastActionType.corp_New
                    };

                    _lastActionsService.Add(item);
                }
            }

            return Task.CompletedTask;
        }
    }
}
