using eveDirect.Repo.ReadWrite;
using eveDirect.Services.EsiConnector;
using eveDirect.Services.Jobs.Core;
using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.Helper;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Services.Jobs.Market
{
    /// <summary>
    /// Сбор текущих ордеров рынков по регионам
    /// </summary>
    public class MarketActualOrders : ConnectorJob
    {
        public MarketActualOrders(IReadWrite repoPublicCommon, ILogger<MarketActualOrders> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }

        public override void Execute()
        {
            var region_ids = MarketRegionsRange.GetList();

            var list = AttachProgressBarToList(region_ids);
            Parallel.ForEach(list, region_id =>
            {
                SimpleRegion(region_id.ToInt32());
            });
        }
        public void SimpleRegion(int region_id)
        {
            var requests = EsiConnector_AutoPaging(esiClient.Market.RegionOrders, region_id, MarketOrderType.All, default(int?));

            if (requests != null)
            {
                var results = requests.Where(x => x.isSuccess).SelectMany(x => x.Data).ToList();
                _jobResult.subValues.Add(new JobResult.Item() { Name = $"A{region_id}", Value = results.Count });

                _repoPublicCommon.Market_UpdateActualOrders(region_id, results);

                ToConsole($"{region_id} -> {results.Count}");
                _jobResult.subValues.Add(new JobResult.Item() { Name = $"{region_id}", Value = results.Count });
            }
        }
    }
}
