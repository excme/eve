using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Incursions:BaseConnector
    {
        /// <summary>
        /// GET /incursions/
        /// </summary>
        [Fact]
        public void IncursionResult()
        {
            ExecuteAndOutput(connector.Incursions.All());
        }
    }
}
