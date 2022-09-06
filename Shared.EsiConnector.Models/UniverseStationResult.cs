using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/stations/{station_id}/
    /// </summary>
    public class UniverseStationInfoResult: ISsoResult
    {
        //[JsonIgnore]
        public int station_id { get; set; }
        //[JsonIgnore]
        public string name { get; set; }
        public int type_id { get; set; }
        //[JsonIgnore]
        public Position position { get; set; }
        //[JsonIgnore]
        public int system_id { get; set; }
        public float reprocessing_efficiency { get; set; }
        public float reprocessing_stations_take { get; set; }
        public float max_dockable_ship_volume { get; set; }
        public float office_rental_cost { get; set; }
        public List<EService> services { get; set; }
        public int owner { get; set; }
        public int race_id { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EService : byte
        {
            [EnumMember(Value = "bounty-missions")]
            bounty_missions = 1,
            [EnumMember(Value = "assasination-missions")] 
            assasination_missions = 2,
            [EnumMember(Value = "courier-missions")]
            courier_missions = 3,
            [EnumMember(Value = "interbus")] 
            interbus = 4,
            [EnumMember(Value = "reprocessing-plant")]
            reprocessing_plant = 5,
            [EnumMember(Value = "refinery")] 
            refinery = 6,
            [EnumMember(Value = "market")]
            market = 7,
            [EnumMember(Value = "black-market")] 
            black_market = 8,
            [EnumMember(Value = "stock-exchange")] 
            stock_exchange = 9,
            [EnumMember(Value = "cloning")] 
            cloning = 10,
            [EnumMember(Value = "surgery")]
            surgery = 11,
            [EnumMember(Value = "dna-therapy")]
            dna_therapy = 12,
            [EnumMember(Value = "repair-facilities")] 
            repair_facilities = 13,
            [EnumMember(Value = "factory")] 
            factory = 14,
            [EnumMember(Value = "labratory")] 
            labratory = 15,
            [EnumMember(Value = "gambling")] 
            gambling = 16,
            [EnumMember(Value = "fitting")] 
            fitting = 17,
            [EnumMember(Value = "paintshop")] 
            paintshop = 18,
            [EnumMember(Value = "news")] 
            news = 19,
            [EnumMember(Value = "storage")] 
            storage = 20,
            [EnumMember(Value = "insurance")] 
            insurance = 21,
            [EnumMember(Value = "docking")] 
            docking = 22,
            [EnumMember(Value = "office-rental")] 
            office_rental = 23,
            [EnumMember(Value = "jump-clone-facility")] 
            jump_clone_facility = 24,
            [EnumMember(Value = "loyalty-point-store")] 
            loyalty_point_store = 25,
            [EnumMember(Value = "navy-offices")] 
            navy_offices = 26,
            [EnumMember(Value = "security-offices")]
            security_offices = 27
        }
    }
}
