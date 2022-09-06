using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/structures/
    /// </summary>
    public class CorporationStructuresResult : List<CorporationStructuresResult.CorporationStructuresItem>, ISsoResult
    {
        public class CorporationStructuresItem
        {
            public int corporation_id { get; set; }
            public DateTime? fuel_expires { get; set; }
            public int next_reinforce_hour { get; set; }
            public int next_reinforce_weekday { get; set; }
            public int profile_id { get; set; }
            public int reinforce_hour { get; set; }
            public int reinforce_weekday { get; set; }
            public DateTime? state_timer_start { get; set; }
            public DateTime? state_timer_end { get; set; }
            public DateTime next_reinforce_apply { get; set; }
            public List<Service> services { get; set; }
            public EStatus state { get; set; }
            public long structure_id { get; set; }
            public int system_id { get; set; }
            public int type_id { get; set; }
            public DateTime? unanchors_at { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EStatus : byte
        {
            anchor_vulnerable = 1,
            anchoring = 2,
            armor_reinforce = 3,
            armor_vulnerable = 4,
            deploy_vulnerable = 5,
            fitting_invulnerable = 6,
            hull_reinforce = 7,
            hull_vulnerable = 8,
            online_deprecated = 9,
            onlining_vulnerable = 10,
            shield_vulnerable = 11,
            unanchored = 12,
            unknown = 13
        }
        public class Service
        {
            public string name { get; set; }
            public EServiceStatus state { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EServiceStatus : byte {
            online=1,
            offline=2,
            cleanup=3
        }
    }
}
