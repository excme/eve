using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Services.EsiConnector.Jobs;
using eveDirect.Repo.ReadWrite;
using eveDirect.Services.EsiConnector;
using eveDirect.Shared.EsiConnector.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Services.Jobs.Market
{
    public class MarketGroups : ConnectorJob
    {
        public MarketGroups(IReadWrite repoPublicCommon, ILogger<MarketGroups> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon
               ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }

        public override void Execute()
        {
           

            var multLangProperties = new List<string>() { "name", "description" };
            List<int> repoListRequest = _repoPublicCommon.Market_Group_Ids();
            Func<MarketsGroupInfoResult, int> selector = x => x.market_group_id;

            // Заказчка 
            var marketGroups_to_add = UniverseGeneric.MakeListUpdate<MarketsGroupsResult, EveOnlineMarketGroup, MarketsGroupInfoResult>(
                repoListRequest,
                esiClient.Market.Groups,
                multLangProperties,
                esiClient.Market.Group
                );

            var _newGroups = _repoPublicCommon.Market_Groups_AddOrUpdate(marketGroups_to_add);
            if (_newGroups?.Any() ?? false)
            {
                var list = AttachProgressBarToList(_newGroups);
                // Особенность является то, что после добавления необходимо пересчитать все дочерние элементы
                //await list.ParallelForEachAsync(async group_id => 
                Parallel.ForEach(list, group_id =>
                { 
                    _jobResult.Value += _repoPublicCommon.Market_Groups_CalcChilds(group_id);
                });
            }

            
        }
    }
}
