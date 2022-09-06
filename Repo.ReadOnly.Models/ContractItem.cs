using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class ContractItem //: ContractsItemsResult.ContractsItem
    {
        [JsonPropertyName("b")]
        public bool is_blueprint_copy { get; set; }
        [JsonPropertyName("ii")]
        public long item_id { get; set; }
        [JsonPropertyName("m")]
        public int material_efficiency { get; set; }
        [JsonPropertyName("r")]
        public int runs { get; set; }
        [JsonPropertyName("te")]
        public int time_efficiency { get; set; }
        [JsonPropertyName("i")]
        public long record_id { get; set; }
        [JsonPropertyName("q")]
        public int quantity { get; set; }
        [JsonPropertyName("t")]
        public int type_id { get; set; }
        [JsonPropertyName("in")]
        public bool is_included { get; set; }
    }
}
