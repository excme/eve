using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /killmails/{killmail_id}/{killmail_hash}/
    /// </summary>
    public class KillMailInfoResult : ISsoResult
    {
        public int killmail_id { get; set; }
        public DateTime killmail_time { get; set; }
        public Victim victim { get; set; }
        public virtual List<Attacker> attackers { get; set; } = new List<Attacker>();
        public int solar_system_id { get; set; }
        public int? moon_id { get; set; }
        public int? war_id { get; set; }

        public class Item: InnerItem
        {
            [JsonIgnore]
            public List<InnerItem> items { get; set; } = new List<InnerItem>();
        }

        public class InnerItem
        {
            public int item_type_id { get; set; }
            public int singleton { get; set; }
            public int flag { get; set; }
            public long quantity_destroyed { get; set; }
            public long quantity_dropped { get; set; }
        }

        public class Victim
        {
            public int damage_taken { get; set; }
            public int ship_type_id { get; set; }
            public int? character_id { get; set; }
            public int? corporation_id { get; set; }
            public int? alliance_id { get; set; }
            public int? faction_id { get; set; }
            [JsonIgnore]
            public virtual List<Item> items { get; set; } = new List<Item>();
            public Position position { get; set; }
        }

        public class Attacker
        {
            public float security_status { get; set; }
            public bool final_blow { get; set; }
            public int damage_done { get; set; }
            public int? character_id { get; set; }
            public int? corporation_id { get; set; }
            public int? alliance_id { get; set; }
            public int? faction_id { get; set; }
            public int? ship_type_id { get; set; }
            public int? weapon_type_id { get; set; }
        }
    }
}
