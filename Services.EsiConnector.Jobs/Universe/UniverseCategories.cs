using System;
using System.Collections.Generic;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector.Models;
using Microsoft.Extensions.Logging;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Job. Categories Universe 
    /// </summary>
    public class UniverseCategories : ConnectorJob
    {
        public UniverseCategories(IReadWrite repoPublicCommon, ILogger<UniverseCategories> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            var multLangProperties = new List<string>() { "name" };
            var repoListRequest = _repoPublicCommon.Universe_Categories_Ids();

            var types_to_add = UniverseGeneric.MakeListUpdate<UniverseCategoriesResult, EveOnlineUniverseCategory, UniverseCategoryInfoResult>(
                repoListRequest,
                esiClient.Universe.Categories,
                multLangProperties,
                esiClient.Universe.Category
                );

            _jobResult.Value = _repoPublicCommon.Universe_Categories_AddOrUpdate(types_to_add);

            
        }
    }
}
