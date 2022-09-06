using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Результат GET /characters/{character_id}/medals/
    /// </summary>
    public class CharacterMedalsResult:List<CharacterMedalsResult.CharacterMedalsItem>, ISsoResult
    {
        public class Graphic
        {
            public int part { get; set; }
            public int layer { get; set; }
            public string graphic { get; set; }
            public int color { get; set; }
        }
        public class CharacterMedalsItem
        {
            public int medal_id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public int corporation_id { get; set; }
            public int issuer_id { get; set; }
            public DateTime date { get; set; }
            public string reason { get; set; }
            public EStatus status { get; set; }
            public List<Graphic> graphics { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EStatus : byte
        {
            [EnumMember(Value = "private")]
            _private = 1,
            [EnumMember(Value = "public")]
            _public = 2
        }
    }
}
