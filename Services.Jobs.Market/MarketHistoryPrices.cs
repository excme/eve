using eveDirect.Shared.Helper;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.Repo.ReadWrite;
using eveDirect.Services.EsiConnector;
using eveDirect.Services.Jobs.Core;

namespace eveDirect.Services.Jobs.Market
{
    /// <summary>
    /// Сбор исторических цен рынков по регионам
    /// </summary>
    public class MarketHistoryPrices : ConnectorJob
    {
        public MarketHistoryPrices(IReadWrite repoPublicCommon, ILogger<MarketHistoryPrices> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }

        public override void Execute()
        {
            var typeIds = _repoPublicCommon.Universe_Types_Ids(where: x => x.published);
            var region_ids = MarketRegionsRange.GetList();

            if ((region_ids?.Any() ?? false) && (typeIds?.Any() ?? false))
            {
                var list = AttachProgressBarToList(region_ids);
                //await list.ParallelForEachAsync(async region_id =>
                Parallel.ForEach(list, region_id =>
                {
                    int r = 0;
                    ToConsole($"S: {region_id}");
                    //await typeIds.ParallelForEachAsync(async type_id =>
                    Parallel.ForEach(typeIds, type_id =>
                    {
                        r += SimpleUpdate(region_id, type_id);
                    });

                    _jobResult.subValues.Add(new JobResult.Item() { Name = $"{region_id}", Value = r });
                    ToConsole($"F: {region_id} / {r}");
                });
            }

            
        }
        public int SimpleUpdate(long region_id, int type_id)
        {
            var request = EsiConnector(esiClient.Market.TypeHistoryInRegion, region_id.ToInt32(), type_id);
            
            if (request.isSuccess)
                return _repoPublicCommon.Market_HistoryPrices(region_id.ToInt32(), type_id, request.Data);
            
            return 0;
        }
    }
}
