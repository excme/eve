using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/stargates/{stargate_id}/
    /// </summary>
    public class UniverseStargateInfoResult: ISsoResult
    {
        //[JsonIgnore]
        public int stargate_id { get; set; }
        //[JsonIgnore]
        public string name { get; set; }
        public int type_id { get; set; }
        //[JsonIgnore]
        public Position position { get; set; }
        //[JsonIgnore]
        public int system_id { get; set; }
        public Destination destination { get; set; }
        public class Destination
        {
            public int system_id { get; set; }
            public int stargate_id { get; set; }
        }
    }
}
