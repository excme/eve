using System.Text.Json.Serialization;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineCharacterPreview
    {
        [JsonPropertyName("k")]
        public int? killmails_count { get; set; }
        [JsonPropertyName("co")]
        public int? contracts_count { get; set; }
        [JsonPropertyName("cm")]
        public int? corp_movings { get; set; }
        [JsonPropertyName("o")]
        public int? private_orders_count { get; set; }
        [JsonPropertyName("i")]
        public int? industry_jobs_count { get; set; }
        [JsonPropertyName("ol")]
        public int? online_last_7d { get; set; }
    }
}
