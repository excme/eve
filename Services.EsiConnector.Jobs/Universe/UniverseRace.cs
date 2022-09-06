using System;
using System.Collections.Generic;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector.Models;
using Microsoft.Extensions.Logging;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class UniverseRace : ConnectorJob
    { 
        public UniverseRace(IReadWrite repoPublicCommon, ILogger<UniverseRace> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            

            var multLangProperties = new List<string>() { "name", "description" };
            var repoListRequest = _repoPublicCommon.Universe_Races_Ids();
            Func<UniverseRacesResult.UniverseRacesItem, int> selector = x => x.race_id;

            var races_to_add = UniverseGeneric.MakeListUpdate<UniverseRacesResult, EveOnlineUniverseRace, UniverseRacesResult.UniverseRacesItem>(
                repoListRequest,
                esiClient.Universe.Races,
                multLangProperties
                );

            _jobResult.Value = _repoPublicCommon.Universe_Races_AddOrUpdate(races_to_add);

            
        }
    }
}