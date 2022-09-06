using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /incursions/
    /// </summary>
    public class IncursionResult:List<IncursionResult.IncursionItem>, ISsoResult
    {
        public class IncursionItem
        {
            public string type { get; set; }
            public EState state { get; set; }
            public float influence { get; set; }
            public bool has_boss { get; set; }
            public int faction_id { get; set; }
            public int constellation_id { get; set; }
            public int staging_solar_system_id { get; set; }
            [NotMapped]
            public List<int> infested_solar_systems { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EState : byte
        {
            withdrawing = 1,
            mobilizing = 2,
            established = 3
        }
    }
}
