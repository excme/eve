using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/structures/
    /// </summary>
    public class UniverseStructuresResult:List<long>, ISsoResult
    {
    }
}
