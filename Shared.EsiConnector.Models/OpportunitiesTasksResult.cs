using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /opportunities/tasks/
    /// </summary>
    public class OpportunitiesTasksResult:List<int>, ISsoResult
    {
    }
}
