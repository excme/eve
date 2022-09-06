using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporation/{corporation_id}/mining/observers/{observer_id}/
    /// </summary>
    public class CorporationIndustryMiningObserverItemResult:List<CorporationIndustryMiningObserverItemResult.CorporationIndustryMiningObserverItemInner>, ISsoResult
    {
        public class CorporationIndustryMiningObserverItemInner
        {
            [Column(TypeName = "smalldatetime")]
            public DateTime last_updated { get; set; }
            public int character_id { get; set; }
            public int recorded_corporation_id { get; set; }
            public int type_id { get; set; }
            public long quantity { get; set; }
        }
    }
}
