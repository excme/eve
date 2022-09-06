using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/ancestries/
    /// </summary>
    public class UniverseAncestriesResult : List<UniverseAncestriesResult.UniverseAncestriesItem>, ISsoResult
    {
        public class UniverseAncestriesItem
        {
            public int id { get; set; }
            public string name { get; set; }
            public int bloodline_id { get; set; }
            public string description { get; set; }
            public string short_description { get; set; }
            public int icon_id { get; set; }
        }
    }
}
