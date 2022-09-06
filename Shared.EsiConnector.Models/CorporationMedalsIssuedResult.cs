using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/medals/issued/
    /// </summary>
    public class CorporationMedalsIssuedResult:List<CorporationMedalsIssuedResult.CorporationMedalsIssuedItem>, ISsoResult
    {
        public class CorporationMedalsIssuedItem
        {
            public int medal_id { get; set; }
            public int character_id { get; set; }
            public string reason { get; set; }
            public EStatus status { get; set; }
            public int issuer_id { get; set; }
            public DateTime issued_at { get; set; }
        }
        //[JsonConverter(typeof(StringEnumConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EStatus : byte
        {
            [EnumMember(Value = "public")]
            _public=1,
            [EnumMember(Value = "private")]
            _private=2
        }
    }
}
