using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Сбор информации о войнах
    /// </summary>
    public class WarInfosUpdate : ConnectorJob
    {
        public WarInfosUpdate(IReadWrite repoPublicCommon)
        {
            _repoPublicCommon = repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            Expression<Func<EveOnlineWar, bool>> where = x => x.declared.Year < 2000 || (x.declared.Year > 2000 && x.finished == null);
            var wars_to_updateInfo = _repoPublicCommon.War_Ids(where);
            wars_to_updateInfo = wars_to_updateInfo.OrderByDescending(x => x).ToList();

            //foreach (var war_id in wars_to_updateInfo)
            Parallel.ForEach(wars_to_updateInfo, war_id =>
            {
                Last_Step = $"Обновление войны. {war_id}";
                War_UpdatePublicInformation(war_id);
            });
        }
        /// <summary>
        /// Выполнение обновления публичной информации через коннектор о войне
        /// </summary>
        public void War_UpdatePublicInformation(int war_id)
        {
            var request = EsiConnector(esiClient.Wars.Information, war_id);
            
            if (request.isSuccess)
            {
                _repoPublicCommon.War_Update(war_id, request.Data);
            }
            else
            {
                // Error 422: War not found
                if ((int)request.StatusCode == 422)
                {
                    _repoPublicCommon.War_Remove(war_id);
                }
            }

        }
    }
}