using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    ///  Post /fleets/{fleet_id}/wings/{wing_id}/squads/ 
    /// </summary>
    public class FleetAddingSquadResult
    {
        public long squad_id { get; set; }
    }
}
