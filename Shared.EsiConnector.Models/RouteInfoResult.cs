using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /route/{origin}/{destination}/
    /// </summary>
    public class RouteInfoResult:List<int>, ISsoResult { }
}
