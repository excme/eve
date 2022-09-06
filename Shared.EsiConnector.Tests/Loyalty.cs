using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Loyalty : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/loyalty/points/
        /// </summary>
        [Fact]
        public void CharacterLoyaltyPointsResult()
        {
            ExecuteAndOutput(connector.Loyalty.Points());
        }

        /// <summary>
        /// GET /loyalty/stores/{corporation_id}/offers/
        /// </summary>
        [Fact]
        public void LoyaltyStoresOffersResult()
        {
            var local_corp = 1000120;
            ExecuteAndOutput(connector.Loyalty.Offers(local_corp));
        }
    }
}
