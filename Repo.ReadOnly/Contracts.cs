using eveDirect.Databases.Contexts;
using eveDirect.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using eveDirect.Repo.PublicReadOnly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.BaseRepo;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly
    {
        public async Task<LoadResult> Contracts_List(int type_id, int[] regions, DataSourceLoadOptionsBase lo)
        {
            IQueryable<ContractData> query = default; List<ContractData> contracts = default;
            using var context = new PublicContext(_options);
   //         if (!_cache.Get(string.Format(CacheKeys.ApiMarketContractsList), out contracts))
    //        {
                query = context.Eveonline_Contracts
                    .AsNoTracking()
                    .Where(x => x.actual)
                    .Select(x => new ContractData()
                    {
                        a = x.title,
                        i = x.contract_id,
                        l = x.start_location_id,
                        p = x.price,
                        b = x.buyout,
                        r = x.reward,
                        c = x.collateral,
                        v = x.volume,
                        t = (int)x.type,
                        k = x.issuer_id,
                        s = x.date_issued,
                        // TODO: когда jobs сами будут вчислять значение, это условие нужно сократить
                        d = x.duration_days > 0 ? x.duration_days : (x.date_expired - x.date_issued).Value.TotalDays.ToInt32(),
                        types = x.items.Select(x => x.type_id),
                        region_id = x.region_id
                    });


            if(type_id > 0)
                query = query.Where(x => x.types.Contains(type_id));

            if (regions?.Any() ?? false)
                query = query.Where(x => regions.Contains(x.region_id));

            return await DataSourceLoader.LoadAsync(query
                .Select(data => new ContractModel()
                {
                    i = data.i,
                    p = data.p,
                    v = data.v,
                    t = data.t,
                    a = data.a,
                    l = data.l,
                    b = data.b,
                    r = data.r,
                    c = data.c,
                    k = data.k,
                    s = data.s,
                    d = data.d
                }), lo);
        }
        public async Task<ContractDetail> Contracts_Detail(int contract_id)
        {
            using var context = new PublicContext(_options);
            var p = await context.Eveonline_Contracts.FirstOrDefaultAsync(x => x.contract_id == contract_id);
            if (p != null)
                return new ContractDetail() {
                    buyout = p.buyout,
                    collateral = p.collateral,
                    days_to_complete = p.days_to_complete,
                    start_location_id=p.start_location_id,
                    date_expired = p.date_expired,
                    date_issued = p.date_issued,
                    end_location_id = p.end_location_id,
                    for_corporation = p.for_corporation,
                    issuer_corporation_id = p.issuer_corporation_id,
                    issuer_id = p.issuer_id,
                    price = p.price,
                    reward = p.reward,
                    title = p.title,
                    type = (byte)p.type,
                    volume = p.volume
                };
            return default;
        }
        public async Task<List<ContractBid>> Contracts_Bids(int contract_id)
        {
            using var context = new PublicContext(_options);
            return await context.Eveonline_ContractBids
                .Where(x => x.contract_id == contract_id /*&& !x.isDisable*/)
                .Select(x => new ContractBid()
                {
                    date_bid = x.date_bid,
                    amount = x.amount,
                    //bid_id = x.bid_id,
                    disabled = x.isDisable
                })
                .ToListAsync();
        }
        public async Task<List<ContractItem>> Contracts_Items(int contract_id)
        {
            using var context = new PublicContext(_options);
            return await context.Eveonline_ContractItems
                .Where(x => x.contract_id == contract_id)
                .Select(data => new ContractItem()
                {
                    is_blueprint_copy = data.is_blueprint_copy,
                    item_id = data.item_id,
                    material_efficiency = data.material_efficiency,
                    runs = data.runs,
                    time_efficiency = data.time_efficiency,
                    record_id = data.record_id,
                    quantity = data.quantity,
                    type_id = data.type_id,
                    is_included = data.is_included,
                })
                .ToListAsync();
        }

        public async Task<IdRanges> Contracts_IdRanges()
        {
            return await Task.Run(() => contract_IdRanges);
        }

        /// <summary>
        /// Расчет диапозов ид контрактов
        /// </summary>
        public async Task Contracts_CalcIdRanges()
        {
            await using var context = new PublicContext(_options);
            var contractIds = await context.Eveonline_Contracts.Select(x => x.contract_id).ToListAsync();
            contract_IdRanges = Generic_CalcRange(contractIds.Select(x => x.ToInt64()).ToList());
        }
    }
}
