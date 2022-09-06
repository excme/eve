using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/orders/history/
    /// </summary>
    public class CharacterOrdersHistoryResult:List<CharacterOrdersHistoryResult.CharacterOrdersHistoryItem>, ISsoResult
    {
        public class CharacterOrdersHistoryItem: CharacterOrdersResult.CharacterOrdersItem
        {
            public EState state { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EState : byte
        {
            cancelled = 1,
            expired = 2
        }
    }
}
