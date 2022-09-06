using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /markets/structures/{structure_id}/
    /// </summary>
    public class MarketsStructureOrdersResult : List<MarketsStructureOrdersResult.MarketsStructureOrdersItem>, ISsoResult
    {
        public class MarketsStructureOrdersItem { 
        
            public long order_id { get; set; }
            public int type_id { get; set; }
            public long location_id { get; set; }
            public int volume_total { get; set; }
            public int volume_remain { get; set; }
            public int min_volume { get; set; }
            public double price { get; set; }
            public bool is_buy_order { get; set; }
            public int duration { get; set; }
            public DateTime issued { get; set; }
            public MarketsOrdersResult.ERange range { get; set; }
        }
    }
}
