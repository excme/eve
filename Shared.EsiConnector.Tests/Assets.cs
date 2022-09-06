using Xunit;
namespace eveDirect.EsiConnector.Tests
{
    public class Assets : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/assets/
        /// </summary>
        [Fact]
        public void CharacterAssetResult()
        {
            ExecuteAndOutput(connector.Assets.ForCharacter());
        }

        /// <summary>
        /// POST /characters/{character_id}/assets/names/
        /// </summary>
        [Fact]
        public void CorporationAssetResult()
        {
            ExecuteAndOutput(connector.Assets.ForCorporation());
        }

        /// <summary>
        /// POST /corporations/{corporation_id}/assets/names/
        /// </summary>
        [Fact]
        public void CharacterAssetsNamesResult()
        {
            var assetId = 1011399534068;
            ExecuteAndOutput(connector.Assets.NamesForCharacter(assetId));
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/assets/
        /// </summary>
        [Fact]
        public void CorporationAssetsNamesResult()
        {
            var assetId = 1020740162686;
            ExecuteAndOutput(connector.Assets.NamesForCorporation(assetId));
        }

        /// <summary>
        /// POST /characters/{corporation_id}/assets/locations/
        /// </summary>
        [Fact]
        public void CharacterAssetsLocationsResult()
        {
            var assetId = 1011399534068;
            ExecuteAndOutput(connector.Assets.LocationsForCharacter(assetId));
        }

        /// <summary>
        /// POST /corporations/{corporation_id}/assets/locations/
        /// </summary>
        [Fact]
        public void CorporationAssetsLocationsResult()
        {
            var assetId = 1020740162686;
            ExecuteAndOutput(connector.Assets.LocationsForCorporation(assetId));
        }
    }
}