using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class MarketGroupModel : NameModel<int>
    {
        //[JsonPropertyName("c")]
        //public List<MarketGroupModel> childs { get; set; }
        //[JsonPropertyName("l")]
        //public bool? lastLevel { get; set; }
        [JsonPropertyName("p")]
        public int? parent { get; set; }
    }
}
