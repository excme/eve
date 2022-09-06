using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /fw/stats/
    /// </summary>
    public class FwStatsResult:List<FwStatsResult.FwStatsItem>, ISsoResult
    {
        public class Kills
        {
            public int yesterday { get; set; }
            public int last_week { get; set; }
            public int total { get; set; }
        }

        public class VictoryPoints
        {
            public int yesterday { get; set; }
            public int last_week { get; set; }
            public int total { get; set; }
        }

        public class FwStatsItem
        {
            public int faction_id { get; set; }
            public int pilots { get; set; }
            public int systems_controlled { get; set; }
            public Kills kills { get; set; }
            public VictoryPoints victory_points { get; set; }
        }
    }

    
}
