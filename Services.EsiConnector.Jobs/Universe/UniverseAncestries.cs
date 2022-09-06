using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Databases.Contexts.Public.Models;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class UniverseAncestries : ConnectorJob
    {
        public UniverseAncestries(IReadWrite repoPublicCommon, ILogger<UniverseAncestries> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            var multLangProperties = new List<string>() { "name", "description"/*, "short"*/, "short_description" };
            var repoListRequest = _repoPublicCommon.Universe_Ancestries_Ids();
            Func<UniverseAncestriesResult.UniverseAncestriesItem, int> selector = x => x.id;

            var ancestries_to_add = UniverseGeneric.MakeListUpdate<UniverseAncestriesResult, EveOnlineUniverseAncestry, UniverseAncestriesResult.UniverseAncestriesItem>(
                repoListRequest,
                esiClient.Universe.Ancestries,
                multLangProperties
                //selector
                );

            _repoPublicCommon.Universe_Ancestries_AddOrUpdate(ancestries_to_add);
        }
    }
}
