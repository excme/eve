using eveDirect.Shared.EsiConnector.Enumerations;
using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Search : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/search/
        /// </summary>
        [Fact]
        public void CharacterSearchResult()
        {
            string query = "Jita";
            ExecuteAndOutput(connector.Search.ForCharacter(query, SearchCategory.SolarSystem));
        }

        /// <summary>
        /// GET /search/
        /// </summary>
        [Fact]
        public void PublicSearchResult()
        {
            string query = "Jita";
            ExecuteAndOutput(connector.Search.Query(query, SearchCategory.SolarSystem));
        }
    }
}
