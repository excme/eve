using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/bloodlines/
    /// </summary>
    public class UniverseBloodlinesResult : List<UniverseBloodlinesResult.UniverseBloodlinesItem>, ISsoResult
    {
        public class UniverseBloodlinesItem
        {
            public int bloodline_id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public int race_id { get; set; }
            public int ship_type_id { get; set; }
            public int corporation_id { get; set; }
            public int perception { get; set; }
            public int willpower { get; set; }
            public int charisma { get; set; }
            public int memory { get; set; }
            public int intelligence { get; set; }
        }
    }
}
