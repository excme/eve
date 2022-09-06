using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/industry/jobs/
    /// </summary>
    public class CharacterIndustryJobsResult:List<CharacterIndustryJobsResult.CharacterIndustryJobsItem>, ISsoResult
    {
        public class CharacterIndustryJobsItem: CorporationIndustryJobsResult.BaseIndustryJobsItem
        {
            public long station_id { get; set; }
        }
    }
}
