using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /industry/facilities/
    /// </summary>
    public class IndustryFacilitiesResult:List<IndustryFacilitiesResult.IndustryFacilitiesItem>, ISsoResult
    {
        public class IndustryFacilitiesItem
        {
            public long facility_id { get; set; }
            public int owner_id { get; set; }
            public int type_id { get; set; }
            public int solar_system_id { get; set; }
            public int region_id { get; set; }
            public float tax { get; set; }
        }
    }
}
