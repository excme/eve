using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Databases.Contexts.Public.Models;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class UniverseFaction : ConnectorJob
    {
        public UniverseFaction(IReadWrite repoPublicCommon, ILogger<UniverseFaction> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            var multLangProperties = new List<string>() { "name", "description" };
            var repoListRequest = _repoPublicCommon.Universe_Factions_Ids();
            Func<UniverseFactionsResult.UniverseFactionsItem, int> selector = x => x.faction_id;

            var fractions_to_add = UniverseGeneric.MakeListUpdate<UniverseFactionsResult, EveOnlineUniverseFaction, UniverseFactionsResult.UniverseFactionsItem>(
                repoListRequest,
                esiClient.Universe.Factions,
                multLangProperties
                );

            _jobResult.Value = _repoPublicCommon.Universe_Factions_AddOrUpdate(fractions_to_add);

            
        }
    }
}
