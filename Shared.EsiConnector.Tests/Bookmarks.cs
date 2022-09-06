using Xunit;
namespace eveDirect.EsiConnector.Tests
{
    public class Bookmarks : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/bookmarks/
        /// </summary>
        [Fact]
        public void CharacterBookmarkResult()
        {
            ExecuteAndOutput(connector.Bookmarks.ForCharacter());
        }

        /// <summary>
        /// GET /characters/{character_id}/bookmarks/folders/
        /// </summary>
        [Fact]
        public void CharacterBookmarkFoldersResult()
        {
            ExecuteAndOutput(connector.Bookmarks.FoldersForCharacter());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/bookmarks/
        /// </summary>
        [Fact]
        public void CorporationBookmarksResult()
        {
            ExecuteAndOutput(connector.Bookmarks.ForCorporation());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/bookmarks/folders/
        /// </summary>
        [Fact]
        public void CorporationBookmarksFoldersResult()
        {
            ExecuteAndOutput(connector.Bookmarks.FoldersForCorporation());
        }
    }
}
