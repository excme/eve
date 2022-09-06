using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Job. Groups Universe
    /// </summary>
    public class UniverseGroups : ConnectorJob
    {
        public UniverseGroups(IReadWrite repoPublicCommon, ILogger<UniverseGroups> logger): base(logger)
        {
            _repoPublicCommon = repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            var multLangProperties = new List<string>() { "name" };
            var repoListRequest = _repoPublicCommon.Universe_Groups_Ids();

            var groups_to_add = UniverseGeneric.MakeListUpdate<UniverseGroupsResult, EveOnlineUniverseGroup, UniverseGroupInfoResult>(
                repoListRequest,
                esiClient.Universe.Groups,
                multLangProperties,
                esiClient.Universe.Group
                );

            _jobResult.Value = _repoPublicCommon.Universe_Groups_AddOrUpdate(groups_to_add);

            
        }
    }
}
