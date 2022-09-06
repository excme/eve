using eveDirect.Repo.ReadWrite;
using eveDirect.Services.Jobs.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationPublicInformation : ConnectorJob
    {
        private int Part_of;

        public CorporationPublicInformation(
           IReadWrite repoPublicCommon,
           ILogger<CorporationPublicInformation> logger,
           int part_of = 3000) : this(repoPublicCommon, logger, null, null, null, null, part_of) { }

        public CorporationPublicInformation(
            IReadWrite repoPublicCommon,
            ILogger<CorporationPublicInformation> logger,
            string proxy_addr,
            string proxy_port,
            string proxy_user,
            string proxy_pass,
            int part_of = 3000)
            : base(logger, proxy_addr, proxy_port, proxy_user, proxy_pass)
        {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
            Part_of = part_of;
        }
        public override void Execute()
        {
           

            // Выбор корпораций на обновление
            var to_updating = _repoPublicCommon.Corporation_Ids(
                part_of: Part_of
            );

            // Если нет имени
            var not_name = _repoPublicCommon.Corporation_Ids(where: x => x.name == null);
            if (not_name?.Any() ?? false)
                to_updating.AddRange(not_name);

            _jobResult.subValues.Add(new JobResult.Item() { Name = "count", Value = to_updating.Count });

            var list = AttachProgressBarToList(to_updating);
            //await AttachProgressBarToList(to_updating).ParallelForEachAsync(async corporation_id =>
            //foreach (var corporation_id in AttachProgressBarToList(to_updating))
            Parallel.ForEach(list, corporation_id => 
            {
                var b = SimpleCorporation(corporation_id);
                if (b)
                    _jobResult.Value++;
            });

            
        }

        public bool SimpleCorporation(int corporation_id, bool newCreated = false)
        {
            // Выполнение запроса
            var request = EsiConnector(esiClient.Corporation.Information, corporation_id);

            if (request.isSuccess)
            {
                bool updated = _repoPublicCommon.Corporation_Update_PublicInfo(corporation_id, request.Data, newCreated);
                return updated;
            }
            else if ((int)request.StatusCode == 404)
            {
                _repoPublicCommon.Corporation_Delete(corporation_id);
                LogInfo($"Удален: {corporation_id}");
            }

            return false;
        }
    }
}
