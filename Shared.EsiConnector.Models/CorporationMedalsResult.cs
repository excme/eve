using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/medals/
    /// </summary>
    public class CorporationMedalsResult:List<CorporationMedalsResult.CorporationMedalsItem>, ISsoResult
    {
        public class CorporationMedalsItem
        {
            public int medal_id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public int creator_id { get; set; }
            public DateTime created_at { get; set; }
        }
    }
}
