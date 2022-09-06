using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/alliancehistory/
    /// </summary>
    public class CorporationAllianceHistoryResult:List<CorporationAllianceHistoryResult.CorporationAllianceHistoryItem>, ISsoResult
    {
        public class CorporationAllianceHistoryItem
        {
            //[JsonPropertyName("s")]
            public DateTime start_date { get; set; }
            //[JsonPropertyName("r")]
            //[JsonIgnore]
            public int record_id { get; set; }
            //[JsonPropertyName("p")]
            public int alliance_id { get; set; }
            //[JsonPropertyName("d")]
            public bool? is_deleted { get; set; }
        }
    }
}
