using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /markets/prices/
    /// </summary>
    public class MarketsPricesResult:List<MarketsPricesResult.MarketsPricesItem>, ISsoResult
    {
        public class MarketsPricesItem
        {
            public int type_id { get; set; }
            public double average_price { get; set; }
            public double adjusted_price { get; set; }
        }
    }
}
