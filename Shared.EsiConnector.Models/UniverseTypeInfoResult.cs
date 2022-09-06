using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/types/{type_id}/
    /// </summary>
    public class UniverseTypeInfoResult : ISsoResult
    {
        public int type_id { get; set; }
        public float capacity { get; set; }
        public string description { get; set; }
        public List<DogmaAttribute> dogma_attributes { get; set; }
        public List<DogmaEffect> dogma_effects { get; set; }
        public int? graphic_id { get; set; }
        public int group_id { get; set; }
        public int? icon_id { get; set; }
        public int? market_group_id { get; set; }
        public float mass { get; set; }
        public string name { get; set; }
        public float packaged_volume { get; set; }
        public int? portion_size { get; set; }
        public bool published { get; set; }
        public float radius { get; set; }
        public float volume { get; set; }
    }
    public class DogmaAttribute
    {
        [JsonPropertyName("i")]
        public int attribute_id { get; set; }
        [JsonPropertyName("v")]
        public float value { get; set; }
    }
    public class DogmaEffect
    {
        [JsonPropertyName("e")]
        public int effect_id { get; set; }
        [JsonPropertyName("d")]
        public bool is_default { get; set; }
    }
}
