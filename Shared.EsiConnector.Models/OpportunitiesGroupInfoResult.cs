using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /opportunities/groups/{group_id}/
    /// </summary>
    public class OpportunitiesGroupInfoResult: ISsoResult
    {
        public int group_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string notification { get; set; }
        [NotMapped]
        public List<int> required_tasks { get; set; }
        [NotMapped]
        public List<int> connected_groups { get; set; }
    }
}
