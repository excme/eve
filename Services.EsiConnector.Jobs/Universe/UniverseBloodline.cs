using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Databases.Contexts.Public.Models;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class UniverseBloodline : ConnectorJob
    {
        public UniverseBloodline(IReadWrite repoPublicCommon, ILogger<UniverseBloodline> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            var multLangProperties = new List<string>() { "name", "description" };
            var repoListRequest = _repoPublicCommon.Universe_Bloodlines_Ids();
            Func<UniverseBloodlinesResult.UniverseBloodlinesItem, int> selector = x => x.bloodline_id;

            var blooflines_to_add = UniverseGeneric.MakeListUpdate<UniverseBloodlinesResult, EveOnlineUniverseBloodLine, UniverseBloodlinesResult.UniverseBloodlinesItem>(
                repoListRequest,
                esiClient.Universe.Bloodlines,
                multLangProperties
                );

            _jobResult.Value = _repoPublicCommon.Universe_Bloodlines_AddOrUpdate(blooflines_to_add);

            
        }
    }
}
