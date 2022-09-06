using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporation/{corporation_id}/mining/observers/
    /// </summary>
    public class CorporationIndustryMiningObserversResult:List<CorporationIndustryMiningObserversResult.CorporationIndustryMiningObserversItem>, ISsoResult
    {
        public class CorporationIndustryMiningObserversItem
        {
            
            public DateTime last_updated { get; set; }
            public long observer_id { get; set; }
            public EObserverType observer_type { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EObserverType : byte
        {
            structure = 1
        }
    }
}
