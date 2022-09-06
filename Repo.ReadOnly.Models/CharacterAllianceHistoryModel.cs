using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class CharacterAllianceHistoryModel
    {
        [JsonPropertyName("i")]
        public int character_id { get; set; }
        [JsonPropertyName("h")]
        public IEnumerable<AllianceHistoryItem> ally_history { get; set; }
        public class AllianceHistoryItem
        {
            [JsonPropertyName("e")]
            public DateTime? end { get; set; }
            [JsonPropertyName("s")] 
            public DateTime start { get; set; }
            [JsonPropertyName("ci")]
            public int corporation_id { get; set; }
            [JsonPropertyName("ai")] 
            public int alliance_id { get; set; }
        }
    }
}
