using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/shareholders/
    /// </summary>
    public class CorporationShareholdersResult:List<CorporationShareholdersResult.CorporationShareholdersItem>, ISsoResult
    {
        public class CorporationShareholdersItem
        {
            public int shareholder_id { get; set; }
            public EShareholderType shareholder_type { get; set; }
            public long share_count { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EShareholderType : byte
        {
            character=1,
            corporation =2
        }
    }
}
