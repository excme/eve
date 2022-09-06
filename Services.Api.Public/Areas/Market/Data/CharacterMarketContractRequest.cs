using DevExtreme.AspNet.Data;

namespace eveDirect.Api.Public.Areas.Market.Data
{
    public class CharacterMarketContractRequest
    {
        public int id { get; set; }
        public DataSourceLoadOptionsBase lo { get; set; }
    }
}
