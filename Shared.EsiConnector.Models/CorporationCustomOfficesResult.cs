using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/customs_offices/
    /// </summary>
    public class CorporationCustomOfficesResult : List<CorporationCustomOfficesResult.CorporationCustomOfficesItem>, ISsoResult { 
        public class CorporationCustomOfficesItem
        {
            public long office_id { get; set; }
            public int system_id { get; set; }
            public int reinforce_exit_start { get; set; }
            public int reinforce_exit_end { get; set; }
            public bool allow_alliance_access { get; set; }
            public bool allow_access_with_standings { get; set; }
            public float corporation_tax_rate { get; set; }
            public EStandingLevel standing_level { get; set; }
            public float excellent_standing_tax_rate { get; set; }
            public float good_standing_tax_rate { get; set; }
            public float neutral_standing_tax_rate { get; set; }
            public float bad_standing_tax_rate { get; set; }
            public float terrible_standing_tax_rate { get; set; }
            public float alliance_tax_rate { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EStandingLevel : byte
        {
            bad = 1,
            excellent = 2,
            good = 3,
            neutral = 4, 
            terrible = 5
        }
    }
}
