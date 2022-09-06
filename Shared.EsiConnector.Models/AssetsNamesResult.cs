using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// POST /characters/{character_id}/assets/names/
    /// POST /corporations/{corporation_id}/assets/names/
    /// </summary>
    public class AssetsNamesResult:List<AssetsNamesResult.AssetsNamesItem>, ISsoResult
    {
        public class AssetsNamesItem
        {
            public long item_id { get; set; }
            public string name { get; set; }
        }
    }
}
