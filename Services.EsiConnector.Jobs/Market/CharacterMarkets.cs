using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterMarketOrders : ConnectorJob
    {
        //static string l_reqName = "Character_Markets";
        //static string l_scope = Scope.Markets.ReadCharacterOrders.Name;
        //static ERequestFolder l_folder = ERequestFolder.Market;
        //public CharacterMarketOrders() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterMarketOrders(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int character_to_update = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = character_to_update;
        //}
        //void MarketOrders(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    var ConnectorResult = SsoOnePage<CharacterOrdersResult, CharacterOrdersResult.CharacterOrdersItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Market.GetOrders, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var db_values = _dbContext.Eveonline_MarketOrderRefs.Where(x => x.receipt_id == sso.character_id)
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
        //                    dbContext.Eveonline_MarketOrderRefs.Add(new EveOnlineMarketOrderRef() { marketOrder_id = _order.order_id, receipt_id = sso.character_id });
        //                };
        //                GenericOperations.UpdateItem(_order, db_values, predicate, newValue, _dbContext, moreAdd, true);
        //            });

        //            dbChanges = _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterMarket, ConnectorResult.items != null ? ConnectorResult.items.Count : 0, dbChanges);
        //}
        //void MarketOrdersHistory(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    var ConnectorResult = SsoPaged<CharacterOrdersHistoryResult, CharacterOrdersHistoryResult.CharacterOrdersHistoryItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Market.GetOrdersHistory, sso.character_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var db_values = _dbContext.Eveonline_MarketOrderRefs.Where(x => x.receipt_id == sso.character_id)
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
        //                    dbContext.Eveonline_MarketOrderRefs.Add(new EveOnlineMarketOrderRef() { marketOrder_id = _order.order_id, receipt_id = sso.character_id });
        //                };
        //                GenericOperations.UpdateItem(_order, db_values, predicate, newValue, _dbContext, moreAdd, true);
        //            });

        //            dbChanges = _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterMarketHistory, ConnectorResult.items != null ? ConnectorResult.items.Count : 0, dbChanges);
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    MarketOrders(sso);
        //    MarketOrdersHistory(sso);

        //    //using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    //{
        //    //    // Запрос оредеров
        //    //    Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Market.GetOrders(sso.character_id).ExecuteAsync);
        //    //    var request = _eveOnlineGeneric.ExecuteRequest<CharacterOrdersResult>(запросКоннектора, folder, CharacterOrdersResult.TimeExpire(), CharacterOrdersResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();
        //    //    _logger.LogInformation($"{reqName}. character {sso.character_id} success = {request.success}. # {request.value?.Count}");

        //    //    if (request.success)
        //    //    {
        //    //        _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterMarket, request.value.Count);

        //    //        // Удаление предыдущих связей с ордерами
        //    //        _dbContext.EveOnlineMarketOrderRefs.RemoveRange(_dbContext.EveOnlineMarketOrderRefs.Where(x => x.receipt_id == sso.character_id));
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
        //    //            EveOnlineMarketOrderRef marketRef = new EveOnlineMarketOrderRef() { receipt_id = sso.character_id, marketOrder_id = _market_Order.order_id };
        //    //            _dbContext.EveOnlineMarketOrderRefs.Add(marketRef);
        //    //            _dbContext.SaveChanges();
        //    //        }
        //    //    }

        //    //    // Запрос истории оредеров
        //    //    int prevPage = 0; var isSuccess = false;
        //    //    (CharacterOrdersHistoryResult value, bool success, DateTime expireOn, string message) tempConnectorResult;
        //    //    var ConnectorResult = new CharacterOrdersHistoryResult();
        //    //    do
        //    //    {
        //    //        prevPage++;
        //    //        Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Market.GetOrdersHistory(sso.character_id, prevPage).ExecuteAsync);
        //    //        tempConnectorResult = _eveOnlineGeneric.ExecuteRequest<CharacterOrdersHistoryResult>(запросКоннектора1, folder, CharacterOrdersHistoryResult.TimeExpire(), CharacterOrdersHistoryResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();
        //    //        if (prevPage == 1) isSuccess = tempConnectorResult.success;

        //    //        if (tempConnectorResult.value?.Count > 0) ConnectorResult.AddRange(tempConnectorResult.value);
        //    //    } while (tempConnectorResult.value?.Count == 1000);

        //    //    if (isSuccess)
        //    //    {
        //    //        _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterMarketHistory, request.value.Count);

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
        //    //            EveOnlineMarketOrderRef marketRef = new EveOnlineMarketOrderRef() { receipt_id = sso.character_id, marketOrder_id = _market_Order.order_id };
        //    //            _dbContext.EveOnlineMarketOrderRefs.Add(marketRef);
        //    //            _dbContext.SaveChanges();
        //    //        }
        //    //    }
        //    //}
        //}
    }
}
