using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Sovereignty : BaseConnector
    {
        /// <summary>
        /// GET /sovereignty/structures/
        /// </summary>
        [Fact]
        public void SovereigntyStructuresResult()
        {
            ExecuteAndOutput(connector.Sovereignty.Structures());
        }

        /// <summary>
        /// GET /sovereignty/campaigns/
        /// </summary>
        [Fact]
        public void SovereigntyCampaignsResult()
        {
            ExecuteAndOutput(connector.Sovereignty.Campaigns());
        }

        /// <summary>
        /// GET /sovereignty/map/
        /// </summary>
        [Fact]
        public void SovereigntyMapsResult()
        {
            ExecuteAndOutput(connector.Sovereignty.Systems());
        }
    }
}
