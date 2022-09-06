using eveDirect.Shared.GeneralTest;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using eveDirect.Clients.Web.Areas.Market.Controllers;

namespace Common.UnitTests
{
    public class MarketOrders : UnitTestCore
    {
        public MarketOrders(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void Market_ActiveRegionsSystems()
        {
            // Arrange

            // Act
            //var marketContoller = new OrdersController(_repoReadOnly, _cache);
            //var actionResult = await marketContoller.Market_ActiveRegionsSystems();

            // Assert
        }

        [Fact]
        public async Task Market_AllRegionsSystems()
        {
            // Arrange

            // Act
            //var marketContoller = new OrdersController(_repoReadOnly, _cache);
            //var actionResult = await marketContoller.Market_AllRegionsSystems();

            // Assert
        }

        [Fact]
        public async Task Market_Groups()
        {
            // Arrange

            // Act
            //var marketContoller = new OrdersController(_repoReadOnly, _cache);
            //var actionResult = await marketContoller.Market_Groups();

            // Assert
        }
    }
}
