using eveDirect.Repo.ReadWrite;
using System;
using System.Linq;

namespace eveDirect.Services.EsiConnector.Jobs
{
    /// <summary>
    /// Сбор war_ids
    /// </summary>
    public class WarIdsUpdate: ConnectorJob
    {
        public WarIdsUpdate(IReadWrite repoPublicCommon)
        {
            _repoPublicCommon= repoPublicCommon ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        public override void Execute()
        {
            //Last_Step = "Wars_ПолучениеИдВойн";
            var war_all_ids_request = EsiConnector(esiClient.Wars.All);
            if (war_all_ids_request.isSuccess)
            {
                var war_all_ids = war_all_ids_request.Data;

                if (war_all_ids?.Any() ?? false)
                {
                    // Текущая последняя война
                    int current_max_war_id = war_all_ids.First();
                    int db_max_war_id = _repoPublicCommon.War_GetLast();

                    // Если есть ные войны, то добавляем
                    if (db_max_war_id < current_max_war_id)
                    {
                        var cur_war_id = db_max_war_id;
                        while (cur_war_id < current_max_war_id)
                        {
                            cur_war_id++;
                            _repoPublicCommon.War_AddNew(cur_war_id);
                        }
                    }
                }
            }
        }
    }
}
