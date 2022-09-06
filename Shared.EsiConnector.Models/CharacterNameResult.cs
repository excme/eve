using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    public class CharacterNameResult : List<CharacterNameResult.CharacterNameItem> {
        public class CharacterNameItem
        {
            public int character_id { get; set; }
            public string character_name { get; set; }
        }
    }
}
