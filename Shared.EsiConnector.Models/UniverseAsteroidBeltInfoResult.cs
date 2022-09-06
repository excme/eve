using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/asteroid_belts/{asteroid_belt_id}/
    /// </summary>
    public class UniverseAsteroidBeltInfoResult: ISsoResult
    {
        //[JsonIgnore]
        public string name { get; set; }
        //[JsonIgnore]
        public Position position { get; set; }
        //[JsonIgnore]
        public int system_id { get; set; }
    }
}
