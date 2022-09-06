using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/groups/{group_id}/
    /// </summary>
    public class UniverseGroupInfoResult: ISsoResult
    {
        public int group_id { get; set; }
        public string name { get; set; }
        public bool published { get; set; }
        public int category_id { get; set; }
        public List<int> types { get; set; }
    }
}
