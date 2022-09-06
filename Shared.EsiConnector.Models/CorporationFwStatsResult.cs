using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Get /corporations/{corporation_id}/fw/stats/
    /// </summary>
    public class CorporationFwStatsResult
    {
        public DateTime enlisted_on { get; set; }
        public int faction_id { get; set; }
        public FwStatsResult.Kills kills { get; set; }
        public int pilots { get; set; }
        public FwStatsResult.VictoryPoints victory_points { get; set; }
    }
}
