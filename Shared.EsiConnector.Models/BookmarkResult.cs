using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/bookmarks/
    /// GET /corporations/{corpration_id}/bookmarks/
    /// </summary>
    public class BookmarksResult:List<BookmarksResult.BookmarkItem>, ISsoResult
    {
        public class BookmarkItemItem
        {
            public long item_id { get; set; }
            public int type_id { get; set; }
        }
        public class BookmarkItem
        {
            public int bookmark_id { get; set; }
            public int creator_id { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime created { get; set; }
            public string label { get; set; }
            public string notes { get; set; }
            public int location_id { get; set; }
            public int folder_id { get; set; }
            [NotMapped]
            public Position coordinates { get; set; }
            [NotMapped]
            public BookmarkItemItem item { get; set; }
        }
    }
}
