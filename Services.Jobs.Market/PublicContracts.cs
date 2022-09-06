using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.Shared.Helper;
using eveDirect.Services.EsiConnector;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Services.Jobs.Core;
using System.Collections.Generic;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Services.Jobs.Market
{
    /// <summary>
    /// Сбор контрактов по регионам
    /// </summary>
    public class PublicContracts : ConnectorJob
    {
        public PublicContracts(IReadWrite repoPublicCommon, ILogger<PublicContracts> logger)
            :base(logger)
        {
            _repoPublicCommon = repoPublicCommon
               ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }

        public override void Execute()
        {
            var region_ids = MarketRegionsRange.GetList();

            var list = AttachProgressBarToList(region_ids);
            //await list.ParallelForEachAsync(async region_id =>
            //foreach (var region_id in list)
            Parallel.ForEach(list, region_id =>
            {
                SimpleRegion(region_id);
            });
        }
        public void SimpleRegion(long region_id)
        {
            var requests = EsiConnector_AutoPaging(esiClient.Contracts.Contracts, region_id.ToInt32());
            if (requests != null)
            {
                var active_contracts = requests.Where(x => x.isSuccess).SelectMany(x => x.Data).ToList();
                ToConsole($"{region_id} -> {active_contracts.Count}");
                _jobResult.subValues.Add(new JobResult.Item() { Name = $"A{region_id}", Value = active_contracts.Count });

                // Описание
                List<int> new_contracts = _repoPublicCommon.Contracts_Public_Update(region_id.ToInt32(), active_contracts);
                _jobResult.subValues.Add(new JobResult.Item() { Name = $"{region_id}", Value = new_contracts.Count });
                _jobResult.Value += new_contracts.Count;

                var to_update_items = active_contracts.Where(x => x.type == ContractsResult.EType.item_exchange || x.type == ContractsResult.EType.auction).Select(x => x.contract_id).Where(x => new_contracts.Contains(x)).ToList();
                if (to_update_items?.Any() ?? false)
                    UpdateItems(to_update_items.ToArray());

                UpdateBids(active_contracts.Where(x => x.type == ContractsResult.EType.auction).Select(x => x.contract_id).ToArray());
            }
        }
        public void UpdateItems(params int[] contract_ids)
        {
            //await contract_ids.ParallelForEachAsync(async contract_id =>
            Parallel.ForEach(contract_ids, contract_id =>
            {
                // Items
                var items_request = EsiConnector_AutoPaging(esiClient.Contracts.ContractItems, contract_id);
                if (items_request != null)
                    _repoPublicCommon.Contracts_PublicItems_Update(contract_id, items_request.Where(x => x.isSuccess).SelectMany(x => x.Data).ToList());
            });
        }
        public void UpdateBids(params int[] contract_ids)
        {
            //await contract_ids.ParallelForEachAsync(async contract_id =>
            Parallel.ForEach(contract_ids, contract_id =>
            {
                // Bids
                var bids_request = EsiConnector_AutoPaging(esiClient.Contracts.ContractBids, contract_id);
                if (bids_request != null && bids_request.Any(x => x.isSuccess))
                    _repoPublicCommon.Contracts_PublicBids_Update(contract_id, bids_request.Where(x => x.isSuccess).SelectMany(x => x.Data).ToList());
            });
        }
    }
}
