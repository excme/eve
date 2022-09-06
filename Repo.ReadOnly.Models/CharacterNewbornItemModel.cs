using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    /// <summary>
    /// Элемент из списка новорожденного персонажа
    /// </summary>
    public class CharacterNewbornItemModel
    {
        /// <summary>
        /// character_id
        /// </summary>
        //[JsonPropertyName("i")]
        public int id { get; set; }

        /// <summary>
        /// character_name
        /// </summary>
        //[JsonPropertyName("n")]
        public string n { get; set; }

        /// <summary>
        /// birthday
        /// </summary>
        //[JsonPropertyName("b")]
        public DateTime b { get; set; }

        /// <summary>
        /// corporation_id
        /// </summary>
        //[JsonPropertyName("c")] 
        public int c { get; set; }
    }
}
