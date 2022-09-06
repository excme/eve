using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /alliances/{alliance_id}/corporations/
    /// </summary>
    public class AlliancesCorporationsResult : List<int>, ISsoResult
    {
        
    }
}
