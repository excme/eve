using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /markets/groups/{market_group_id}/
    /// </summary>
    public class MarketsGroupInfoResult: ISsoResult
    {
        public int market_group_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<int> types { get; set; }
        public int parent_group_id { get; set; }
    }
}
