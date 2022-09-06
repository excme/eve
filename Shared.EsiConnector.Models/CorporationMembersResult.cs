using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/members/
    /// </summary>
    public class CorporationMembersResult : List<int>, ISsoResult { }
}
