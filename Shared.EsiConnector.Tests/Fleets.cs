using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Fleets : BaseConnector
    {
        /// <summary>
        /// GET /fleets/{fleet_id}/
        /// </summary>
        [Fact]
        public void FleetInfoResult()
        {
            long fleetID = 1128411456575;
            ExecuteAndOutput(connector.Fleets.Settings(fleetID));
        }

        /// <summary>
        /// GET /characters/{character_id}/fleet/
        /// </summary>
        [Fact]
        public void CharacterFleetInfoResult()
        {
            ExecuteAndOutput(connector.Fleets.FleetInfo());
        }

        /// <summary>
        /// GET /fleets/{fleet_id}/members/
        /// </summary>
        [Fact]
        public void FleetMembersResult()
        {
            long fleetID = 1128411456575;
            ExecuteAndOutput(connector.Fleets.Members(fleetID));
        }

        /// <summary>
        /// GET /fleets/{fleet_id}/wings/
        /// </summary>
        [Fact]
        public void FleetWingsResult()
        {
            long fleetID = 1128411456575;
            ExecuteAndOutput(connector.Fleets.Wings(fleetID));
        }

    }
}
