using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Insurance : BaseConnector
    {
        /// <summary>
        /// GET /insurance/prices/
        /// </summary>
        [Fact]
        public void InsurancePricesResult()
        {
            ExecuteAndOutput(connector.Insurance.Levels());
        }
    }
}
