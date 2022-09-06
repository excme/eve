using eveDirect.Clients.Web.Models;
using eveDirect.Clients.Web.Services;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using eveDirect.Shared.EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Clients.Web.IntegrationEvents.LastActions
{
    public class MarketContract_Handler : IIntegrationEventHandler<ContractAddNewIntegrationEvent>
    {
        readonly ILogger<MarketContract_Handler> _logger;
        readonly ILastActionsService _lastActionsService;
        readonly DbContextOptions<PublicContext> _options;
        public MarketContract_Handler(ILogger<MarketContract_Handler> logger, ILastActionsService lastActionsService, DbContextOptions<PublicContext> options)
        {
            _logger = logger;
            _lastActionsService = lastActionsService;
            _options = options;
        }

        public Task Handle(ContractAddNewIntegrationEvent @event)
        {
            dynamic data = new ExpandoObject();
            data.volume = @event.Volume;
            data.type = @event.Type;
            data.region_id = @event.Region_Id;

            LastActionModel item = new LastActionModel()
            {
                dt = @event.OnDate,
                item_id = @event.Contract_id,
                type = ELastActionType.Contract,
                data = data
            };

            _lastActionsService.Add(item);

            return Task.CompletedTask;
        }
    }
}
