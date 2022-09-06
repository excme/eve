using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Post /fleets/{fleet_id}/wings/
    /// </summary>
    public class FleetAddingResult
    {
        public long wing_id { get; set; }
    }
}
