using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/planets/{planet_id}/
    /// </summary>
    public class UniversePlanetInfoResult: ISsoResult
    {
        //[JsonIgnore]
        public int planet_id { get; set; }
        //[JsonIgnore]
        public string name { get; set; }
        public int type_id { get; set; }
        //[JsonIgnore]
        public Position position { get; set; }
        //[JsonIgnore]
        public int system_id { get; set; }
    }
}
