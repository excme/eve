using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /markets/{region_id}/types/
    /// </summary>
    public class MarketsTypesResult : List<int>, ISsoResult
    {
    }
}
