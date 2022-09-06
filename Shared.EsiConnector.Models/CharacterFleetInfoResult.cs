using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/fleet/
    /// </summary>
    public class CharacterFleetInfoResult
    {
        public long fleet_id { get; set; }
        public long wing_id { get; set; }
        public long squad_id { get; set; }
        public ERole role { get; set; }

        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ERole : byte
        {
            fleet_commander = 1,
            squad_commander = 2,
            squad_member = 3,
            wing_commander = 4
        }
    }
}
