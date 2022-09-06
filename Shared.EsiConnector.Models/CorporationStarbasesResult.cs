using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/starbases/
    /// </summary>
    public class CorporationStarbasesResult : List<CorporationStarbasesResult.CorporationStarbasesItem>, ISsoResult {
        public class CorporationStarbasesItem
        {
            public int type_id { get; set; }
            public int system_id { get; set; }
            public int moon_id { get; set; }
            public DateTime onlined_since { get; set; }
            public DateTime reinforced_until { get; set; }
            public DateTime unanchor_at { get; set; }
            public long starbase_id { get; set; }
            public EState state { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EState : byte
        {
            offline = 1,
            online = 2,
            onlining = 3,
            reinforced = 4,
            unanchoring = 5
        }
    }
}
