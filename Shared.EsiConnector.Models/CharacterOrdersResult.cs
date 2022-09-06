using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/orders/
    /// </summary>
    public class CharacterOrdersResult:List<CharacterOrdersResult.CharacterOrdersItem>,ISsoResult
    {
        public class CharacterOrdersItem : MarketsOrdersResult.BaseMarketOrderItem
        {
            public double escrow { get; set; }
            public bool is_corporation { get; set; }
            public int region_id { get; set; }
        }
    }
}
