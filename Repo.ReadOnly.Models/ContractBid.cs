using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class ContractBid //: ContractsBidsResult.ContractsBidsItem
    {
        [JsonPropertyName("i")]
        public int bid_id { get; set; }
        [JsonPropertyName("d")]
        public DateTime? date_bid { get; set; }
        [JsonPropertyName("a")]
        public float amount { get; set; }
        [JsonPropertyName("q")]
        public bool? disabled { get; set; }
    }
}
