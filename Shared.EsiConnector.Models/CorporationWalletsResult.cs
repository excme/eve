using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/wallets/
    /// </summary>
    public class CorporationWalletsResult : List<CorporationWalletsResult.CorporationWalletsItem>, ISsoResult {
        public class CorporationWalletsItem
        {
            public int division { get; set; }
            public double balance { get; set; }
        }
    }
}
