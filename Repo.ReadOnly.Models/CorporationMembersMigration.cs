using System;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class CorporationMembersMigrationItem 
    {
        [JsonPropertyName("i")]
        public int character_id { get; set; }
        [JsonPropertyName("u")]
        public string character_name { get; set; }

        [JsonPropertyName("t")]
        public EMemberMigrationType type { get; set; }
        
        [JsonPropertyName("r")]
        public int cur_corp_id { get; set; }
        [JsonPropertyName("f")]
        public string cur_corp_name { get; set; }

        [JsonPropertyName("b")]
        public int? next_corp_id { get; set; }
        [JsonPropertyName("m")]
        public string next_corp_name { get; set; }

        [JsonPropertyName("j")]
        public int? prev_corp_id { get; set; }
        [JsonPropertyName("k")]
        public string prev_corp_name { get; set; }

        [JsonPropertyName("s")]
        public DateTime start_date { get; set; }
        [JsonPropertyName("e")]
        public DateTime? end_date { get; set; }
    }

    public enum EMemberMigrationType : byte
    {
        joined = 0,
        unjoin = 1
    }
}
