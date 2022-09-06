using Xunit;
using Xunit.Abstractions;
using eveDirect.Shared.GeneralTest;

namespace eveDirect.Warface.Jobs.Tests
{
    public class KillmailsInfos : UnitTestCore
    {
        public KillmailsInfos(ITestOutputHelper output) : base(output) { }
        [Fact]
        public void CollectionKillsFromZKillBoardApi()
        {
            var job = new CollectionKillsFromZKillBoardApi(_repoPublicCommon, null);
            job.Execute();
        }
        [Fact]
        public void SearchInnerIds()
        {
            var job = new SearcInnerIds(_repoPublicCommon);
            job.SimpleTask(68653937);
        }
        //[Fact]
        //public async Task UpdateLocationIds()
        //{
        //    var job = new UpdateLocationIds(_repoPublicCommon);
        //    await job.SimpleTask(68653937);
        //}
    }
}
