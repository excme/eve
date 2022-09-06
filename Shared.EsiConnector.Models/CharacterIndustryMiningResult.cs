using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/mining/
    /// </summary>
    public class CharacterIndustryMiningResult:List<CharacterIndustryMiningResult.CharacterIndustryMiningItem>, ISsoResult
    {
        public class CharacterIndustryMiningItem
        {
            [Column(TypeName = "smalldatetime")]
            public DateTime date { get; set; }
            public int solar_system_id { get; set; }
            public int type_id { get; set; }
            public long quantity { get; set; }
        }
    }
}
