using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class UniverseTypeDetail : NameModel<int>
    {
        [JsonPropertyName("t")]
        public List<string> img_tages { get; set; }
    }
}
