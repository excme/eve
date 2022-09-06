using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/races/
    /// </summary>
    public class UniverseRacesResult : List<UniverseRacesResult.UniverseRacesItem>, ISsoResult
    {
        public class UniverseRacesItem { 
            public int race_id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public int alliance_id { get; set; }
        }
    }
}
