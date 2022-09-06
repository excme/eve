using eveDirect.Repo.ReadWrite;
using eveDirect.Services.EsiConnector.Jobs;
using eveDirect.Shared.GeneralTest;
using Xunit;
using Xunit.Abstractions;

namespace eveDirect.Shared.EsiConnector.Jobs.Test
{
    public class Corporation : UnitTestCore
    {
        public Corporation(ITestOutputHelper output) : base(output)
        {
            _repoPublicCommon = new ReadWriteRepo(_eventBus, _publicContextOptions);
        }

        [Fact]
        public void PublicInformation()
        {
            var job = new CorporationPublicInformation(_repoPublicCommon, null);
            job.Execute();
        }

        [Fact]
        public void Ncp()
        {
            CorporationNpcs job = new CorporationNpcs(_repoPublicCommon, null);
            job.Execute();
        }

        [Fact]
        public void CorporationAllianceHistories()
        {
            var job = new CorporationAllianceHistories(_repoPublicCommon, null);
            job.Execute();
            //job.SimpleCorporation(98010376);
        }
    }
}
