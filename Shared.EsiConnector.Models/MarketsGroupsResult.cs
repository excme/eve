using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /markets/groups/
    /// </summary>
    public class MarketsGroupsResult:List<int>, ISsoResult { }
}
