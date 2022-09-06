using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineAlliancePreview
    {
        [JsonPropertyName("k")]
        public int killmails_count { get; set; }
    }
}
