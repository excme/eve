using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /fleets/{fleet_id}/wings/
    /// </summary>
    public class FleetWingsResult:List<FleetWingsResult.FleetWingsItem>
    {
        public class Squad
        {
            public string name { get; set; }
            public long id { get; set; }
        }

        public class FleetWingsItem
        {
            public string name { get; set; }
            public long id { get; set; }
            public List<Squad> squads { get; set; }
        }
    }
}
