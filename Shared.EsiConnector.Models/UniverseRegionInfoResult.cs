using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/regions/{region_id}/
    /// </summary>
    public class UniverseRegionInfoResult: ISsoResult
    {
        //[JsonIgnore]
        public int region_id { get; set; }
        //[JsonIgnore] 
        public string name { get; set; }
        public string description { get; set; }
        public List<int> constellations { get; set; }
    }
}
