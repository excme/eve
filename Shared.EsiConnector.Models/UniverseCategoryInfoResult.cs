using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/categories/{category_id}/
    /// </summary>
    public class UniverseCategoryInfoResult: ISsoResult
    {
        public int category_id { get; set; }
        public string name { get; set; }
        public bool published { get; set; }
        public List<int> groups { get; set; }
    }
}
