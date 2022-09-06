using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared;
using eveDirect.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.ReadWrite.IntegrationEvents;

namespace eveDirect.Repo.ReadWrite
{
    public partial class ReadWriteRepo : IReadWrite
    {
        public void Market_UpdateActualOrders(int region_id, List<MarketsOrdersResult.MarketsOrdersItem> results)
        {
            int r = 0;
            using var _context = new PublicContext(_options);

            // Изменение статуса на неактуальные ордера
            var new_orders = results.Select(x => x.order_id);
            var db_actual = _context.Eveonline_MarketOrders
                .Where(x => !x.disabled && x.region_id == region_id)
                .Select(x => x.order_id).ToList();
            var to_disable = db_actual.Except(new_orders).ToList();
            if (to_disable?.Any() ?? false)
            {
                //var vals = to_disable.Select(x => new EveOnlineMarketOrder { order_id = x, disabled = true }).ToList();
                //_context.Eveonline_MarketOrders.AttachRange(vals);
                //vals.ForEach(x => _context.Entry(x).Property(p => p.disabled).IsModified = true);
                //r += _context.SaveChanges();

                _context.Eveonline_MarketOrders
                    .Where(x => to_disable.Contains(x.order_id))
                    .UpdateFromQuery(x => new EveOnlineMarketOrder() { disabled = true });

                // Уведомление подписчиков
                to_disable.ForEach(order_id =>
                {
                    var @event = new MarketOrderAfterDisableStatusIntegrationEvent(order_id);
                    _eventBus.Publish(@event);
                });
            }

            // Добавление новых
            var to_add = new_orders.Except(db_actual).ToList();
            if (to_add?.Any() ?? false)
            {
                // Ордера, которые были деактивированы
                var db_disabled = _context.Eveonline_MarketOrders
                    .Where(x => x.region_id == region_id && x.disabled && to_add.Contains(x.order_id))
                    .Select(x => x.order_id)
                    .ToList();
                if(db_disabled?.Any() ?? false)
                {
                    //var vals = db_disabled.Select(x => new EveOnlineMarketOrder { order_id = x, disabled = false }).ToList();
                    //_context.Eveonline_MarketOrders.AttachRange(vals);
                    //vals.ForEach(x => _context.Entry(x).Property(p => p.disabled).IsModified = true);

                    _context.Eveonline_MarketOrders
                        .Where(x => db_disabled.Contains(x.order_id))
                        .UpdateFromQuery(x => new EveOnlineMarketOrder() { disabled = false });
                }

                to_add = to_add.Except(db_disabled).ToList();
                var db_to_add = results
                    .Where(xx => to_add.Contains(xx.order_id))
                    .Select(x => new EveOnlineMarketOrder(x) { region_id = region_id });
                //_context.Eveonline_MarketOrders.AddRange(db_to_add);
                //r += _context.SaveChanges();
                _context.Eveonline_MarketOrders
                    .BulkInsert(db_to_add, options =>
                    {
                        options.InsertKeepIdentity = true;
                        options.AutoMapOutputDirection = false;
                    });

                // Уведомление подписчиков
                to_add.ForEach(order_id =>
                {
                    var @event = new MarketOrderAfterAddIntegrationEvent(order_id);
                    _eventBus.Publish(@event);
                });
                _eventBus.Publish(new MarketOrderCountAddedIntegrationEvent(DateTime.UtcNow, to_add.Count));
            }
        }
        public int Market_HistoryPrices(int region_id, int type_id, List<MarketsHistoryResult.MarketsHistoryItem> data)
        {
            if (data?.Any() ?? false)
            {
                using var _eveOnlinePublicContext = new PublicContext(_options);
                var min_date = data.OrderBy(x => x.date).First().date;
                List<EveOnlineMarketRegionHistoryPriceInfo> historyPrices = _eveOnlinePublicContext.Eveonline_MarketHistoryPrices.Where(x => x.region_id == region_id && x.type_id == type_id && x.date >= min_date).ToList();
                var collectionSpecDic = new Dictionary<Type, IEnumerable<string>>();
                collectionSpecDic.Add(typeof(MarketsHistoryResult.MarketsHistoryItem), new List<string>() { "date" });

                var compare = historyPrices.UpdateProperties_NonBased(data, false, collectionSpec: collectionSpecDic);
                if (!compare.AreEqual)
                {
                    if(compare.Differences?.Any(x => x.ChildPropertyName == "Item-toAdd") ?? false)
                    {
                        var toAdd = compare.Differences.Where(x => x.ChildPropertyName == "Item-toAdd").Select(x => new EveOnlineMarketRegionHistoryPriceInfo((MarketsHistoryResult.MarketsHistoryItem)x.Object2) { region_id = region_id, type_id = type_id });
                        _eveOnlinePublicContext.Eveonline_MarketHistoryPrices.AddRange(toAdd);
                        return _eveOnlinePublicContext.SaveChanges();
                    }


                    //foreach (var compareItem in compare.Differences)
                    //{
                    //    // Добавление
                    //    if (compareItem.ChildPropertyName == "Item-toAdd")
                    //    {
                    //        var itemToAdd = new EveOnlineMarketRegionHistoryPriceInfo((MarketsHistoryResult.MarketsHistoryItem)compareItem.Object2) { region_id = region_id, type_id = type_id };
                    //        await _eveOnlinePublicContext.Eveonline_MarketHistoryPrices.AddAsync(itemToAdd);
                    //    }
                    //    // Обновление записи
                    //    //else if(compareItem.ChildPropertyName != "Count" && compareItem.ChildPropertyName != "Item-toRemove")
                    //    //{
                    //    //    EveOnlineMarketRegionHistoryPriceInfo dbItem = historyPrices.FirstOrDefault(x => x.date == ((EveOnlineMarketRegionHistoryPriceInfo)compareItem.ParentObject1).date);
                    //    //    if (dbItem != null && compareItem.ParentObject2 != null)
                    //    //    {
                    //    //        dbItem.UpdateValues((MarketsHistoryResult.MarketsHistoryItem)compareItem.ParentObject2);
                    //    //        _eveOnlinePublicContext.Eveonline_MarketHistoryPrices.Update(dbItem);
                    //    //    }
                    //    //}
                    //}

                    //_eveOnlinePublicContext.SaveChanges();
                }
            }

            return 0;
        }
        public double Market_HistoryPrice(int type_id, DateTime date) {
            using var _eveOnlinePublicContext = new PublicContext(_options);

            // Если имущетсво не опубликовано
            if (Universe_Types_IsPublished(type_id))
            {
                //var query = _eveOnlinePublicContext.Eveonline_MarketHistoryPrices.OrderBy(i => i.date > date ? i.date - date : date - i.date);
                //var sql = query.ToSql();

                // Сначала поиск в регионе 10000002
                var hPrice = _eveOnlinePublicContext.Eveonline_MarketHistoryPrices
                    .Select(x => new { x.region_id, x.date, x.average, x.type_id })
                    .Where(x => x.region_id == 10000002 && x.type_id == type_id /*&& x.date >= date*/)
                    //.OrderBy(x => x.date)
                    .OrderBy(i => i.date > date ? i.date - date : date - i.date)
                    .Take(1)
                    .FirstOrDefault();

                //var sql2 = hPriceQuery.ToSql();
                //var hPrice = await hPriceQuery.FirstOrDefaultAsync();
                if (hPrice != null)
                    return hPrice.average;

                hPrice = _eveOnlinePublicContext.Eveonline_MarketHistoryPrices
                    .Select(x => new { x.region_id, x.date, x.average, x.type_id })
                    .Where(x => x.type_id == type_id/* && x.date >= date*/)
                    //.OrderBy(x => x.date)
                    .OrderBy(i => i.date > date ? i.date - date : date - i.date)
                    .Take(1)
                    .FirstOrDefault();

                if (hPrice != null)
                    return hPrice.average;
            }

            return 0.01;
        }
        public List<int> Market_Group_Ids(Expression<Func<EveOnlineMarketGroup, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.Eveonline_MarketGroups.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return source.Select(x => x.market_group_id).ToList();
        }
        public List<int> Market_Groups_AddOrUpdate(List<EveOnlineMarketGroup> marketGroups)
        {
            List<int> _groupsToCalcChilds = new List<int>();
            using var _eveOnlinePublicContext = new PublicContext(_options);
            foreach (EveOnlineMarketGroup marketGroup in marketGroups)
            {
                var db_marketGroup = _eveOnlinePublicContext.Eveonline_MarketGroups.FirstOrDefault(x => x.market_group_id == marketGroup.market_group_id);

                if (db_marketGroup == null)
                {
                    _groupsToCalcChilds.Add(marketGroup.market_group_id);
                    _groupsToCalcChilds.Add(marketGroup.parent_group_id);
                    _eveOnlinePublicContext.Eveonline_MarketGroups.Add(marketGroup);
                }
                else
                {
                    var r = db_marketGroup.UpdateProperties(marketGroup);
                    if (!r.AreEqual)
                    {
                        _groupsToCalcChilds.Add(marketGroup.market_group_id);
                        _groupsToCalcChilds.Add(marketGroup.parent_group_id);
                        _eveOnlinePublicContext.Eveonline_MarketGroups.Update(db_marketGroup);
                    }
                }
            }

            _eveOnlinePublicContext.SaveChanges();
            return _groupsToCalcChilds;
        }
        public int Market_Groups_CalcChilds(int group_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            EveOnlineMarketGroup market_group = _eveOnlinePublicContext.Eveonline_MarketGroups.FirstOrDefault(x => x.market_group_id == group_id);
            if(market_group != null)
            {
                List<int> childs = _eveOnlinePublicContext.Eveonline_MarketGroups.Select(x => new { x.market_group_id, x.parent_group_id }).Where(x => x.parent_group_id == group_id).Select(x => x.market_group_id).ToList();
                if(childs?.Any() ?? false)
                {
                    market_group.childs = childs;

                    _eveOnlinePublicContext.Entry(market_group).Property(p => p.childs).IsModified = true;
                    return _eveOnlinePublicContext.SaveChanges();
                }
            }

            return 0;
        }
    }
}
;