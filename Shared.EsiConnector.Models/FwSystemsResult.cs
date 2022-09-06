using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /fw/systems/
    /// </summary>
    public class FwSystemsResult:List<FwSystemsResult.FwSystemsItem>, ISsoResult
    {
        public class FwSystemsItem
        {
            public int solar_system_id { get; set; }
            public int occupier_faction_id { get; set; }
            public int owner_faction_id { get; set; }
            public int victory_points { get; set; }
            public int victory_points_threshold { get; set; }
            public EContested contested { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EContested : byte
        {
            captured = 1,
            contested = 2,
            uncontested = 3,
            vulnerable = 4
        }
    }
}
