using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eveDirect.Repo.ReadWrite;
using eveDirect.Services.Jobs.Core;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class AlliancesPublicInformation : ConnectorJob
    {
        int Part_of { get; }
        public AlliancesPublicInformation(IReadWrite repoPublicCommon,
            ILogger<AlliancesPublicInformation> logger, int part_of = 40) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
            Part_of = part_of;
        }
        public override void Execute()
        {
            

            // Выбор корпораций на обновление
            var to_updating = _repoPublicCommon.Alliance_Ids(
                part_of: Part_of
            );

            // Если нет имени
            var not_name = _repoPublicCommon.Alliance_Ids(where: x => x.name == null);
            if (not_name?.Any() ?? false)
                to_updating.AddRange(not_name);

            _jobResult.subValues.Add(new JobResult.Item() { Name = "count", Value = to_updating.Count });

            var list = AttachProgressBarToList(to_updating);
            //await list.ParallelForeac(async alliance_id =>
            //foreach (var character_id in list)
            Parallel.ForEach(list, alliance_id =>
            {
                var b = SimpleAlliance(alliance_id);
                if (b)
                    _jobResult.Value++;
            });

        }
        public bool SimpleAlliance(int alliance_id, bool new_Created = false)
        {
            // Выполнение запроса
            var result = EsiConnector(esiClient.Alliance.Information, alliance_id);

            if (result.isSuccess)
            {
                var updated = _repoPublicCommon.Alliance_Update(alliance_id, result.Data, new_Created);
                return updated;
            }
            else if ((int)result.StatusCode == 404)
            {
                _repoPublicCommon.Alliance_Delete(alliance_id);
                LogInfo($"Удален: {alliance_id}");
            }

            return false;
        }
    }
}
