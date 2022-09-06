using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/membertracking/
    /// </summary>
    public class CorporationMembertrackingResult:List<CorporationMembertrackingResult.CorporationMembertrackingItem>, ISsoResult
    {
        public class CorporationMembertrackingItem
        {
            public int character_id { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime? start_date { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime? logon_date { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime? logoff_date { get; set; }
            public long location_id { get; set; }
            public int ship_type_id { get; set; }
            public int base_id { get; set; }
        }
    }
}
