using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/facilities/
    /// </summary>
    public class CorporationFacilitiesResult:List<CorporationFacilitiesResult.CorporationFacilitiesItem>, ISsoResult
    {
        public class CorporationFacilitiesItem
        {
            public long facility_id { get; set; }
            public int type_id { get; set; }
            public int system_id { get; set; }
        }
    }
}
