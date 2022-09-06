using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// /dogma/dynamic/items/{type_id}/{item_id}/
    /// </summary>
    public class DogmaDynamicItemResult
    {
        public int created_by { get; set; }
        public List<Attribute> dogma_attributes { get; set; } = new List<Attribute>();
        public List<Effect> dogma_effects { get; set; } = new List<Effect>();
        public int mutator_type_id { get; set; }
        public int source_type_id { get; set; }
        public class Attribute
        {
            public int attribute_id { get; set; }
            public float value { get; set; }
        }
        public class Effect
        {
            public int effect_id { get; set; }
            public bool is_default { get; set; }
        }
    }
}