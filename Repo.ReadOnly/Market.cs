using eveDirect.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using eveDirect.Repo.PublicReadOnly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.BaseRepo;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Databases.Contexts;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly
    {
        public async Task<LoadResult> Market_Orders(int type_id, bool is_buy, int[] systems, DataSourceLoadOptionsBase options)
        {
            //IQueryable<MarketOrder> query = default; List<MarketOrder> orders = default;
            using var context = new PublicContext(_options);

            IQueryable<EveOnlineMarketOrder> query1 = context.Eveonline_MarketOrders
                .AsNoTracking()
                .Where(x => !x.disabled && x.is_buy_order == is_buy);

            if(type_id > 0)
                query1 = query1.Where(x => x.type_id == type_id);

            if (systems?.Any() ?? false)
                query1 = query1.Where(x => systems.Contains(x.system_id));

            return await DataSourceLoader.LoadAsync(query1.Select(x => new MarketOrderModel()
            {
                i = x.order_id,
                iss = x.issued,
                p = x.price,
                vr = x.volume_remain,
                vt = x.volume_total,
                vm = x.min_volume,
                d = x.duration,
                li = x.location_id,
            }), options);
        }

        public async Task<IdRanges> Orders_IdRanges()
        {
            return await Task.Run(() => order_IdRanges);
        }

        public async Task Orders_CalcIdRanges()
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var order_ids = await _eveOnlinePublicContext.Eveonline_MarketOrders.Select(x => x.order_id).ToListAsync();
            order_IdRanges = Generic_CalcRange(order_ids);
        }

        public async Task<List<MarketRegionModel>> Market_AllRegionsAndSystems()
        {
            // Выбор регионов и внутренних систем
            using var context = new PublicContext(_options);

            var regions_and_systems = await context.EveOnlineUniverseLocations
                .Where(x =>
                    (x.type == EUniverseLocationType.Region && MarketRegionsRange.GetList().Contains(x.id)) ||
                    (x.type == EUniverseLocationType.System && x.region_id.HasValue && MarketRegionsRange.GetList().Contains(x.region_id.Value))
                )
                .Select(x => new { x.type, x.id, x.region_id, x.name })
                .ToListAsync();

            List<MarketRegionModel> grouped = regions_and_systems
                .Where(x => x.type == EUniverseLocationType.Region)
                .Select(x => new MarketRegionModel()
                {
                    name = x.name,
                    id = x.id,
                    systems = regions_and_systems
                        .Where(xx => xx.region_id == x.id)
                        .Select(xx => new NameModel<long>() { id = xx.id, name = xx.name })
                        .ToList()
                }).ToList();

            return grouped;
        }

        public async Task<List<NameModel<long>>> Market_AllRegions()
        {
            // Выбор регионов и внутренних систем
            using var context = new PublicContext(_options);

            var regions_and_systems = await context.EveOnlineUniverseLocations
                .Where(x => x.type == EUniverseLocationType.Region /*&& MarketRegionsRange.GetList().Contains(x.id)*/)
                .Select(x => new { x.type, x.id, x.region_id, x.name })
                .ToListAsync();

            var grouped = regions_and_systems
                .Where(x => x.type == EUniverseLocationType.Region)
                .Select(x => new NameModel<long>()
                {
                    id = x.id,
                    name = x.name,
                }).ToList();

            return grouped;
        }

        public async Task<List<int>> Market_ActiveOrdersRegionsAndSystems()
        {
            using var context = new PublicContext(_options);

            // Запрашиваем все регионы и системы текущих ордеров
            var systems_and_regions = await context.Eveonline_MarketOrders.Where(x => x.disabled == false).Select(x => new { x.system_id, x.region_id }).ToListAsync();

            var location_ids = systems_and_regions.Select(x => x.system_id).ToList();
            location_ids.AddRange(systems_and_regions.Select(x => x.region_id).ToList() ?? new List<int>());

            return location_ids.Distinct().ToList();

            //systems_and_regions = systems_and_regions.GroupBy(x => x.system_id).Select(x => x.FirstOrDefault()).ToList();
            //var all_ids = systems_and_regions.Select(x => x.system_id).ToList();
            //all_ids.AddRange(systems_and_regions.Select(x => x.region_id).Distinct());
            //List<NameModel> all_names = await context.EveOnlineUniverseLocations.Where(x => x.type == EUniverseLocationType.Region || x.type == EUniverseLocationType.System).Where(x => all_ids.Contains(x.id)).Select(x => new NameModel() { id = x.id, name = x.name }).ToListAsync();
            //// Прикрепляем имена
            //var temp = systems_and_regions
            //    .Join(all_names,
            //        s => s.system_id,
            //        n => n.id,
            //        (s, n) => new { s, n }
            //    )
            //    .Select(x => new {
            //        x.s.region_id,
            //        x.s.system_id,
            //        system_name = x.n.name
            //    })
            //    .Join(all_names,
            //        r => r.region_id,
            //        n => n.id,
            //        (r, n) => new { r, n }
            //    )
            //    .Select(x => new {
            //        x.r.region_id,
            //        region_name = x.n.name,
            //        x.r.system_id,
            //        x.r.system_name,
            //    });
            //var market_regions_and_systems = temp.GroupBy(x => x.region_id).Select(x => new MarketRegionModel() { 
            //    id = x.Key,
            //    name = x.First().region_name,
            //    systems = x.Select(xx => new NameModel() { id = xx.system_id, name = xx.system_name }).ToList()
            //}).ToList();

            //return market_regions_and_systems;
        }
        public async Task<List<MarketGroupModel>> Market_Groups(string lang)
        {
            using var context = new PublicContext(_options);

            var r = await context.Eveonline_MarketGroups
                .Select(x => new MarketGroupModel()
                {
                    id = x.market_group_id + 1000000,
                    parent = x.parent_group_id == 0 ? 0 : x.parent_group_id + 1000000,
                    name = EF.Property<string>(x, lang + "name")
                })
                .ToListAsync();
            var types = await context.Eveonline_UniverseTypes
                .Where(x => x.published && x.market_group_id > 0)
                .Select(x => new MarketGroupModel()
                {
                    id = x.type_id,
                    //name = EF.Property<string>(x, lang + "name"),
                    parent = x.market_group_id + 1000000
                })
                .ToListAsync();

            return r.Concat(types).ToList();
        }

        //public async Task<List<MarketGroupModel>> Market_Groups(string lang)
        //{
        //    using var context = new PublicContext(_options);

        //    var market_groups = await context.Eveonline_MarketGroups
        //        .Select(x => new MarketGroup() { id = x.market_group_id, parent_group_id = x.parent_group_id, childs = x.childs, types = x.types, name = EF.Property<string>(x, lang + "name") })
        //        .ToListAsync();

        //    List<NameModel<int>> types = await context.Eveonline_UniverseTypes
        //        .Where(x => x.published)
        //        .Select(x => new NameModel<int>() { id = x.type_id, name = EF.Property<string>(x, lang + "name") })
        //        .ToListAsync();

        //    if (market_groups?.Any() ?? false)
        //    {
        //        List<MarketGroupModel> marketGroupModels = new List<MarketGroupModel>();
        //        foreach (var i in market_groups.Where(x => x.parent_group_id == 0).Select(x => x.id).ToList())
        //        {
        //            marketGroupModels.Add(get_childs(market_groups, i, types, lang));
        //        }

        //        return marketGroupModels;
        //    }

        //    return default;
        //}
        //MarketGroupModel get_childs(List<MarketGroup> db_groups, int item_id, List<NameModel<int>> types, string lang)
        //{
        //    MarketGroup item = db_groups.First(x => x.id == item_id);
        //    MarketGroupModel i = new MarketGroupModel()
        //    {
        //        name = item.name,
        //        id = item.id,
        //    };
        //    if (item.types?.Any() ?? false)
        //    {
        //        i.childs = types
        //            .Where(x => item.types.Contains(x.id))
        //            .Select(x => new MarketGroupModel() { id = x.id, name = x.name, lastLevel = true })
        //            .ToList();
        //    }
        //    else if (item.childs?.Any() ?? false)
        //    {
        //        i.childs = item.childs?.Select(x => get_childs(db_groups, x, types, lang)).ToList() ?? default;
        //    }

        //    return i;
        //}
    }
    class MarketGroup
    {
        public int id { get; set; }
        public string name { get; set; }
        public int parent_group_id { get; set; }
        public List<int> childs { get; set; }
        public List<int> types { get; set; }
    }
}
