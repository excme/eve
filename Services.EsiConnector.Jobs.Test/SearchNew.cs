using eveDirect.Services.EsiConnector.Jobs;
using eveDirect.Shared.ConfigurationHelper;
using eveDirect.Shared.GeneralTest;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace eveDirect.Shared.EsiConnector.Jobs.Test
{
    public class SearchNew : UnitTestCore
    {
        public SearchNew(ITestOutputHelper output) : base(output){ }

        [Fact]
        public void CharacterSearchNewborn()
        {
            var job = new CharacterSearchNewborn(_repoPublicCommon, null, ConfigurationStatic.GetConfiguration());
             job.Execute();
        }


        [Fact]
        public void CharacterNPCSearchNewborn()
        {
            var job = new CharacterNPCSearchNewborn(_repoPublicCommon, null, ConfigurationStatic.GetConfiguration());
             job.Execute();
        }

        [Fact]
        public void CorporationSearchNew()
        {
            var job = new CorporationSearchNew(_repoPublicCommon, null, ConfigurationStatic.GetConfiguration());
             job.Execute();
        }

        [Fact]
        public void AllianceSearchNew()
        {
            var job = new AllianceSearchNew(_repoPublicCommon, null, ConfigurationStatic.GetConfiguration());
             job.Execute();
        }
    }
}
