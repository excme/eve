using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/opportunities/
    /// </summary>
    public class CharacterOpportunitiesResult:List<CharacterOpportunitiesResult.CharacterOpportunitiesItem>
    {
        public class CharacterOpportunitiesItem
        {
            public int task_id { get; set; }
            public DateTime completed_at { get; set; }
        }
    }
}
