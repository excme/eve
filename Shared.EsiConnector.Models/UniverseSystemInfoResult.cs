using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/systems/{system_id}/
    /// </summary>
    public class UniverseSystemInfoResult: ISsoResult
    {
        //[JsonIgnore]
        public int constellation_id { get; set; }
        //[JsonIgnore]
        public string name { get; set; }
        public int star_id { get; set; }
        public string security_class { get; set; }
        public float security_status { get; set; }
        //[JsonIgnore]
        public int system_id { get; set; }
        public List<Planet> planets { get; set; }
        //[JsonIgnore]
        public Position position { get; set; }
        public List<int> stargates { get; set; }
        public List<int> stations { get; set; }

        public class Planet
        {
            //[JsonPropertyName("a")]
            public List<int> asteroid_belts { get; set; }
            //[JsonPropertyName("p")]
            public int planet_id { get; set; }
            //[JsonPropertyName("m")]
            public List<int> moons { get; set; }
        }
    }
}
