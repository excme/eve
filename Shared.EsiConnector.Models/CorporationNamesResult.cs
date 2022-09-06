using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/names/
    /// </summary>
    public class CorporationNamesResult:List<CorporationNamesResult.CorporationNamesItem>
    {
        public class CorporationNamesItem
        {
            public int corporation_id { get; set; }
            public string corporation_name { get; set; }
        }
    }
}
