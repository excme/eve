using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Post /universe/names/
    /// </summary>
    public class UniverseNamesResult : List<UniverseNamesResult.UniverseNameItem>, ISsoResult
    {
        public class UniverseNameItem
        {
            public int id { get; set; }
            public string name { get; set; }
            public EResolvedInfoCategory category { get; set; }
        }

        //[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EResolvedInfoCategory : byte
        {
            alliance = 1,
            character = 2,
            constellation = 3,
            corporation = 4 ,
            inventory_type = 5,
            region = 6,
            solar_system = 7,
            station = 8,
            faction = 9,
        }
    }
}
