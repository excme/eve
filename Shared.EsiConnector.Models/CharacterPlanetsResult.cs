using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/planets/
    /// </summary>
    public class CharacterPlanetsResult : List<CharacterPlanetsResult.CharacterPlanetItem>, ISsoResult
    {
        public class CharacterPlanetItem
        {
            public int solar_system_id { get; set; }
            public int planet_id { get; set; }
            public EPlanetType planet_type { get; set; }
            public int owner_id { get; set; }
            public DateTime last_update { get; set; }
            public int upgrade_level { get; set; }
            public int num_pins { get; set; }
        }

        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EPlanetType : byte
        {
            temperate = 1,
            barren = 2,
            oceanic = 3,
            ice = 4,
            gas = 5,
            lava = 6,
            storm = 7,
            plasma = 8
        }
    }
}
