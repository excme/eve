using System;
using System.Threading.Tasks;
using eveDirect.Repo.ReadWrite;
using eveDirect.Services.Jobs.Core;
using Microsoft.Extensions.Logging;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Закачивание истории миграций корпораций между альянсами
    /// </summary>
    public class CorporationAllianceHistories : ConnectorJob
    {
        private int Part_of;
        public CorporationAllianceHistories(
            IReadWrite repoPublicCommon,
            ILogger<CorporationAllianceHistories> logger, 
            int part_of = 3000) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));

            Part_of = part_of;
        }
        public override void Execute()
        {
           

            //Expression<Func<EveOnlineCorporation, bool>> where = x => x.lastUpdate_allianceHistory == null;

            // Мертвые корпорации
            //Expression<Func<EveOnlineCorporation, bool>> not_dead = x => x.member_count > 0;

            // Выбор корпораций на обновление
            var to_updating = _repoPublicCommon.Corporation_Ids(
                part_of: Part_of
            );

            _jobResult.subValues.Add(new JobResult.Item() { Name = "count", Value = to_updating.Count });
            
            //foreach(var corporation_id in AttachProgressBarToList(to_updating))
            Parallel.ForEach(AttachProgressBarToList(to_updating), corporation_id => 
            {
                var b = SimpleCorporation(corporation_id);
                if (b)
                    _jobResult.Value++;
            });
        }
        public bool SimpleCorporation(int corporation_id)
        {
            // Выполнение запроса
            var result = EsiConnector(esiClient.Corporation.AllianceHistory, corporation_id);
            if (result.isSuccess)
                // Проверка отношений корпораций к альянсу
                _repoPublicCommon.Corporation_UpdateAllianceHistory(corporation_id, result.Data);

            return result.isSuccess;
        }
    }
}
