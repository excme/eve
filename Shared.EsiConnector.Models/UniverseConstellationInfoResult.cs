using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/constellations/{constellation_id}/
    /// </summary>
    public class UniverseConstellationInfoResult: ISsoResult
    {
        //[JsonIgnore]
        public int constellation_id { get; set; }
        //[JsonIgnore]
        public string name { get; set; }
        //[JsonIgnore]
        public Position position { get; set; }
        //[JsonIgnore]
        public int region_id { get; set; }
        public List<int> systems { get; set; }
    }
}
