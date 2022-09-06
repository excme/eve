using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/mail/lists/
    /// </summary>
    public class CharacterMailListsResult:List<CharacterMailListsResult.CharacterMailListsItem>, ISsoResult
    {
        public class CharacterMailListsItem
        {
            public int mailing_list_id { get; set; }
            public string name { get; set; }
        }
    }
}
