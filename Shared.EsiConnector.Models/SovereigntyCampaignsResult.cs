using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /sovereignty/campaigns/
    /// </summary>
    public class SovereigntyCampaignsResult:List<SovereigntyCampaignsResult.SovereigntyCampaignsItem>, ISsoResult
    {
        public class SovereigntyCampaignsItem
        {
            public int campaign_id { get; set; }
            public long structure_id { get; set; }
            public int solar_system_id { get; set; }
            public int constellation_id { get; set; }
            public EEventType event_type { get; set; }
            public DateTime start_time { get; set; }
            public int defender_id { get; set; }
            public float defender_score { get; set; }
            public float attackers_score { get; set; }
            public List<SovereigntyCampaignsParticipant> participants { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EEventType : byte
        {
            tcu_defense = 1,
            ihub_defense = 2,
            station_defense = 3,
            station_freeport = 4
        }
        public class SovereigntyCampaignsParticipant
        {
            public int alliance_id { get; set;}
            public float score { get; set; }
        }
    }
    
}
