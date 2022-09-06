using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Результат GET /characters/{character_id}/assets/
    /// </summary>
    public class CharacterAssetsResult :List<CharacterAssetsResult.AssetItem>, ISsoResult
    {
        public class AssetItem
        {
            public long location_id { get; set; }
            public int type_id { get; set; }
            public long item_id { get; set; }
            public ELocationType location_type { get; set; }
            public int quantity { get; set; }
            [NotMapped]
            public ElocationFlag location_flag { get; set; }
            public bool is_singleton { get; set; }
            public bool is_blueprint_copy { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ELocationType : byte
        {
            station = 1,
            solar_system = 2,
            other = 3
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ElocationFlag : byte
        {
            AssetSafety = 1,
            AutoFit = 2,
            BoosterBay = 3,
            Cargo = 4,
            CorpseBay = 5,
            Deliveries = 6,
            DroneBay = 7,
            FighterBay = 8,
            FighterTube0 = 9,
            FighterTube1 = 10,
            FighterTube2 = 11,
            FighterTube3 = 12,
            FighterTube4 = 13,
            FleetHangar = 14,
            Hangar = 15,
            HangarAll = 16,
            HiSlot0 = 17,
            HiSlot1 = 18,
            HiSlot2 = 19,
            HiSlot3 = 20,
            HiSlot4 = 21,
            HiSlot5 = 22,
            HiSlot6 = 23,
            HiSlot7 = 24,
            HiddenModifiers = 25,
            Implant = 26,
            LoSlot0 = 27,
            LoSlot1 = 28,
            LoSlot2 = 29,
            LoSlot3 = 30,
            LoSlot4 = 31,
            LoSlot5 = 32,
            LoSlot6 = 33,
            LoSlot7 = 34,
            Locked = 35,
            MedSlot0 = 36,
            MedSlot1 = 37,
            MedSlot2 = 38,
            MedSlot3 = 39,
            MedSlot4 = 40,
            MedSlot5 = 41,
            MedSlot6 = 42,
            MedSlot7 = 43,
            QuafeBay = 44,
            RigSlot0 = 45,
            RigSlot1 = 46,
            RigSlot2 = 47,
            RigSlot3 = 48,
            RigSlot4 = 49,
            RigSlot5 = 50,
            RigSlot6 = 51,
            RigSlot7 = 52,
            ShipHangar = 53,
            Skill = 54,
            SpecializedAmmoHold = 55,
            SpecializedCommandCenterHold = 56,
            SpecializedFuelBay = 57,
            SpecializedGasHold = 58,
            SpecializedIndustrialShipHold = 59,
            SpecializedLargeShipHold = 60,
            SpecializedMaterialBay = 61,
            SpecializedMediumShipHold = 62,
            SpecializedMineralHold = 63,
            SpecializedOreHold = 64,
            SpecializedPlanetaryCommoditiesHold = 65,
            SpecializedSalvageHold = 66,
            SpecializedShipHold = 67,
            SpecializedSmallShipHold = 68,
            SubSystemBay = 69,
            SubSystemSlot0 = 70,
            SubSystemSlot1 = 71,
            SubSystemSlot2 = 72,
            SubSystemSlot3 = 73,
            SubSystemSlot4 = 74,
            SubSystemSlot5 = 75,
            SubSystemSlot6 = 76,
            SubSystemSlot7 = 77,
            Unlocked = 78,
            Wardrobe = 79
        }
    }

    /// <summary>
    /// Результат GET /corporations/{corporation_id}/assets/
    /// </summary>
    public class CorporationAssetsResult : List<CorporationAssetsResult.AssetItem>, ISsoResult
    {
        public class AssetItem
        {
            public long location_id { get; set; }
            public int type_id { get; set; }
            public long item_id { get; set; }
            public ELocationType location_type { get; set; }
            public int quantity { get; set; }
            [NotMapped]
            public ElocationFlag location_flag { get; set; }
            public bool is_singleton { get; set; }
            public bool is_blueprint_copy { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ELocationType : byte
        {
            station = 1,
            solar_system = 2,
            other = 3
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ElocationFlag : byte
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
            Wardrobe = 115,
        }
    }

    /// <summary>
    /// Результат POST /characters/{character_id}/assets/names/
    /// Результат POST /corporations/{corporation_id}/assets/names/
    /// </summary>
    public class AssetNameResult : List<AssetNameResult.AssetName>
    {
        public class AssetName
        {
            public long item_id { get; set; }
            public string name { get; set; }
        }
    }
    /// <summary>
    /// Результат POST /characters/{character_id}/assets/locations/
    /// Результат POST /corporations/{corporation_id}/assets/locations/
    /// </summary>
    public class AssetLocationResult : List<AssetLocationResult.AssetLocation>
    {
        public class AssetLocation
        {
            public long item_id { get; set; }
            public Position position { get; set; }
        }
    }
}
