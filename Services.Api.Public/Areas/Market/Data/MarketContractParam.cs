using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using System.Text.Json;

namespace eveDirect.Api.Public.Areas.Market.Data
{
    public class MarketContractParam
    {
        public int t { get; set; }
        public int[] r { get; set; }
        public DataSourceLoadOptionsBase lo { get; set; }
    }
}
