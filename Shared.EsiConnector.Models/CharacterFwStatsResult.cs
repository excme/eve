using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Get /characters/{character_id}/fw/stats/
    /// </summary>
    public class CharacterFwStatsResult
    {
        public int current_rank { get; set; }
        public DateTime enlisted_on { get; set; }
        public int faction_id { get; set; }
        public int highest_rank { get; set; }
        public FwStatsResult.Kills kills { get; set; }
        public FwStatsResult.VictoryPoints victory_points { get; set; }
    }
}
