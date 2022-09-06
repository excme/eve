using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/attributes/
    /// </summary>
    public class CharacterAttributesResult: ISsoResult
    {
        public int charisma { get; set; }
        public int intelligence { get; set; }
        public int memory { get; set; }
        public int perception { get; set; }
        public int willpower { get; set; }
        public int bonus_remaps { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime last_remap_date { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime accrued_remap_cooldown_date { get; set; }
    }
}
