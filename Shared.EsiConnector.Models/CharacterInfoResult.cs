using System;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/
    /// </summary>
    public class CharacterInfoResult : ISsoResult
    {
        public string name { get; set; }
        public string description { get; set; }
        public int corporation_id { get; set; }
        public int alliance_id { get; set; }
        public DateTime birthday { get; set; }
        public EGender gender { get; set; }
        public int race_id { get; set; }
        public int bloodline_id { get; set; }
        public int ancestry_id { get; set; }
        public float security_status { get; set; }
        public int faction_id { get; set; }
        public string title { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum EGender:byte
        {
            female = 1, 
            male = 2
        }
    }
}
