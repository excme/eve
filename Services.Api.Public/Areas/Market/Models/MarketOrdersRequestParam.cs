using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Api.Public.Areas.Market.Models
{
    public class MarketOrdersRequestParam
    {
        public int type_id { get; set; }
        public bool is_buy { get; set; }
        public int page { get; set; }
        public int[] systems { get; set; }
    }
}
