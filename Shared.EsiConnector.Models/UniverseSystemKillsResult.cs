using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/system_kills/
    /// </summary>
    public class UniverseSystemKillsResult:List<UniverseSystemKillsResult.UniverseSystemKillsItem>, ISsoResult
    {
        public class UniverseSystemKillsItem
        {
            public int system_id { get; set; }
            public int ship_kills { get; set; }
            public int npc_kills { get; set; }
            public int pod_kills { get; set; }
        }
    }
}
