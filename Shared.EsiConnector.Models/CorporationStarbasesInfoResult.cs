using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/starbases/{starbase_id}/
    /// </summary>
    public class CorporationStarbasesInfoResult:ISsoResult
    {
        public bool allow_alliance_members { get; set; }
        public bool allow_corporation_members { get; set; }
        public EType anchor { get; set; }
        public bool attack_if_at_war { get; set; }
        public bool attack_if_other_security_status_dropping { get; set; }
        public float attack_security_status_threshold { get; set; }
        public float attack_standing_threshold { get; set; }
        public EType fuel_bay_take { get; set; }
        public EType fuel_bay_view { get; set; }
        public List<Fuel> fuels { get; set; }
        public EType online { get; set; }
        public EType offline { get; set; }
        public EType unanchor { get; set; }
        public bool use_alliance_standings { get; set; }

        public class Fuel
        {
            public int type_id { get; set; }
            public int quantity { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EType : byte
        {
            alliance_member=1,
            config_starbase_equipment_role=2,
            corporation_member=3,
            starbase_fuel_technician_role=4
        }
    }
}
