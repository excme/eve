using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporation/{corporation_id}/mining/extractions/
    /// </summary>
    public class CorporationIndustryMiningExtractionsResult:List<CorporationIndustryMiningExtractionsResult.CorporationMiningExtractionsItem>, ISsoResult
    {
        public class CorporationMiningExtractionsItem
        {
            public long structure_id { get; set; }
            public int moon_id { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime extraction_start_time { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime chunk_arrival_time { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime natural_decay_time { get; set; }
        }
    }
}
