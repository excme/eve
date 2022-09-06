using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Результат GET /characters/{character_id}/fatigue/
    /// </summary>
    public class CharacterFatigueResult : ISsoResult
    {
        [Column(TypeName = "smalldatetime")]
        public DateTime? last_jump_date { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? jump_fatigue_expire_date { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? last_update_date { get; set; }
    }
}
