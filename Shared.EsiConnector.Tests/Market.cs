using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Market : BaseConnector
    {
        /// <summary>
        /// GET /markets/prices/
        /// </summary>
        [Fact]
        public void MarketsPricesResult()
        {
            ExecuteAndOutput(connector.Market.Prices());
        }
        /// <summary>
        /// GET /markets/{region_id}/orders/
        /// </summary>
        [Fact]
        public void MarketsOrdersResult()
        {
            var regionId = 10000002;
            ExecuteAndOutput(connector.Market.RegionOrders(regionId));
        }
        /// <summary>
        /// GET /markets/{region_id}/history/
        /// </summary>
        [Fact]
        public void MarketsHistoryResult()
        {
            var regionId = 10000002;
            var typeId = 34;
            ExecuteAndOutput(connector.Market.TypeHistoryInRegion(regionId, typeId));
        }
        /// <summary>
        /// GET /markets/structures/{structure_id}/
        /// </summary>
        [Fact]
        public void MarketsStructureOrdersResult()
        {
            var structureId = 1023454626196;
            ExecuteAndOutput(connector.Market.StructureOrders(structureId));
        }
        /// <summary>
        /// GET /markets/groups/
        /// </summary>
        [Fact]
        public void MarketsGroupsResult()
        {
            ExecuteAndOutput(connector.Market.Groups());
        }
        /// <summary>
        /// GET /markets/groups/{market_group_id}/
        /// </summary>
        [Fact]
        public void MarketsGroupInfoResult()
        {
            var marketId = 1334;
            ExecuteAndOutput(connector.Market.Group(marketId));
        }
        /// <summary>
        /// GET /characters/{character_id}/orders/
        /// </summary>
        [Fact]
        public void CharacterOrdersResult()
        {
            ExecuteAndOutput(connector.Market.CharacterOrders());
        }
        /// <summary>
        /// GET /characters/{character_id}/orders/history/
        /// </summary>
        [Fact]
        public void CharacterOrdersHistoryResult()
        {
            ExecuteAndOutput(connector.Market.CharacterOrderHistory());
        }
        /// <summary>
        /// GET /markets/{region_id}/types/
        /// </summary>
        [Fact]
        public void MarketsTypesResult()
        {
            var regionId = 10000002;
            ExecuteAndOutput(connector.Market.Types(regionId));
        }
        /// <summary>
        /// GET /corporations/{corporation_id}/orders/
        /// </summary>
        [Fact]
        public void CorporationOrdersResult()
        {
            ExecuteAndOutput(connector.Market.CorporationOrders());
        }
        /// <summary>
        /// GET /corporations/{corporation_id}/orders/history/
        /// </summary>
        [Fact]
        public void CorporationOrdersHistoryResult()
        {
            ExecuteAndOutput(connector.Market.CorporationOrderHistory());
        }

    }
}
