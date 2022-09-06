using eveDirect.Services.EsiConnector.Jobs;
using eveDirect.Shared.ConfigurationHelper;
using eveDirect.Shared.GeneralTest;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace eveDirect.Shared.EsiConnector.Jobs.Test
{
    public class Universe : UnitTestCore
    {
        public Universe(ITestOutputHelper output) : base(output)
        {
            //_repoPublicCommon = new ReadWriteRepo(LoadContext<PublicContext>("PublicContext"), _eventBus);
        }
        [Fact]
        public void Ancestries()
        {
            UniverseAncestries job = new UniverseAncestries(_repoPublicCommon, null);
             job.Execute();
        }
        [Fact]
        public void Bloodlines()
        {
            UniverseBloodline job = new UniverseBloodline(_repoPublicCommon, null);
             job.Execute();
        }
        [Fact]
        public void Categories()
        {
            UniverseCategories job = new UniverseCategories(_repoPublicCommon, null);
             job.Execute();
        }
        [Fact]
        public void Factions()
        {
            UniverseFaction job = new UniverseFaction(_repoPublicCommon, null);
             job.Execute();
        }
        [Fact]
        public void Graphics()
        {
            UniverseGraphics job = new UniverseGraphics(_repoPublicCommon);
             job.Execute();
        }
        [Fact]
        public void Groups()
        {
            UniverseGroups job = new UniverseGroups(_repoPublicCommon, null);
             job.Execute();
        }
        [Fact]
        public  Task Ids()
        {
            return Task.CompletedTask;
        }
        [Fact]
        public void UniverseSearchNewInRange()
        {
            var job = new UniverseSearchNewInRange(_repoPublicCommon, null);
            // job.TaskSimple2();
        }
        [Fact]
        public void Races()
        {
            UniverseRace job = new UniverseRace(_repoPublicCommon, null);
             job.Execute();
        }
        [Fact]
        public void Structures()
        {
            var job = new UniverseStructures(_repoPublicCommon, null, clientId, secretKey);
             job.Execute();
        }
        [Fact]
        public void SystemJumps()
        {
        }
        [Fact]
        public void SystemKills()
        {
        }
        [Fact]
        public void ОбновлениеКосмоса()
        {
            UniverseОбновлениеКосмоса job = new UniverseОбновлениеКосмоса(_repoPublicCommon, null);
             job.Execute();
        }
        [Fact]
        public void Types()
        {
            update_DbConnOprions("PublicDb");
            UniverseTypes job = new UniverseTypes(_repoPublicCommon, null);
            job.Execute();
        }
    }
}
