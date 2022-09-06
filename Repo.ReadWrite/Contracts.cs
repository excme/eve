using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Databases;
using eveDirect.Shared.Helper;
using Microsoft.EntityFrameworkCore;
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
        public List<EveOnlineContract> Contracts_Get(
            Expression<Func<EveOnlineContract, bool>> where = null,
            Expression<Func<EveOnlineContract, EveOnlineContract>> select = null
        ){
            using var context = new PublicContext(_options);
            var source = context.Eveonline_Contracts.AsNoTracking().AsQueryable();

            if (where != null)
                source = source.Where(where);

            if (select != null)
                source = source.Select(select);

            return source.ToList();
        }
        public void Contracts_Public_Update(List<EveOnlineContract> to_update)
        {
            using var context = new PublicContext(_options);
            context.Eveonline_Contracts.UpdateRange(to_update);
            context.SaveChanges();
        }
        public List<int> Contracts_Public_Update(int region_id, List<ContractsResult.Contract> results)
        {
            var cur_contracts = results.Select(x => x.contract_id);

            using var context = new PublicContext(_options);

            // Текущие в базе
            var db_all = context.Eveonline_Contracts
                .Where(x => x.region_id == region_id)
                .Select(x => new { x.contract_id, x.actual })
                .ToList();

            // Измнение на неактуальные
            var to_disable = db_all
                .Where(x => x.actual)
                .Select(x => x.contract_id)
                .Except(cur_contracts)
                .ToList();

            if (to_disable?.Any() ?? false)
            {
                var vals = to_disable
                    .Select(x => new EveOnlineContract { contract_id = x, actual = false })
                    .ToList();
                context.Eveonline_Contracts.AttachRange(vals);
                vals.ForEach(x => context.Entry(x).Property(p => p.actual).IsModified = true);
                context.SaveChanges();

                // Уведомление подписчиков
                to_disable.ForEach(contract_id =>
                {
                    var @event = new ContractChangeStatusIntegrationEvent(contract_id);
                    _eventBus.Publish(@event);
                });
            }

            // Изменение на актуальные
            var to_actual = db_all
                .Where(x => !x.actual && cur_contracts.Contains(x.contract_id))
                .Select(x => x.contract_id)
                .ToList();

            if (to_actual?.Any() ?? false)
            {
                var vals = to_actual.Select(x => new EveOnlineContract { contract_id = x, actual = true }).ToList();
                context.Eveonline_Contracts.AttachRange(vals);
                vals.ForEach(x => context.Entry(x).Property(p => p.actual).IsModified = true);
                context.SaveChanges();

                // Уведомление подписчиков
                to_disable.ForEach(contract_id =>
                {
                    var @event = new ContractChangeStatusIntegrationEvent(contract_id);
                    _eventBus.Publish(@event);
                });
            }

            // На Добавление
            var to_add = cur_contracts
                .Except(db_all.Select(x => x.contract_id))
                .ToList();
            if (to_add?.Any() ?? false)
            {
                var db_to_add = results
                    .Where(xx => to_add.Contains(xx.contract_id))
                    .Select(x => new EveOnlineContract(x) { region_id = region_id, actual = true });

                context.Eveonline_Contracts.AddRange(db_to_add);
                context.SaveChanges();

                // Уведомление подписчиков
                to_add.ForEach(contract_id =>
                {
                    var contract_data = results.FirstOrDefault(x => x.contract_id == contract_id);
                    if (contract_data != null)
                    {
                        // Rabbit
                        var @event = new ContractAddNewIntegrationEvent(contract_id,
                            region_id,
                            (byte)contract_data.type,
                            contract_data.volume,
                            contract_data.date_issued);
                        _eventBus.Publish(@event);
                    }
                });
            }

            return to_add;
        }
        public void Contracts_PublicBids_Update(int contract_id, List<ContractsBidsResult.ContractsBidsItem> results)
        {
            using var context = new PublicContext(_options);

            // Изменение статуса на неактуальные 
            var new_bids = results.Select(x => x.bid_id);
            var db_actual = context.Eveonline_ContractBids.Where(x => !x.isDisable && x.contract_id == contract_id).Select(x => x.bid_id).ToList();
            var to_disable = db_actual.Except(new_bids).ToList();
            if (to_disable?.Any() ?? false)
            {
                var vals = to_disable.Select(x => new EveOnlineContractBid { contract_id = x, isDisable = true }).ToList();
                context.Eveonline_ContractBids.AttachRange(vals);
                vals.ForEach(x => context.Entry(x).Property(p => p.isDisable).IsModified = true);
                context.SaveChanges();

                // Уведомление подписчиков
                to_disable.ForEach(bid_id =>
                {
                    var @event = new ContractBidChangeStatusIntegrationEvent(contract_id, bid_id);
                    _eventBus.Publish(@event);
                });
            }

            // Добавление новых
            var to_add = new_bids.Except(db_actual).ToList();
            if (to_add?.Any() ?? false)
            {
                var db_to_add = results.Where(xx => to_add.Contains(xx.bid_id)).Select(x => new EveOnlineContractBid(x) { contract_id = contract_id });
                context.Eveonline_ContractBids.AddRange(db_to_add);
                context.SaveChanges();

                // Уведомление подписчиков
                to_add.ForEach(bid_id =>
                {
                    var @event = new ContractBidAddNewIntegrationEvent(contract_id, bid_id);
                    _eventBus.Publish(@event);
                });
            }
        }
        public void Contracts_PublicItems_Update(int contract_id, List<ContractsItemsResult.ContractsItem> results) {
            using var context = new PublicContext(_options);

            // Изменение статуса на неактуальные 
            var new_items = results.Select(x => x.item_id);
            var db_actual = context.Eveonline_ContractItems.Where(x => /*!x.isDisable && */x.contract_id == contract_id).Select(x => x.item_id).ToList();

            // Добавление новых
            var to_add = new_items.Except(db_actual).ToList();
            if (to_add?.Any() ?? false)
            {
                var db_to_add = results.Where(xx => to_add.Contains(xx.item_id)).Select(x => new EveOnlineContractItem(x) { contract_id = contract_id });
                context.Eveonline_ContractItems.AddRange(db_to_add);
                context.SaveChanges();

                // Уведомление подписчиков
                to_add.ForEach(item_id =>
                {
                    var @event = new ContractItemAddNewIntegrationEvent(contract_id, item_id);
                    _eventBus.Publish(@event);
                });
            }

        }
    }
}
