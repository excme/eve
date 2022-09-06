using eveDirect.Repo.ReadWrite;
using eveDirect.Services.EsiConnector.Jobs;
using eveDirect.Shared.GeneralTest;
using Xunit;
using Xunit.Abstractions;

namespace eveDirect.Shared.EsiConnector.Jobs.Test
{
    public class Alliance : UnitTestCore
    {
        public Alliance(ITestOutputHelper output) : base(output) 
        {
            _repoPublicCommon = new ReadWriteRepo(_eventBus, _publicContextOptions );
        }

        [Fact]
        public void AlliancesGetList()
        {
            AlliancesGetList job = new AlliancesGetList(_repoPublicCommon, null);
             job.Execute();
        }

        [Fact]
        public void AlliancesGetListCorporations()
        {
            AlliancesGetListCorporations job = new AlliancesGetListCorporations(_repoPublicCommon, null, 0);
             job.Execute();
        }

        [Fact]
        public void AlliancesPublicInformation()
        {
            AlliancesPublicInformation job = new AlliancesPublicInformation(_repoPublicCommon, null, 0);
             job.Execute();
        }
    }
}
