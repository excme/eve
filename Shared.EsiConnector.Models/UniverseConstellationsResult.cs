using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/constellations/
    /// </summary>
    public class UniverseConstellationsResult:List<int>, ISsoResult
    {
    }
}
