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
    /// Last Actions. Новые ордера
    /// </summary>
    public class MarketOrderNew_Handler : IIntegrationEventHandler<MarketOrderCountAddedIntegrationEvent>
    {
        readonly ILogger<MarketOrderNew_Handler> _logger;
        readonly ILastActionsService _lastActionsService;
        //readonly DbContextOptions<PublicContext> _options;
        public MarketOrderNew_Handler(ILogger<MarketOrderNew_Handler> logger, ILastActionsService lastActionsService/*, DbContextOptions<PublicContext> options*/)
        {
            _logger = logger;
            _lastActionsService = lastActionsService;
            //_options = options;
        }

        public Task Handle(MarketOrderCountAddedIntegrationEvent @event)
        {
            LastActionModel item = new LastActionModel()
            {
                dt = @event.OnDate,
                item_id = @event.Count,
                type = ELastActionType.Order
            };

            _lastActionsService.Add(item);

            return Task.CompletedTask;
        }
    }
}
