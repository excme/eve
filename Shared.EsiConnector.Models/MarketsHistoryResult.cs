using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /markets/{region_id}/history/
    /// </summary>
    public class MarketsHistoryResult : List<MarketsHistoryResult.MarketsHistoryItem>, ISsoResult
    {
        public class MarketsHistoryItem
        {
            public DateTime date { get; set; }
            public long order_count { get; set; }
            public long volume { get; set; }
            public double highest { get; set; }
            public double average { get; set; }
            public double lowest { get; set; }
        }
    }
}
