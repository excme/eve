using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class MarketRegionModel : NameModel<long>
    {
        [JsonPropertyName("s")]
        public List<NameModel<long>> systems { get; set; }
        //[JsonPropertyName("p")]
        //public int? parent_id { get; set; }
    }
}
