using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/categories/
    /// </summary>
    public class UniverseCategoriesResult:List<int>, ISsoResult
    {
    }
}
