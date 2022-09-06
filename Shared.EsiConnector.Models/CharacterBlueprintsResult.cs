﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/blueprints/
    /// </summary>
    public class CharacterBlueprintsResult:List<CharacterBlueprintsResult.BlueprintsItem>, ISsoResult
    {
        public class BlueprintsItem
        {
            public long item_id { get; set; }
            public long location_id { get; set; }
            public int material_efficiency { get; set; }
            public int quantity { get; set; }
            public int runs { get; set; }
            public int time_efficiency { get; set; }
            public int type_id { get; set; }
            public ELocationFlag location_flag { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ELocationFlag : byte
        {
            AutoFit = 1,
            Cargo = 2,
            CorpseBay = 3,
            DroneBay = 4,
            FleetHangar = 5,
            Deliveries = 6,
            HiddenModifiers = 7,
            Hangar = 8,
            HangarAll = 9,
            LoSlot0 = 10,
            LoSlot1 = 11,
            LoSlot2 = 12,
            LoSlot3 = 13,
            LoSlot4 = 14,
            LoSlot5 = 15,
            LoSlot6 = 16,
            LoSlot7 = 17,
            MedSlot0 = 18,
            MedSlot1 = 19,
            MedSlot2 = 20,
            MedSlot3 = 21,
            MedSlot4 = 22,
            MedSlot5 = 23,
            MedSlot6 = 24,
            MedSlot7 = 25,
            HiSlot0 = 26,
            HiSlot1 = 27,
            HiSlot2 = 28,
            HiSlot3 = 29,
            HiSlot4 = 30,
            HiSlot5 = 31,
            HiSlot6 = 32,
            HiSlot7 = 33,
            AssetSafety = 34,
            Locked = 35,
            Unlocked = 36,
            Implant = 37,
            QuafeBay = 38,
            RigSlot0 = 39,
            RigSlot1 = 40,
            RigSlot2 = 41,
            RigSlot3 = 42,
            RigSlot4 = 43,
            RigSlot5 = 44,
            RigSlot6 = 45,
            RigSlot7 = 46,
            ShipHangar = 47,
            SpecializedFuelBay = 48,
            SpecializedOreHold = 49,
            SpecializedGasHold = 50,
            SpecializedMineralHold = 51,
            SpecializedSalvageHold = 52,
            SpecializedShipHold = 53,
            SpecializedSmallShipHold = 54,
            SpecializedMediumShipHold = 55,
            SpecializedLargeShipHold = 56,
            SpecializedIndustrialShipHold = 57,
            SpecializedAmmoHold = 58,
            SpecializedCommandCenterHold = 59,
            SpecializedPlanetaryCommoditiesHold = 60,
            SpecializedMaterialBay = 61,
            SubSystemSlot0 = 62,
            SubSystemSlot1 = 63,
            SubSystemSlot2 = 64,
            SubSystemSlot3 = 65,
            SubSystemSlot4 = 66,
            SubSystemSlot5 = 67,
            SubSystemSlot6 = 68,
            SubSystemSlot7 = 69,
            FighterBay = 70,
            FighterTube0 = 71,
            FighterTube1 = 72,
            FighterTube2 = 73,
            FighterTube3 = 74,
            FighterTube4 = 75,
            Module = 76
        }
    }
}
