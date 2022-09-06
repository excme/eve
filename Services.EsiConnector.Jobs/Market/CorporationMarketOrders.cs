using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationMarketOrders : ConnectorJob
    {
        //static string l_reqName = "Corporation_MarketsOrders";
        //static string l_scope = Scope.Markets.ReadCorporationOrders.Name;
        //static ERequestFolder l_folder = ERequestFolder.Market;
        //static string[] l_needed_roles = new string[] { "Accountant", "Trader" };
        //public CorporationMarkets() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationMarkets(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //void MarketOrders(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    var ConnectorResult = SsoOnePage<CorporationOrdersResult, CorporationOrdersResult.CorporationOrdersItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Market.GetOrders, sso.corporation_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var db_values = _dbContext.Eveonline_MarketOrderRefs.Where(x => x.receipt_id == sso.corporation_id)
        //                .Join(_dbContext.Eveonline_MarketOrders.Where(x => x.state == CharacterOrdersHistoryResult.EState.active),
        //                    rf => rf.marketOrder_id,
        //                    ord => ord.order_id,
        //                    (rf, ord) => new { rf, ord }
        //                )
        //                .Select(x => x.ord)
        //                .ToList();

        //            // Обновление и добавление
        //            //foreach (var _order in ConnectorResult.items)
        //            Parallel.ForEach(ConnectorResult.items, new ParallelOptions() { MaxDegreeOfParallelism = 30 }, _order =>
        //            {
        //                var predicate = new Func<EveOnlineMarketOrder, bool>(x => x.order_id == _order.order_id);
        //                var newValue = new EveOnlineMarketOrder() { order_id = _order.order_id };
        //                Action<EveContextDbContext> moreAdd = (dbContext) =>
        //                {
        //                    //Добавление связи и сохранение
        //                    dbContext.Eveonline_MarketOrderRefs.Add(new EveOnlineMarketOrderRef() { marketOrder_id = _order.order_id, receipt_id = sso.corporation_id });
        //                };
        //                GenericOperations.UpdateItem(_order, db_values, predicate, newValue, _dbContext, moreAdd, true);
        //            });

        //            dbChanges = _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationMarket, ConnectorResult.items != null ? ConnectorResult.items.Count : 0, dbChanges);
        //}
        //void MarketOrdersHistory(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    var ConnectorResult = SsoPaged<CorporationOrdersHistoryResult, CorporationOrdersHistoryResult.CorporationOrdersHistoryItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Market.GetOrdersHistory, sso.corporation_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var db_values = _dbContext.Eveonline_MarketOrderRefs.Where(x => x.receipt_id == sso.corporation_id)
        //                .Join(_dbContext.Eveonline_MarketOrders.Where(x => x.state != CharacterOrdersHistoryResult.EState.active),
        //                    rf => rf.marketOrder_id,
        //                    ord => ord.order_id,
        //                    (rf, ord) => new { rf, ord }
        //                )
        //                .Select(x => x.ord)
        //                .ToList();

        //            // Обновление и добавление
        //            //foreach (var _order in ConnectorResult.items)
        //            Parallel.ForEach(ConnectorResult.items, new ParallelOptions() { MaxDegreeOfParallelism = 30 }, _order =>
        //            {
        //                var predicate = new Func<EveOnlineMarketOrder, bool>(x => x.order_id == _order.order_id);
        //                var newValue = new EveOnlineMarketOrder() { order_id = _order.order_id };
        //                Action<EveContextDbContext> moreAdd = (dbContext) =>
        //                {
        //                    //Добавление связи и сохранение
        //                    dbContext.Eveonline_MarketOrderRefs.Add(new EveOnlineMarketOrderRef() { marketOrder_id = _order.order_id, receipt_id = sso.corporation_id });
        //                };
        //                GenericOperations.UpdateItem(_order, db_values, predicate, newValue, _dbContext, moreAdd, true);
        //            });

        //            dbChanges = _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationMarketHistory, ConnectorResult.items != null ? ConnectorResult.items.Count : 0, dbChanges);
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    MarketOrders(sso);
        //    MarketOrdersHistory(sso);

        //    //using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    //{
        //    //    // Запрос оредеров
        //    //    Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Market.GetOrders(sso.corporation_id).ExecuteAsync);
        //    //    var request = _eveOnlineGeneric.ExecuteRequest<CorporationOrdersResult>(запросКоннектора, folder, CorporationOrdersResult.TimeExpire(), CorporationOrdersResult.GetArgs(sso.corporation_id)).GetAwaiter().GetResult();
        //    //    _logger.LogInformation($"{reqName}. corporation {sso.corporation_id} success = {request.success}. # {request.value?.Count}");

        //    //    if (request.success)
        //    //    {
        //    //        _eveOnlineGeneric.Sso_RequestStatistic(sso.corporation_id, ESsoRequestType.corporationMarket, request.value.Count);

        //    //        // Удаление предыдущих связей с ордерами
        //    //        _dbContext.EveOnlineMarketOrderRefs.RemoveRange(_dbContext.EveOnlineMarketOrderRefs.Where(x => x.receipt_id == sso.corporation_id));
        //    //        _dbContext.SaveChanges();

        //    //        foreach (var _market_Order in request.value)
        //    //        {
        //    //            // Добавление ордера
        //    //            var db_value = _dbContext.EveOnlineMarketOrders.FirstOrDefault(x => x.order_id == _market_Order.order_id);

        //    //            if (db_value == null)
        //    //            {
        //    //                db_value = new EveOnlineMarketOrder();
        //    //                db_value.UpdateProperties(_market_Order);
        //    //                _dbContext.EveOnlineMarketOrders.Add(db_value);
        //    //            }
        //    //            else
        //    //            {
        //    //                db_value.UpdateProperties(_market_Order);
        //    //                _dbContext.EveOnlineMarketOrders.Update(db_value);
        //    //            }
        //    //            _dbContext.SaveChanges();

        //    //            // Добавление связи
        //    //            EveOnlineMarketOrderRef marketRef = new EveOnlineMarketOrderRef() { receipt_id = sso.corporation_id, marketOrder_id = _market_Order.order_id };
        //    //            _dbContext.EveOnlineMarketOrderRefs.Add(marketRef);
        //    //            _dbContext.SaveChanges();
        //    //        }
        //    //    }

        //    //    // Запрос истории оредеров
        //    //    int prevPage = 0; var isSuccess = false;
        //    //    (CorporationOrdersHistoryResult value, bool success, DateTime expireOn, string message) tempConnectorResult;
        //    //    var ConnectorResult = new CorporationOrdersHistoryResult();
        //    //    do
        //    //    {
        //    //        prevPage++;
        //    //        Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(connector.Corporation.Market.GetOrdersHistory(sso.corporation_id, prevPage).ExecuteAsync);
        //    //        tempConnectorResult = _eveOnlineGeneric.ExecuteRequest<CorporationOrdersHistoryResult>(запросКоннектора1, folder, CorporationOrdersHistoryResult.TimeExpire(), CorporationOrdersHistoryResult.GetArgs(sso.corporation_id)).GetAwaiter().GetResult();
        //    //        if (prevPage == 1) isSuccess = tempConnectorResult.success;

        //    //        if (tempConnectorResult.value?.Count > 0) ConnectorResult.AddRange(tempConnectorResult.value);
        //    //    } while (tempConnectorResult.value?.Count == 1000);

        //    //    if (isSuccess)
        //    //    {
        //    //        _eveOnlineGeneric.Sso_RequestStatistic(sso.corporation_id, ESsoRequestType.corporationMarketHistory, ConnectorResult.Count);

        //    //        foreach (var _market_Order in ConnectorResult)
        //    //        {
        //    //            // Добавление ордера
        //    //            var db_value = _dbContext.EveOnlineMarketOrders.FirstOrDefault(x => x.order_id == _market_Order.order_id);

        //    //            if (db_value == null)
        //    //            {
        //    //                db_value = new EveOnlineMarketOrder();
        //    //                db_value.UpdateProperties(_market_Order);
        //    //                _dbContext.EveOnlineMarketOrders.Add(db_value);
        //    //            }
        //    //            else
        //    //            {
        //    //                db_value.UpdateProperties(_market_Order);
        //    //                _dbContext.EveOnlineMarketOrders.Update(db_value);
        //    //            }
        //    //            _dbContext.SaveChanges();

        //    //            // Добавление связи
        //    //            EveOnlineMarketOrderRef marketRef = new EveOnlineMarketOrderRef() { receipt_id = sso.corporation_id, marketOrder_id = _market_Order.order_id };
        //    //            _dbContext.EveOnlineMarketOrderRefs.Add(marketRef);
        //    //            _dbContext.SaveChanges();
        //    //        }
        //    //    }
        //    //}
        //}
    }
}
