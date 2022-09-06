using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Результат GET /characters/{character_id}/agents_research/
    /// </summary>
    public class CharacterAgentsResearchResult:List<CharacterAgentsResearchResult.CharacterAgentsResearchItem>, ISsoResult
    {
        public class CharacterAgentsResearchItem
        {
            public int agent_id { get; set; }
            public int skill_type_id { get; set; }
            public DateTime started_at { get; set; }
            public float points_per_day { get; set; }
            public float remainder_points { get; set; }
        }
    }
}
