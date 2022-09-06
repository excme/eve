using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/moons/{moon_id}/
    /// </summary>
    public class UniverseMoonInfoResult: ISsoResult
    {
        //[JsonIgnore]
        public int moon_id { get; set; }
        //[JsonIgnore]
        public string name { get; set; }
        //[JsonIgnore]
        public Position position { get; set; }
        //[JsonIgnore]
        public int system_id { get; set; }
    }
}
