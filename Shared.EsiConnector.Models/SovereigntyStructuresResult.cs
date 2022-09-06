using System;
using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /sovereignty/structures/
    /// </summary>
    public class SovereigntyStructuresResult:List<SovereigntyStructuresResult.SovereigntyStructuresItem>, ISsoResult
    {
        public class SovereigntyStructuresItem
        {
            public int alliance_id { get; set; }
            public int solar_system_id { get; set; }
            public long structure_id { get; set; }
            public int structure_type_id { get; set; }
            public float vulnerability_occupancy_level { get; set; }
            public DateTime vulnerable_start_time { get; set; }
            public DateTime vulnerable_end_time { get; set; }
        }
    }
}
