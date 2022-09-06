using System;
using Hangfire;
using eveDirect.Services.Jobs.Core;
using eveDirect.Repo.ReadWrite;
using eveDirect.Databases.Contexts.Public.Models;

namespace eveDirect.Warface.Jobs
{
    /// <summary>
    /// Поиск внутернних id
    /// </summary>
    [MaximumConcurrentExecutions(3)]
    public class SearcInnerIds : JobBase
    {
        IReadWrite _repoPublicCommon { get; set; }
        public SearcInnerIds(IReadWrite repoPublicCommon, int max_Count = 6000): base(null)
        {
            _repoPublicCommon = repoPublicCommon
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
            
            Max_Items_To_Request = max_Count;
        }
        public override void Execute()
        {
            //_performContext = context;

            //// Выбор из загруженных
            //Expression<Func<EveOnlineKillMail, bool>> where = x => !x.updatedSearchIds && (x.victim != null || x.attackers != null);
            //List<EveOnlineKillMail> killmails_toUpdate = await _repoPublicCommon.Killmails_Get(where, skip: random.Next(50) * 1000, take: Max_Items_To_Request);

            //// Перебор
            //await killmails_toUpdate.ParallelForEachAsync(async killmail => {
            //    await SimpleTask(killmail);
            //}, Environment.ProcessorCount * 4);

            //return Task.CompletedTask;
        }
        public void SimpleTask(int killmail_id)
        {
            var killmail = _repoPublicCommon.Killmail_Get(killmail_id);
            SimpleTask(killmail);
        }
        public void SimpleTask(EveOnlineKillMail killmail)
        {
            _repoPublicCommon.Killmails_SearcInnerIds(killmail);
        }
    }
}
