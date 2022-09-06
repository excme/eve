using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/system_jumps/
    /// </summary>
    public class UniverseSystemJumpsResult : List<UniverseSystemJumpsResult.UniverseSystemJumpsItem>, ISsoResult
    {
        public class UniverseSystemJumpsItem
        {
            public int system_id { get; set; }
            public int ship_jumps { get; set; }
        }
    }
}
