using System;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    /// <summary>
    /// Элемент из списка новорожденного персонажа
    /// </summary>
    public class CharacterNewbornCorporationItem
    {
        /// <summary>
        /// corporation_id
        /// </summary>
        //[JsonPropertyName("i")]
        public int id { get; set; }

        /// <summary>
        /// corporation_name
        /// </summary>
        //public string n { get; set; }

        /// <summary>
        /// count
        /// </summary>
        //[JsonPropertyName("c")]
        public int count { get; set; }
    }
}
