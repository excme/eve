using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class CorporationAllianceHistory
    {
        [JsonPropertyName("i")]
        public int corporation_id { get; set; }
        [JsonPropertyName("h")]
        public IEnumerable<AllianceHistoryItem> ally_history { get; set; }
        public class AllianceHistoryItem{
            [JsonPropertyName("ai")]
            public int alliance_id { get; set; }
            [JsonPropertyName("an")]
            public string alliance_name { get; set; }
            [JsonPropertyName("s")]
            public DateTime start_date { get; set; }
            [JsonPropertyName("e")]
            public DateTime? end_date { get; set; }
            [JsonPropertyName("d")]
            public bool? deleted { get; set; }
        }
    }
}
