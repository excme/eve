using eveDirect.Databases.Contexts.Public.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class CorporationPreview : EveOnlineCorporationPreview
    {
        [JsonPropertyName("n")]
        public string corporation_name { get; set; }
        [JsonPropertyName("cl")]
        public bool? is_closed { get; set; }
        [JsonPropertyName("s")]
        public double sec_status { get; set; }

        [JsonPropertyName("ci")]
        public int? ceo_id { get; set; }
        [JsonPropertyName("cn")]
        public string ceo_name { get; set; }

        [JsonPropertyName("b")]
        public bool? npc { get; set; }

        [JsonPropertyName("ai")]
        public int? alliance_id { get; set; }
        [JsonPropertyName("an")]
        public string alliance_name { get; set; }
        [JsonPropertyName("mc")]
        public int? member_count { get; set; }

        public CorporationPreview()
        {

        }
        public CorporationPreview(EveOnlineCorporationPreview corporationPreview)
        {
            killmails_count = corporationPreview.killmails_count;
            contracts_count = corporationPreview.contracts_count;
            ally_movings = corporationPreview.ally_movings;
            industry_jobs_count = corporationPreview.industry_jobs_count;
            members_online_last_7d = corporationPreview.members_online_last_7d;
        }
    }
}
