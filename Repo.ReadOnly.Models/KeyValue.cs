using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    /// <summary>
    /// Замена Dictionary item для сериалиации json
    /// </summary>
    public class KeyValue<TKey, TValue>
    {
        public KeyValue()
        {

        }
        public KeyValue(TKey _key, TValue _value)
        {
            key = _key;
            value = _value;
        }
        [JsonPropertyName("k")]
        public TKey key { get; set; }

        [JsonPropertyName("v")]
        public TValue value { get; set; }
    }
}
