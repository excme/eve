using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class CharacterPublicModel
    {

        [JsonPropertyName("i")]
        public int id { get; set; }
        [JsonPropertyName("n")]
        public string name { get; set; }
    }
}
