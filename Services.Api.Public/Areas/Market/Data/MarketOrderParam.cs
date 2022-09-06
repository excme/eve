using DevExtreme.AspNet.Data;

namespace eveDirect.Api.Public.Areas.Market.Data
{
    public class MarketOrderParam
    {
        public int t { get; set; }
        public bool b { get; set; }
        public int[] s { get; set; }
        public DataSourceLoadOptionsBase lo { get; set; }
    }
}
