using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /markets/{region_id}/orders/
    /// </summary>
    public class MarketsOrdersResult : List<MarketsOrdersResult.MarketsOrdersItem>, ISsoResult
    {
        public class MarketsOrdersItem: BaseMarketOrderItem
        {
            public int system_id { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ERange : byte
        {
            station = 1,
            region = 2,
            solarsystem = 3,
            [EnumMember(Value = "1")]
            _1 = 4,
            [EnumMember(Value = "2")]
            _2 = 5,
            [EnumMember(Value = "3")]
            _3 = 6,
            [EnumMember(Value = "4")]
            _4 = 7,
            [EnumMember(Value = "5")]
            _5 = 8,
            [EnumMember(Value = "10")]
            _10 = 9,
            [EnumMember(Value = "20")]
            _20 = 10,
            [EnumMember(Value = "30")]
            _30 = 11,
            [EnumMember(Value = "40")]
            _40 = 12
        }
        public class BaseMarketOrderItem {
            public int duration { get; set; }
            public DateTime issued { get; set; }
            public long order_id { get; set; }
            public int type_id { get; set; }
            public long location_id { get; set; }
            public ERange range { get; set; }
            public bool is_buy_order { get; set; }
            public int min_volume { get; set; }
            public double price { get; set; }
            public int volume_total { get; set; }
            public int volume_remain { get; set; }
        }
    }
}
