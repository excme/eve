using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// POST /characters/{character_id}/assets/locations/
    /// POST /corporations/{corporation_id}/assets/locations/
    /// </summary>
    public class AssetsLocationsResult:List<AssetsLocationsResult.AssetsLocationsItem>, ISsoResult
    {
        public class AssetsLocationsItem
        {
            public long item_id { get; set; }
            public Position position { get; set; }
        }
    }
}
