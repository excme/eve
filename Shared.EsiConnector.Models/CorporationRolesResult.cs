using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/roles/
    /// </summary>
    public class CorporationRolesResult:List<CorporationRolesResult.CorporationRolesItem>, ISsoResult
    {
        public class CorporationRolesItem
        {
            public CorporationRolesItem() { }
            public int character_id { get; set; }
            public List<ERole> roles { get; set; } = new List<ERole>();
            [NotMapped]
            public List<ERole> grantable_roles { get; set; } = new List<ERole>();
            [NotMapped]
            public List<ERole> roles_at_hq { get; set; } = new List<ERole>();
            [NotMapped]
            public List<ERole> grantable_roles_at_hq { get; set; } = new List<ERole>();
            [NotMapped]
            public List<ERole> roles_at_base { get; set; } = new List<ERole>();
            [NotMapped]
            public List<ERole> grantable_roles_at_base { get; set; } = new List<ERole>();
            [NotMapped]
            public List<ERole> roles_at_other { get; set; } = new List<ERole>();
            [NotMapped]
            public List<ERole> grantable_roles_at_other { get; set; } = new List<ERole>();
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ERoleType : byte
        {
            grantable_roles = 1,
            grantable_roles_at_base = 2,
            grantable_roles_at_hq = 3,
            grantable_roles_at_other = 4,
            roles = 5,
            roles_at_base = 6,
            roles_at_hq = 7,
            roles_at_other = 8
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ERole : byte
        {
            Account_Take_1 = 1,
            Account_Take_2 = 2,
            Account_Take_3 = 3,
            Account_Take_4 = 4,
            Account_Take_5 = 5,
            Account_Take_6 = 6,
            Account_Take_7 = 7,
            Accountant = 8,
            Auditor = 9,
            Communications_Officer = 10,
            Config_Equipment = 11,
            Config_Starbase_Equipment = 12,
            Container_Take_1 = 13,
            Container_Take_2 = 14,
            Container_Take_3 = 15,
            Container_Take_4 = 16,
            Container_Take_5 = 17,
            Container_Take_6 = 18,
            Container_Take_7 = 19,
            Contract_Manager = 20,
            Diplomat = 21,
            Director = 22,
            Factory_Manager = 23,
            Fitting_Manager = 24,
            Hangar_Query_1 = 25,
            Hangar_Query_2 = 26,
            Hangar_Query_3 = 27,
            Hangar_Query_4 = 28,
            Hangar_Query_5 = 29,
            Hangar_Query_6 = 30,
            Hangar_Query_7 = 31,
            Hangar_Take_1 = 32,
            Hangar_Take_2 = 33,
            Hangar_Take_3 = 34,
            Hangar_Take_4 = 35,
            Hangar_Take_5 = 36,
            Hangar_Take_6 = 37,
            Hangar_Take_7 = 38,
            Junior_Accountant = 39,
            Personnel_Manager = 40,
            Rent_Factory_Facility = 41,
            Rent_Office = 42,
            Rent_Research_Facility = 43,
            Security_Officer = 44,
            Starbase_Defense_Operator = 45,
            Starbase_Fuel_Technician = 46,
            Station_Manager = 47,
            Trader = 48
        }
    }
}
