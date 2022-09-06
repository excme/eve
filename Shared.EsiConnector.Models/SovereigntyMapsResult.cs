using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /sovereignty/map/
    /// </summary>
    public class SovereigntyMapsResult:List<SovereigntyMapsResult.SovereigntyMapsItem>, ISsoResult
    {
        public class SovereigntyMapsItem
        {
            public int system_id { get; set; }
            public int? faction_id { get; set; }
            public int? alliance_id { get; set; }
            public int? corporation_id { get; set; }
        }
    }
}
