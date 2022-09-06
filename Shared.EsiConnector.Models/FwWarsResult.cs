using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /fw/wars/
    /// </summary>
    public class FwWarsResult : List<FwWarsResult.FwWarsItem>, ISsoResult
    {
        public class FwWarsItem
        {
            public int faction_id { get; set; }
            public int against_id { get; set; }
        }
    }
}
