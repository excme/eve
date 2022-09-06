using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /dogma/effects/{effect_id}/
    /// </summary>
    public class DogmaEffectInfoResult
    {
        public int effect_id { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public string description { get; set; }
        public int effect_category { get; set; }
        public int pre_expression { get; set; }
        public int post_expression { get; set; }
        public List<Modifier> modifiers { get; set; }
        public int tracking_speed_attribute_id { get; set; }
        public int range_attribute_id { get; set; }
        public int falloff_attribute_id { get; set; }
        public int duration_attribute_id { get; set; }
        public int discharge_attribute_id { get; set; }
        public int icon_id { get; set; }
        public bool range_chance { get; set; }
        public bool published { get; set; }
        public bool is_warp_safe { get; set; }
        public bool is_offensive { get; set; }
        public bool is_assistance { get; set; }
        public bool electronic_chance { get; set; }
        public bool disallow_auto_repeat { get; set; }
        public class Modifier
        {
            public string func { get; set; }
            public string domain { get; set; }
            public int modified_attribute_id { get; set; }
            public int modifying_attribute_id { get; set; }
            [JsonPropertyName("operator")]
            public int _operator { get; set; }
            public int effect_id { get; set; }
        }
    }
}
