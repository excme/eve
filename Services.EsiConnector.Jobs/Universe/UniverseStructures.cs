using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector;
using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Job. Structures Universe
    /// </summary>
    public class UniverseStructures : ConnectorJob
    {
        //IReadWrite _repoPublicCommon { get; set; }
        public UniverseStructures(IReadWrite repoPublicCommon, ILogger<UniverseStructures> logger, string client_id, string secret)
            : base(logger, client_id, secret)
        {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
            Requires_scope = Scope.Universe.ReadStructures.Name;
        }
        public UniverseStructures(IReadWrite repoPublicCommon, ILogger<UniverseStructures> logger, IConfiguration configuration)
            : this(repoPublicCommon, logger, configuration["SSO:ClientId"], configuration["SSO:Secret"]) { }
        public override void Execute()
        {
            

            // Запрос к esi
            var structures_to_add = new List<long>();
            var request = EsiConnector(esiClient.Universe.Structures);
            if (request.isSuccess)
                structures_to_add = request.Data?.ToList();

            // Запрос с structures
            List<long> cur_structures_ids = _repoPublicCommon.Universe_Structures_Ids();
            structures_to_add.AddRange(cur_structures_ids);

            // Текущие Ид из базы
            // Из ордеров
            var order_locs = _repoPublicCommon.Db_SelectColumn<EveOnlineMarketOrder, long>(select: x => x.location_id, where: x => x.location_id >= int.MaxValue);
            structures_to_add.AddRange(order_locs);

            // Из контрактов
            var contract_slocs = _repoPublicCommon.Db_SelectColumn<EveOnlineContract, long>(select: x => x.start_location_id, where: x => x.start_location_id >= int.MaxValue);
            structures_to_add.AddRange(contract_slocs);
            var contract_elocs = _repoPublicCommon.Db_SelectColumn<EveOnlineContract, long>(select: x => x.end_location_id, where: x => x.end_location_id >= int.MaxValue);
            structures_to_add.AddRange(contract_elocs);

            // На обновление
            var distincts = structures_to_add.Distinct().ToList();
            distincts.Remove(0);

            // Токен
            var sso = _repoPublicCommon.CharacterAuth_GetSso(Requires_scope);

            if (sso != null)
            {
                var client = newEsiClient(sso);
                //await distincts.ParallelForEachAsync(async structure_id =>
                Parallel.ForEach(distincts, structure_id =>
                {
                    Structure_SimpleUpdate(structure_id, client);
                });
            }
        }

        public void Structure_SimpleUpdate(long structure_id, AuthorizedCharacterData data)
        {
            var client =  newEsiClient(data);
             Structure_SimpleUpdate(structure_id, client);
        }

        void Structure_SimpleUpdate(long structure_id, EsiClient client)
        {
            RequestResult<UniverseStructureInfoResult> result = EsiConnector(client.Universe.Structure, structure_id);
            if (result.isSuccess)
                _repoPublicCommon.Universe_Structures_AddOrUpdate(structure_id, result.Data);
        }
    }
}
