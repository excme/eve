using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/fw/stats/
    /// </summary>
    public class CharactersFwStatsResult
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
        public int current_rank { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime enlisted_on { get; set; }
        public int faction_id { get; set; }
        public int highest_rank { get; set; }
        public Kills kills { get; set; }
        public VictoryPoints victory_points { get; set; }
    }
}
