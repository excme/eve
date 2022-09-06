using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/loyalty/points/
    /// </summary>
    public class CharacterLoyaltyPointsResult:List<CharacterLoyaltyPointsResult.CharacterLoyaltyPointsItem>, ISsoResult
    {
        public class CharacterLoyaltyPointsItem
        {
            public int corporation_id { get; set; }
            public int loyalty_points { get; set; }
        }
    }
}
