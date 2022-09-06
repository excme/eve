using eveDirect.Shared.GeneralTest;
using eveDirect.Services.EsiConnector.Jobs;
using System;
using Xunit;
using Xunit.Abstractions;

namespace eveDirect.Shared.EsiConnector.Jobs.Test
{
    public class Killmails : UnitTestCore
    {
        string killmailsConStr { get; set; }
        string commonConStr { get; set; }
        public Killmails(ITestOutputHelper output) : base(output){ }
        [Fact]
        public void KillmailsInfo()
        {
            KillmailsInfos job = new KillmailsInfos(_repoPublicCommon, null, 1);
             job.Execute();
        }
        [Fact]
        public void GetPrice()
        {
            KillmailsInfos job = new KillmailsInfos(_repoPublicCommon, null);
             job.TestPrice(34, new DateTime(2019,6,16, 0, 0,0));
        }
        //[Fact]
        //public void KillmailItemInfo()
        //{
        //    int killmail_id = 77012809;
        //    string killmail_hash = "4e2283492fd769fb89aaa22f84bb4885dfe898e8";
        //    KillmailsInfos job = new KillmailsInfos(_repoPublicWarsKillmails);
        //     job.UpdateKillmail(killmail_id, killmail_hash);
        //}
    }
}
