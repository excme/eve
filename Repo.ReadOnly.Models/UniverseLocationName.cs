using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class UniverseLocationName : NameModel<long>
    {
        [JsonPropertyName("s")]
        public double? sec_status { get; set; }
    }
}
