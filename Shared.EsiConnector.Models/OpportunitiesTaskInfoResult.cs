using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /opportunities/tasks/{task_id}/
    /// </summary>
    public class OpportunitiesTaskInfoResult: ISsoResult
    {
        public int task_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string notification { get; set; }
    }
}
