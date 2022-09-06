using System.Text.Json.Serialization;
namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineCorporationPreview
    {
        [JsonPropertyName("k")]
        public int? killmails_count { get; set; }
        [JsonPropertyName("co")]
        public int? contracts_count { get; set; }
        [JsonPropertyName("am")]
        public int? ally_movings { get; set; }
        [JsonPropertyName("i")]
        public int? industry_jobs_count { get; set; }
        [JsonPropertyName("mo")]
        public int? members_online_last_7d { get; set; }
    }
}
