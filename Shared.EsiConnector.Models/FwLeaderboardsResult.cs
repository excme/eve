using System;
using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /fw/leaderboards/
    /// </summary>
    public class TotalFwLeaderboardsResult : ISsoResult
    {
        public TotalFwLeaderboardSummary kills { get; set; }
        public TotalFwLeaderboardSummary victory_points { get; set; }
        public class FwTotal
        {
            public int faction_id { get; set; }
            public int amount { get; set; }
        }
        public class TotalFwLeaderboardSummary
        {
            public List<FwTotal> yesterday { get; set; } = new List<FwTotal>();
            public List<FwTotal> last_week { get; set; } = new List<FwTotal>();
            public List<FwTotal> active_total { get; set; } = new List<FwTotal>();
        }
    }

    /// <summary>
    /// GET /fw/leaderboards/characters/
    /// </summary>
    public class CharacterFwLeaderboardsResult : ISsoResult
    {
        public CharacterFwLeaderboardSummary kills { get; set; }
        public CharacterFwLeaderboardSummary victory_points { get; set; }
        public class CharacterFwLeaderboardSummary
        {
            public List<FwCharacterTotal> yesterday { get; set; } = new List<FwCharacterTotal>();
            public List<FwCharacterTotal> last_week { get; set; } = new List<FwCharacterTotal>();
            public List<FwCharacterTotal> active_total { get; set; } = new List<FwCharacterTotal>();
        }
        public class FwCharacterTotal
        {
            public int character_id { get; set; }
            public int amount { get; set; }
        }
    }

    /// <summary>
    /// GET /fw/leaderboards/corporations/
    /// </summary>
    public class CorporationFwLeaderboardsResult : ISsoResult
    {
        public CorporationFwLeaderboardSummary kills { get; set; }
        public CorporationFwLeaderboardSummary victory_points { get; set; }
        public class CorporationFwLeaderboardSummary
        {
            public List<FwCorporationTotal> yesterday { get; set; } = new List<FwCorporationTotal>();
            public List<FwCorporationTotal> last_week { get; set; } = new List<FwCorporationTotal>();
            public List<FwCorporationTotal> active_total { get; set; } = new List<FwCorporationTotal>();
        }
        public class FwCorporationTotal
        {
            public int corporation_id { get; set; }
            public int amount { get; set; }
        }
    }
}
