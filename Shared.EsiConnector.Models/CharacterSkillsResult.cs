using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/skills/
    /// </summary>
    public class CharacterSkillsResult:ISsoResult
    {
        [NotMapped]
        public List<Skill> skills { get; set; }
        public long total_sp { get; set; }
        public int unallocated_sp { get; set; }
        public class Skill
        {
            public int skill_id { get; set; }
            public long skillpoints_in_skill { get; set; }
            public int trained_skill_level { get; set; }
            public int active_skill_level { get; set; }
        }
    }
}
