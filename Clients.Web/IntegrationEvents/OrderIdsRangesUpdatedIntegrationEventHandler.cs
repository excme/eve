using Serilog.Context;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.PublicReadOnly.Models.Events;
using eveDirect.Clients.Web.Services;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Clients.Web.IntegrationEvents
{
    /// <summary>
    /// Обновление локального кэша order_ids
    /// Причина отмены - Ордеров добавляется 300к в день. В месяц 9М. Нет смысла хранить в операвке, они не актуальны после деактивации.
    /// </summary>
    //public class OrderIdsRangesUpdatedIntegrationEventHandler : IIntegrationEventHandler<Order_RangeUpdated_IntegrationEvent>
    //{
    //    ICheckExistService CheckExistService { get; }
    //    private ILogger<OrderIdsRangesUpdatedIntegrationEventHandler> Logger { get; }

    //    public OrderIdsRangesUpdatedIntegrationEventHandler(
    //        ILogger<OrderIdsRangesUpdatedIntegrationEventHandler> logger,
    //        ICheckExistService checkExistService)
    //    {
    //        CheckExistService = checkExistService;
    //        Logger = logger;
    //    }

    //    public async Task Handle(Order_RangeUpdated_IntegrationEvent @event)
    //    {
    //        using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
    //        {
    //            Logger.LogInformation($"Обработка события - {@event}");

    //            await CheckExistService.OrdersIdsRanges_Update();
    //        }
    //    }
    //}
}
