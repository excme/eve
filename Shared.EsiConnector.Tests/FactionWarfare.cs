using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class FactionWarfare:BaseConnector
    {
        /// <summary>
        /// GET /fw/wars/
        /// </summary>
        [Fact]
        public void FwWarsResult()
        {
            ExecuteAndOutput(connector.FactionWarfare.Wars());
        }
        /// <summary>
        /// GET /fw/stats/
        /// </summary>
        [Fact]
        public void FwStatsResult()
        {
            ExecuteAndOutput(connector.FactionWarfare.Stats());
        }
        /// <summary>
        /// GET /fw/systems/
        /// </summary>
        [Fact]
        public void FwSystemsResult()
        {
            ExecuteAndOutput(connector.FactionWarfare.Systems());
        }
        /// <summary>
        /// GET /fw/leaderboards/
        /// </summary>
        [Fact]
        public void FwLeaderboardsResult()
        {
            ExecuteAndOutput(connector.FactionWarfare.Leaderboads());
        }
        /// <summary>
        /// GET /fw/leaderboards/characters/
        /// </summary>
        [Fact]
        public void FwLeaderboardsCharactersResult()
        {
            ExecuteAndOutput(connector.FactionWarfare.LeaderboardsForCharacters());
        }
        /// <summary>
        /// GET /fw/leaderboards/corporations/
        /// </summary>
        [Fact]
        public void FwLeaderboardsCorporationsResult()
        {
            ExecuteAndOutput(connector.FactionWarfare.LeaderboardsForCorporations());
        }
        /// <summary>
        /// GET /corporations/{corporation_id}/fw/stats/
        /// </summary>
        [Fact]
        public void CorporationFwStatsResult()
        {
            ExecuteAndOutput(connector.FactionWarfare.StatsForCorporation());
        }
        /// <summary>
        /// GET /characters/{character_id}/fw/stats/
        /// </summary>
        [Fact]
        public void CharactersFwStatsResult()
        {
            ExecuteAndOutput(connector.FactionWarfare.StatsForCharacter());
        }
    }
}
