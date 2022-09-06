namespace eveDirect.Warface.Jobs
{
    //[DisableConcurrentExecution(10)]
    //public class UpdateLocationIds : JobBase
    //{
    //    IReadWrite _repoPublic { get; set; }
    //    public UpdateLocationIds(IReadWrite repoPublic, int max_count = 6000)
    //    {
    //        _repoPublic = repoPublic
    //            ?? throw new ArgumentNullException(nameof(repoPublic));

    //        Max_Items_To_Request = max_count;
    //    }
    //    public override async Task TaskJob(PerformContext context = null)
    //    {
    //       

    //        Expression<Func<EveOnlineKillMail, bool>> where = x => x.killmail_hash.Length == 40 && x.victim.location_id == null && x.victim.px != 0 && x.victim.py != 0 && x.victim.pz != 0;
    //        List<EveOnlineKillMail> killmails_toUpdate = await _repoPublic.Killmails_Get(where, take: Max_Items_To_Request);

    //        await killmails_toUpdate.ParallelForEachAsync(async killmail => {
    //            await SimpleKillmail(killmail);
    //        }, Environment.ProcessorCount);
    //    }

    //    public async Task SimpleTask(int killmail_id)
    //    {
    //        var killmail = await _repoPublic.Killmail_Get(killmail_id);
    //        await SimpleKillmail(killmail);
    //    }

    //    async Task SimpleKillmail(EveOnlineKillMail killmail)
    //    {
    //        await _repoPublic.Killmails_UpdateGetLocationId(killmail);
    //    }
    //}
}
