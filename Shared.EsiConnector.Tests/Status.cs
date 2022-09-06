using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Status:BaseConnector
    {
        /// <summary>
        /// GET /status/
        /// </summary>
        [Fact]
        public void StatusResult()
        {
            ExecuteAndOutput(connector.Status.Retrieve());
        }
    }
}
