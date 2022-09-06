using eveDirect.Shared.EsiConnector.Models;
using System.ComponentModel.DataAnnotations;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineMarketOrder : MarketsOrdersResult.MarketsOrdersItem
    {
        public bool disabled { get; set; }

        public EveOnlineMarketOrder(MarketsOrdersResult.MarketsOrdersItem data)
        {
            duration = data.duration;
            issued = data.issued;
            order_id = data.order_id;
            type_id = data.type_id;
            location_id = data.location_id;
            range = data.range;
            is_buy_order = data.is_buy_order;
            min_volume = data.min_volume;
            price = data.price;
            volume_remain = data.volume_remain;
            volume_total = data.volume_total;
            system_id = data.system_id; 
        }
        public EveOnlineMarketOrder() { }

        [Key]
        public new long order_id { get; set; }

        public int region_id { get; set; }
    }
}
