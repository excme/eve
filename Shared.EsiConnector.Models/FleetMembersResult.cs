using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /fleets/{fleet_id}/members/
    /// </summary>
    public class FleetMembersResult:List<FleetMembersResult.FleetMembersItem>
    {
        public class FleetMembersItem
        {
            public int character_id { get; set; }
            public int ship_type_id { get; set; }
            public long wing_id { get; set; }
            public long squad_id { get; set; }
            public ERole role { get; set; }
            public string role_name { get; set; }
            public DateTime join_time { get; set; }
            public bool takes_fleet_warp { get; set; }
            public int solar_system_id { get; set; }
            public long station_id { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ERole : byte
        {
            fleet_commander = 1,
            wing_commander = 2,
            squad_commander = 3,
            squad_member = 4
        }
    }
}
