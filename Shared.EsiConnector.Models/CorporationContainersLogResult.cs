using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/containers/logs/
    /// </summary>
    public class CorporationContainersLogResult:List<CorporationContainersLogResult.CorporationContainersLogItem>, ISsoResult
    {
        public class CorporationContainersLogItem
        {
            public EAction action { get; set; }
            public int character_id { get; set; }
            public long container_id { get; set; }
            public int container_type_id { get; set; }
            public ELocationFlag location_flag { get; set; }
            public long location_id { get; set; }
            public DateTime logged_at { get; set; }
            public int? new_config_bitmask { get; set; }
            public int? old_config_bitmask { get; set; }
            public EPasswordType password_type { get; set; }
            public int? quantity { get; set; }
            public int? type_id { get; set; }
        }
        //[JsonConverter(typeof(StringEnumConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EAction : byte
        {
            add = 1,
            assemble = 2,
            configure =3,
            enter_password = 4,
            [EnumMember(Value = "lock")]
            _lock = 5,
            move = 6,
            repackage=7,
            set_name=8,
            set_password=9,
            unlock=10
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EPasswordType : byte
        {
            config=1,
            general=2
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ELocationFlag : byte
        {
            AssetSafety = 1,
            AutoFit = 2,
            Bonus = 3,
            Booster = 4,
            BoosterBay = 5,
            Capsule = 6,
            Cargo = 7,
            CorpDeliveries = 8,
            CorpSAG1 = 9,
            CorpSAG2 = 10,
            CorpSAG3 = 11,
            CorpSAG4 = 12,
            CorpSAG5 = 13,
            CorpSAG6 = 14,
            CorpSAG7 = 15,
            CrateLoot = 16,
            Deliveries = 17,
            DroneBay = 18,
            DustBattle = 19,
            DustDatabank = 20,
            FighterBay = 21,
            FighterTube0 = 22,
            FighterTube1 = 23,
            FighterTube2 = 24,
            FighterTube3 = 25,
            FighterTube4 = 26,
            FleetHangar = 27,
            Hangar = 28,
            HangarAll = 29,
            HiSlot0 = 30,
            HiSlot1 = 31,
            HiSlot2 = 32,
            HiSlot3 = 33,
            HiSlot4 = 34,
            HiSlot5 = 35,
            HiSlot6 = 36,
            HiSlot7 = 37,
            HiddenModifiers = 38,
            Implant = 39,
            Impounded = 40,
            JunkyardReprocessed = 41,
            JunkyardTrashed = 42,
            LoSlot0 = 43,
            LoSlot1 = 44,
            LoSlot2 = 45,
            LoSlot3 = 46,
            LoSlot4 = 47,
            LoSlot5 = 48,
            LoSlot6 = 49,
            LoSlot7 = 50,
            Locked = 51,
            MedSlot0 = 52,
            MedSlot1 = 53,
            MedSlot2 = 54,
            MedSlot3 = 55,
            MedSlot4 = 56,
            MedSlot5 = 57,
            MedSlot6 = 58,
            MedSlot7 = 59,
            OfficeFolder = 60,
            Pilot = 61,
            PlanetSurface = 62,
            QuafeBay = 63,
            Reward = 64,
            RigSlot0 = 65,
            RigSlot1 = 66,
            RigSlot2 = 67,
            RigSlot3 = 68,
            RigSlot4 = 69,
            RigSlot5 = 70,
            RigSlot6 = 71,
            RigSlot7 = 72,
            SecondaryStorage = 73,
            ServiceSlot0 = 74,
            ServiceSlot1 = 75,
            ServiceSlot2 = 76,
            ServiceSlot3 = 77,
            ServiceSlot4 = 78,
            ServiceSlot5 = 79,
            ServiceSlot6 = 80,
            ServiceSlot7 = 81,
            ShipHangar = 82,
            ShipOffline = 83,
            Skill = 84,
            SkillInTraining = 85,
            SpecializedAmmoHold = 86,
            SpecializedCommandCenterHold = 87,
            SpecializedFuelBay = 88,
            SpecializedGasHold = 89,
            SpecializedIndustrialShipHold = 90,
            SpecializedLargeShipHold = 91,
            SpecializedMaterialBay = 92,
            SpecializedMediumShipHold = 93,
            SpecializedMineralHold = 94,
            SpecializedOreHold = 95,
            SpecializedPlanetaryCommoditiesHold = 96,
            SpecializedSalvageHold = 97,
            SpecializedShipHold = 98,
            SpecializedSmallShipHold = 99,
            StructureActive = 100,
            StructureFuel = 101,
            StructureInactive = 102,
            StructureOffline = 103,
            SubSystemBay = 104,
            SubSystemSlot0 = 105,
            SubSystemSlot1 = 106,
            SubSystemSlot2 = 107,
            SubSystemSlot3 = 108,
            SubSystemSlot4 = 109,
            SubSystemSlot5 = 110,
            SubSystemSlot6 = 111,
            SubSystemSlot7 = 112,
            Unlocked = 113,
            Wallet = 114,
            Wardrobe = 115
        }
    }
}
