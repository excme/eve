using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using eveDirect.Services.EsiConnector;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Databases.Contexts.Public.Models;
using System.Transactions;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Сбор types
    /// </summary>
    public class UniverseTypes : ConnectorJob
    {
        public UniverseTypes(IReadWrite repoPublicCommon, ILogger<UniverseTypes> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            //Очистка недозакаченных типов
            _repoPublicCommon.Universe_Types_RemoveBroked();

            var multLangProperties = new List<string>() { "name", "description" };
            var repoListRequest = _repoPublicCommon.Universe_Types_Ids();

            var types_to_add = UniverseGeneric.MakeListUpdate<UniverseTypesResult, EveOnlineUniverseType, UniverseTypeInfoResult>(
                repoListRequest,
                esiClient.Universe.Types,
                multLangProperties,
                esiClient.Universe.Type
                );

            // Получение всех type_id, где нет icons
            var get_imgs = types_to_add.Where(x => x.published).ToList();
            var list = AttachProgressBarToList(get_imgs);
            //await get_imgs.ParallelForEachAsync(async type => 
            Parallel.ForEach(get_imgs, type =>
            {
                HttpClient client = new HttpClient();
                do
                {
                    try
                    {
                        HttpResponseMessage response = client.GetAsync("https://images.evetech.net/types/" + type.type_id).GetAwaiter().GetResult();
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string tags = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            if (tags?.Length > 0)
                                type.img_tags = JsonSerializer.Deserialize<List<string>>(tags);
                            break;
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            type.img_tags = new List<string>();
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }catch(Exception ex)
                    {
                        continue;
                    }
                } while (true);
            });

           _jobResult.Value = _repoPublicCommon.Universe_Types_AddOrUpdate(types_to_add);
        }
    }
}
