using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Post /universe/ids/
    /// </summary>
    public class UniverseIdResult: ISsoResult
    {
        public class UniverseNameItem
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public List<UniverseNameItem> agents { get; set; } = new List<UniverseNameItem>();
        public List<UniverseNameItem> alliances { get; set; } = new List<UniverseNameItem>();
        public List<UniverseNameItem> characters { get; set; } = new List<UniverseNameItem>();
        public List<UniverseNameItem> constellations { get; set; } = new List<UniverseNameItem>();
        public List<UniverseNameItem> corporations { get; set; } = new List<UniverseNameItem>();
        public List<UniverseNameItem> factions { get; set; } = new List<UniverseNameItem>();
        public List<UniverseNameItem> inventory_types { get; set; } = new List<UniverseNameItem>();
        public List<UniverseNameItem> regions { get; set; } = new List<UniverseNameItem>();
        public List<UniverseNameItem> systems { get; set; } = new List<UniverseNameItem>();
        public List<UniverseNameItem> stations { get; set; } = new List<UniverseNameItem>();
    }
}
