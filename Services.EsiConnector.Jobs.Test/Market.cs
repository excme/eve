using eveDirect.Shared.GeneralTest;
using Xunit;
using Xunit.Abstractions;
using eveDirect.Services.Jobs.Market;
using eveDirect.Repo.ReadWrite;

namespace eveDirect.Shared.EsiConnector.Jobs.Test
{
    public class Market : UnitTestCore
    {
        public Market(ITestOutputHelper output) : base(output)
        {
            _repoPublicCommon = new ReadWriteRepo(_eventBus, _publicContextOptions);
        }

        [Fact]
        public void ActualOrders()
        {
            //update_DbConnOprions("PublicDb");
            MarketActualOrders job = new MarketActualOrders(_repoPublicCommon, null);
            job.Execute();
        }
        [Fact]
        public void HistoryPrices()
        {
            MarketHistoryPrices job = new MarketHistoryPrices(_repoPublicCommon, null);
             job.SimpleUpdate(10000002, 3651);
            // job.Execute();
        }
        [Fact]
        public void MarketGroups()
        {
            MarketGroups job = new MarketGroups(_repoPublicCommon, null);
             job.Execute();
        }
        [Fact]
        public void Public_Contracts()
        {
            PublicContracts job = new PublicContracts(_repoPublicCommon, null);
             job.SimpleRegion(10000002);
        }
    }
}
