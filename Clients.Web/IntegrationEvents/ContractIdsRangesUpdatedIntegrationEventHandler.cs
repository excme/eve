using Serilog.Context;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.PublicReadOnly.Models.Events;
using eveDirect.Clients.Web.Services;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Clients.Web.IntegrationEvents
{
    /// <summary>
    /// Обновление локалльного кэша contract_ids
    /// </summary>
    public class ContractIdsRangesUpdatedIntegrationEventHandler : IIntegrationEventHandler<Contract_RangeUpdated_IntegrationEvent>
    {
        ICheckExistService CheckExistService { get; }
        private ILogger<ContractIdsRangesUpdatedIntegrationEventHandler> Logger { get; }

        public ContractIdsRangesUpdatedIntegrationEventHandler(
            ILogger<ContractIdsRangesUpdatedIntegrationEventHandler> logger,
            ICheckExistService checkExistService)
        {
            CheckExistService = checkExistService;
            Logger = logger;
        }

        public async Task Handle(Contract_RangeUpdated_IntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                Logger.LogInformation($"Обработка события - {@event}");

                await CheckExistService.ContractsIdsRanges_Update();
            }
        }
    }
}
