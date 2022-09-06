using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/industry/jobs/
    /// </summary>
    public class CorporationIndustryJobsResult:List<CorporationIndustryJobsResult.CorporationIndustryJobsItem>, ISsoResult
    {
        public class CorporationIndustryJobsItem: BaseIndustryJobsItem
        {
            public long location_id { get; set; }
        }
        public class BaseIndustryJobsItem
        {
            public int job_id { get; set; }
            public int installer_id { get; set; }
            public long facility_id { get; set; }
            public int activity_id { get; set; }
            public long blueprint_id { get; set; }
            public int blueprint_type_id { get; set; }
            public long blueprint_location_id { get; set; }
            public long output_location_id { get; set; }
            public int runs { get; set; }
            public EIndustryJobStatus status { get; set; }
            public int duration { get; set; }
            public DateTime? start_date { get; set; }
            public DateTime? end_date { get; set; }
            public DateTime? pause_date { get; set; }
            public DateTime? completed_date { get; set; }
            public int successful_runs { get; set; }
            public int completed_character_id { get; set; }
            public double cost { get; set; }
            public int licensed_runs { get; set; }
            public float probability { get; set; }
            public int product_type_id { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EIndustryJobStatus : byte
        {
            active = 1,
            cancelled = 2,
            delivered = 3,
            paused = 4,
            ready = 5,
            reverted = 6
        }
    }
}
