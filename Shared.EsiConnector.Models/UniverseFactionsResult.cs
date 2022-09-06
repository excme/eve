using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/factions/
    /// </summary>
    public class UniverseFactionsResult : List<UniverseFactionsResult.UniverseFactionsItem>, ISsoResult
    {
        public class UniverseFactionsItem
        {
            public int faction_id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public float size_factor { get; set; }
            public int station_count { get; set; }
            public int station_system_count { get; set; }
            public bool is_unique { get; set; }
            public int solar_system_id { get; set; }
            public int corporation_id { get; set; }
            public int militia_corporation_id { get; set; }
        }
    }
}
