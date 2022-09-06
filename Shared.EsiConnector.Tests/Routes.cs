using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Routes: BaseConnector
    {
        /// <summary>
        /// GET /route/{origin}/{destination}/
        /// </summary>
        [Fact]
        public void RouteInfoResult()
        {
            int OriginId = 30002771;
            int destinationId = 30002779;
            ExecuteAndOutput(connector.Routes.Map(OriginId, destinationId));
        }
    }
}
