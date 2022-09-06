using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/bookmarks/folders/
    /// </summary>
    public class CharacterBookmarksFoldersResult:List<CharacterBookmarksFoldersResult.BookmarksFolderItem>, ISsoResult
    {
        public class BookmarksFolderItem
        {
            public int folder_id { get; set; }
            public string name { get; set; }
        }
    }
    /// <summary>
    /// GET /corporations/{corporation_id}/bookmarks/folders/
    /// </summary>
    public class CorporationBookmarksFoldersResult : List<CorporationBookmarksFoldersResult.CorpBookmarksFolderItem>, ISsoResult
    {
        public class CorpBookmarksFolderItem : CharacterBookmarksFoldersResult.BookmarksFolderItem
        {
            public int creator_id { get; set; }
        }
    }
}
