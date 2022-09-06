using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector.Models;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class KillmailsInfos : ConnectorJob
    {
        public KillmailsInfos(IReadWrite repoPublicCommon, ILogger<KillmailsInfos> logger, int maxItems = 10000) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
            Max_Items_To_Request = maxItems;
        }

        public void TestPrice(int v, DateTime dateTime)
        {
           _repoPublicCommon.Market_HistoryPrice(v, dateTime);
        }

        public override void Execute()
        {
           

            Expression<Func<EveOnlineKillMail, bool>> where = x => x.killmail_hash.Length == 40 && x.killmail_time == null;
            List<EveOnlineKillMail> killmails_toUpdate = _repoPublicCommon.Killmails_Get(
                where, 
                take: Max_Items_To_Request, 
                skip: random.Next(100)*1000, 
                include: x=> x.victim
            );

            Simple(killmails_toUpdate.ToArray());
        }
        ConcurrentDictionary<EveOnlineKillMail, KillMailInfoResult> SimpleUpdate(params EveOnlineKillMail[] killMails)
        {
            ConcurrentDictionary<EveOnlineKillMail, KillMailInfoResult> updated = new ConcurrentDictionary<EveOnlineKillMail, KillMailInfoResult>();
            // Запросы Esi
            var kms = killMails.ToList();
            //await killMails.ToList().ParallelForEachAsync(async killmail =>
            Parallel.ForEach(kms, killmail =>
            {
                var killmailResult = EsiConnector(esiClient.Killmails.Information, killmail.killmail_id, killmail.killmail_hash);
                if (killmailResult.isSuccess)
                    updated.TryAdd(killmail, killmailResult.Data);

            });
            
            return updated;
        }
        public void Simple(params EveOnlineKillMail[] killMails)
        {
            var updated = SimpleUpdate(killMails);
            // Обновление в БД
            _repoPublicCommon.Killmails_UpdateResults(updated);
        }
    }
}
