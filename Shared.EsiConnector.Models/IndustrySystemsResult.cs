using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /industry/systems/
    /// </summary>
    public class IndustrySystemsResult:List<IndustrySystemsResult.IndustrySystemsItem>, ISsoResult
    {
        public class CostIndice
        {
            public EActivity activity { get; set; }
            public float cost_index { get; set; }
        }

        public class IndustrySystemsItem
        {
            public int solar_system_id { get; set; }
            public List<CostIndice> cost_indices { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EActivity : byte
        {
            copying = 1,
            duplicating = 2,
            invention = 3,
            manufacturing = 4,
            none = 5,
            reaction = 6,
            researching_material_efficiency = 7,
            researching_technology = 8,
            researching_time_efficiency = 9,
            reverse_engineering = 10
        }
    }
}
