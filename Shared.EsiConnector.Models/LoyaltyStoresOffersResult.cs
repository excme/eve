using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /loyalty/stores/{corporation_id}/offers/
    /// </summary>
    public class LoyaltyStoresOffersResult:List<LoyaltyStoresOffersResult.LoyaltyStoresOffersItem>, ISsoResult
    {
        public class LoyaltyStoresOffersItem
        {
            public int offer_id { get; set; }
            public int type_id { get; set; }
            public int quantity { get; set; }
            public int lp_cost { get; set; }
            public long isk_cost { get; set; }
            public List<RequiredItem> required_items { get; set; }
            public int? ak_cost { get; set; }

            public class RequiredItem
            {
                public int type_id { get; set; }
                public int quantity { get; set; }
            }
        }
    }
}
