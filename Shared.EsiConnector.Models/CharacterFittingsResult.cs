using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/fittings/
    /// </summary>
    public class CharacterFittingsResult:List<CharacterFittingsResult.CharacterFittingsItem>, ISsoResult
    {
        public class Item
        {
            public int type_id { get; set; }
            public EFlag flag { get; set; }
            public int quantity { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EFlag : byte
        {
            Cargo = 1,
            DroneBay = 2,
            FighterBay = 3,
            HiSlot0 = 4,
            HiSlot1 = 5,
            HiSlot2 = 6,
            HiSlot3 = 7,
            HiSlot4 = 8,
            HiSlot5 = 9,
            HiSlot6 = 10,
            HiSlot7 = 11,
            Invalid = 12,
            LoSlot0 = 13,
            LoSlot1 = 14,
            LoSlot2 = 15,
            LoSlot3 = 16,
            LoSlot4 = 17,
            LoSlot5 = 18,
            LoSlot6 = 19,
            LoSlot7 = 20,
            MedSlot0 = 21,
            MedSlot1 = 22,
            MedSlot2 = 23,
            MedSlot3 = 24,
            MedSlot4 = 25,
            MedSlot5 = 26,
            MedSlot6 = 27,
            MedSlot7 = 28,
            RigSlot0 = 29,
            RigSlot1 = 30,
            RigSlot2 = 31,
            ServiceSlot0 = 32,
            ServiceSlot1 = 33,
            ServiceSlot2 = 34,
            ServiceSlot3 = 35,
            ServiceSlot4 = 36,
            ServiceSlot5 = 37,
            ServiceSlot6 = 38,
            ServiceSlot7 = 39,
            SubSystemSlot0 = 40,
            SubSystemSlot1 = 41,
            SubSystemSlot2 = 42,
            SubSystemSlot3 = 43
        }
        public class CharacterFittingsItem
        {
            public int fitting_id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public int ship_type_id { get; set; }
            public List<Item> items { get; set; } = new List<Item>();
        }
    }
}
