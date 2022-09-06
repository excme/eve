using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Результат POST /characters/affiliation/
    /// </summary>
    public class CharacterAffiliationResult:List<CharacterAffiliationResult.CharacterAffiliationItem>, ISsoResult
    {
        public class CharacterAffiliationItem : CharacterAffiliationItemValue
        {
            public int character_id { get; set; }
        }
        public class CharacterAffiliationItemValue
        {
            public int corporation_id { get; set; }
            public int alliance_id { get; set; }
            public int faction_id { get; set; }
        }
    }
}
