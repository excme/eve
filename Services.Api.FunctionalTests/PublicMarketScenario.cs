using eveDirect.Clients.Web;
using eveDirect.Shared.GeneralTest;
using System.Threading.Tasks;
using Xunit;

namespace Common.FunctionalTests
{
    public class PublicMarketScenario : FunctionalTestCore<Startup>
    {
        [Fact]
        public async Task Get_ActiveRegionsSystems()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().GetAsync(ApiMarketPaths.ActiveRegionsSystems());
            var httpResponseCode = response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_AllRegionsSystems()
        {
            using var server = CreateServer();
            var response = await server.CreateClient().GetAsync(ApiMarketPaths.AllRegionsSystems());
            var httpResponseCode = response.EnsureSuccessStatusCode();
        }
    }

    public static class ApiMarketPaths
    {
        public static string ActiveRegionsSystems()
        {
            return "/api/market/mars";
        }
        public static string AllRegionsSystems()
        {
            return "/api/market/mrs";
        }
    }
}
