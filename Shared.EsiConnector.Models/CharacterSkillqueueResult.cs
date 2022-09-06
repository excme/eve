using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/skillqueue/
    /// </summary>
    public class CharacterSkillqueueResult:List<CharacterSkillqueueResult.CharacterSkillsItem>, ISsoResult
    {
        public class CharacterSkillsItem
        {
            public int skill_id { get; set; }
            public int finished_level { get; set; }
            public int queue_position { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime? finish_date { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime? start_date { get; set; }
            public int training_start_sp { get; set; }
            public int level_end_sp { get; set; }
            public int level_start_sp { get; set; }
        }
    }
}
