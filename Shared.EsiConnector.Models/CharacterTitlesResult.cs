using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Результат GET /characters/{character_id}/titles/
    /// </summary>
    public class CharacterTitlesResult:List<CharacterTitlesResult.CharacterTitlesItem>
    {
        public class CharacterTitlesItem
        {
            public int title_id { get; set; }
            public string name { get; set; }
        }
    }
}
