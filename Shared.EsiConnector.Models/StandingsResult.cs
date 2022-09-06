using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/standings/
    /// GET /corporations/{corporation_id}/standings/
    /// </summary>
    public class StandingsResult:List<StandingsResult.StandingsItem>, ISsoResult
    {
        public class StandingsItem
        {
            public int from_id { get; set; }
            public EFromType from_type { get; set; }
            public float standing { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EFromType : byte
        {
            agent = 1,
            npc_corp = 2,
            faction = 3
        }
    }
}
