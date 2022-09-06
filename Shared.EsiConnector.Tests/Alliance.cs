using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Alliance:BaseConnector
    {
        /// <summary>
        /// GET /alliances/{alliance_id}/
        /// </summary>
        [Fact]
        public void AllianceInfoResult()
        {
            ExecuteAndOutput(connector.Alliance.Information(allinceId));
        }

        /// <summary>
        /// GET /alliances/{alliance_id}/corporations/
        /// </summary>
        [Fact]
        public void AlliancesCorporationsResult()
        {
            ExecuteAndOutput(connector.Alliance.Corporations(allinceId));
        }
        
        /// <summary>
        /// GET /alliances/{alliance_id}/icons/
        /// </summary>
        [Fact]
        public void AlliancesIconsResult()
        {
            ExecuteAndOutput(connector.Alliance.Icons(allinceId));
        }

        /// <summary>
        /// GET /alliances/
        /// </summary>
        [Fact]
        public void AlliancesResult()
        {
            ExecuteAndOutput(connector.Alliance.All());
        }
    }
}
