using eveDirect.Clients.Web.Areas.Market.Controllers;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using eveDirect.Shared.GeneralTest;
using eveDirect.Clients.Web.Services;
using Xunit.Sdk;

namespace Common.UnitTests
{
    public class MarketContracts : UnitTestCore
    {
        int contract_id = 156585308;
        ICheckExistService checkExistService { get; }
        public MarketContracts(ITestOutputHelper output) : base(output) {
            throw new NullException(nameof(checkExistService));
        }

        [Fact]
        public async Task Contracts_List()
        {
            // Arrange

            // Act
            var marketContoller = new ContractsController(checkExistService);
            //var actionResult = await marketContoller.List(null);

            // Assert
        }

        [Fact]
        public async Task Contracts_Groups()
        {
            // Arrange

            // Act
            var marketContoller = new ContractsController(checkExistService);
            //var actionResult = await marketContoller.Groups("ru");

            // Assert
        }

        [Fact]
        public async Task Contracts_Items()
        {
            // Arrange

            // Act
            var marketContoller = new ContractsController(checkExistService);
            //var actionResult = await marketContoller.Items(contract_id);

            // Assert
        }

        [Fact]
        public async Task Contracts_Bids()
        {
            // Arrange

            // Act
            var marketContoller = new ContractsController(checkExistService);
            //var actionResult = await marketContoller.Bids(contract_id);

            // Assert
        }
    }
}
