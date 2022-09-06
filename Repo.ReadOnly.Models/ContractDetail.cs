using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class ContractDetail //: ContractsResult.Contract
    {
        [JsonPropertyName("b")]
        public double buyout { get; set; }
        [JsonPropertyName("c")]
        public double collateral { get; set; }
        //[JsonPropertyName("i")]
        //public int contract_id { get; set; }
        [JsonPropertyName("de")]
        public DateTime? date_expired { get; set; }
        [JsonPropertyName("di")]
        public DateTime? date_issued { get; set; }
        [JsonPropertyName("dc")]
        public int days_to_complete { get; set; }
        [JsonPropertyName("el")]
        public long end_location_id { get; set; }
        [JsonPropertyName("fc")]
        public bool for_corporation { get; set; }
        [JsonPropertyName("ic")]
        public int issuer_corporation_id { get; set; }
        [JsonPropertyName("ii")]
        public int issuer_id { get; set; }
        [JsonPropertyName("p")]
        public double price { get; set; }
        [JsonPropertyName("r")]
        public double reward { get; set; }
        [JsonPropertyName("sl")]
        public long start_location_id { get; set; }
        [JsonPropertyName("z")]
        public string title { get; set; }
        [JsonPropertyName("t")]
        public byte type { get; set; }
        [JsonPropertyName("v")]
        public double volume { get; set; }
    }
}
