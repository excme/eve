using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/orders/history/
    /// </summary>
    public class CorporationOrdersHistoryResult:List<CorporationOrdersHistoryResult.CorporationOrdersHistoryItem>, ISsoResult
    {
        public class CorporationOrdersHistoryItem: CorporationOrdersResult.CorporationOrdersItem
        {
            public CharacterOrdersHistoryResult.EState state { get; set; }
        }
    }
}
