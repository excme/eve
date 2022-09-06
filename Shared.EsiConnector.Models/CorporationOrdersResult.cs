using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/orders/
    /// </summary>
    public class CorporationOrdersResult : List<CorporationOrdersResult.CorporationOrdersItem>, ISsoResult
    {
        public class CorporationOrdersItem : MarketsOrdersResult.BaseMarketOrderItem
        {
            public int issued_by { get; set; }
            public int wallet_division { get; set; }
            public double escrow { get; set; }
            public int region_id { get; set; }
        }
    }
}
