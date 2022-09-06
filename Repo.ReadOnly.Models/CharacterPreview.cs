using eveDirect.Databases.Contexts.Public.Models;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class CharacterPreview : EveOnlineCharacterPreview
    {
        [JsonPropertyName("s")]
        public double sec_status { get; set; }
        [JsonPropertyName("n")]
        public string character_name { get; set; }
        [JsonPropertyName("ci")]
        public int? corporation_id { get; set; }
        [JsonPropertyName("cn")]
        public string corporation_name { get; set; }
        [JsonPropertyName("ai")]
        public int? alliance_id { get; set; }
        [JsonPropertyName("an")]
        public string alliance_name { get; set; }
        public CharacterPreview() { }
        public CharacterPreview(EveOnlineCharacterPreview characterPreview)
        {
            killmails_count = characterPreview.killmails_count;
            contracts_count = characterPreview.contracts_count;
            corp_movings = characterPreview.corp_movings;
            private_orders_count = characterPreview.private_orders_count;
            industry_jobs_count = characterPreview.industry_jobs_count;
            online_last_7d = characterPreview.online_last_7d;
        }
    }
}
