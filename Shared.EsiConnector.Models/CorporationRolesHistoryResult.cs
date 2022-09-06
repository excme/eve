using System;
using System.Collections.Generic;
using System.Text;
using static eveDirect.Shared.EsiConnector.Models.CorporationRolesHistoryResult;
using static eveDirect.Shared.EsiConnector.Models.CorporationRolesResult;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/roles/history/
    /// </summary>
    public class CorporationRolesHistoryResult:List<CorporationRolesHistoryItem>
    {
        public class CorporationRolesHistoryItem
        {
            public int character_id { get; set; }
            public DateTime changed_at { get; set; }
            public int issuer_id { get; set; }
            public ERoleType role_type { get; set; }
            public List<ERole> old_roles { get; set; }
            public List<ERole> new_roles { get; set; }
        }
    }
}
