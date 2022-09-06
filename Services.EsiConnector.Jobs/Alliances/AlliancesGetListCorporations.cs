using System;
using System.Threading.Tasks;
using eveDirect.Repo.ReadWrite;
using Microsoft.Extensions.Logging;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Обновление у альянсов из базы внутренних корпораций
    /// </summary>
    public class AlliancesGetListCorporations : ConnectorJob
    {
        int Part_of { get; }
        public AlliancesGetListCorporations(
            IReadWrite repoPublicCommon,
            ILogger<AlliancesGetListCorporations> logger,
            int max_Items_To_Request = 100,
            int part_of = -1):base (logger) {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));

            Max_Items_To_Request = max_Items_To_Request;
            Part_of = part_of;
        }
        public override void Execute()
        {
           

            // Выбор корпораций на обновление
            var to_updating = _repoPublicCommon.Alliance_Ids( 
                max_count: Max_Items_To_Request,
                part_of: Part_of
            );

            var list = AttachProgressBarToList(to_updating);
            //await list.ParallelForEachAsync(async alliance_id =>
            //foreach (var alliance_id in list)
            Parallel.ForEach(list, alliance_id =>
            {
                SimpleAlliance(alliance_id);
            });

            
        }

        public void SimpleAlliance(int alliance_id)
        {
            // Выполнение запроса
            var result = EsiConnector(esiClient.Alliance.Corporations, alliance_id);
            if (result.isSuccess)
            {
                // Обновление связей у текущего альянса
                var changed = _repoPublicCommon.Alliance_UpdateCorpations(alliance_id, result.Data);
                _jobResult.Value += changed;
            }
        }
    }
}
