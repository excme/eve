using eveDirect.Shared.EsiConnector.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineCharacterAffiliation : CharacterAffiliationResult.CharacterAffiliationItemValue
    {
        [Key]
        public int character_id { get; set; }
    }
}
