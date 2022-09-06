using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class CharacterCorporationHistoryModel
    {
        [JsonPropertyName("i")]
        public int character_id { get; set; }
        [JsonPropertyName("h")]
        public IEnumerable<CorporationHistoryItem> corp_history { get; set; }
        public class CorporationHistoryItem
        {
            //[JsonPropertyName("r")]
            //public int record_id { get; set; }
            [JsonPropertyName("ci")]
            public int corporation_id { get; set; }
            //[JsonPropertyName("cn")]
            //public string corporation_name { get; set; }
            [JsonPropertyName("s")]
            public DateTime start_date { get; set; }
            [JsonPropertyName("d")]
            public bool? deleted { get; set; }
        }
    }
}
