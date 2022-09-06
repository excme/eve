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
    /// Сбор Last Actions - Новый альянс
    /// </summary>
    public class AllianceNew_Handler : IIntegrationEventHandler<AllianceAfterUpdatedNameIntegrationEvent>
    {
        readonly ILogger<AllianceNew_Handler> _logger;
        readonly ILastActionsService _lastActionsService;
        readonly DbContextOptions<PublicContext> _options;
        public AllianceNew_Handler(ILogger<AllianceNew_Handler> logger, ILastActionsService lastActionsService, DbContextOptions<PublicContext> options)
        {
            _logger = logger;
            _lastActionsService = lastActionsService;
            _options = options;
        }

        public Task Handle(AllianceAfterUpdatedNameIntegrationEvent @event)
        {
            // Только если альянс новосозданный
            if (@event.New_Created)
            {
                using PublicContext dbContext = new PublicContext(_options);

                var alliance = dbContext.EveOnline_Alliances
                    .Select(x => new {
                        x.alliance_id,
                        x.name,
                        x.date_founded
                    })
                    .FirstOrDefault(x => x.alliance_id == @event.Alliance_id);

                if (alliance != null)
                {

                    LastActionModel item = new LastActionModel()
                    {
                        dt = alliance.date_founded,
                        item_id = alliance.alliance_id,
                        type = ELastActionType.ally_New
                    };

                    _lastActionsService.Add(item);
                }
            }

            return Task.CompletedTask;
        }
    }
}
