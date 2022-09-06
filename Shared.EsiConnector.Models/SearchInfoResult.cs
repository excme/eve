using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /search/
    /// </summary>
    public class SearchInfoResult: ISsoResult
    {
        public List<int> agent { get; set; } = new List<int>();
        public List<int> alliance { get; set; } = new List<int>();
        public List<int> character { get; set; } = new List<int>();
        public List<int> constellation { get; set; } = new List<int>();
        public List<int> corporation { get; set; } = new List<int>();
        public List<int> faction { get; set; } = new List<int>();
        public List<int> inventory_type { get; set; } = new List<int>();
        public List<int> region { get; set; } = new List<int>();
        public List<int> solar_system { get; set; } = new List<int>();
        public List<int> station { get; set; } = new List<int>();
    }
    /// <summary>
    /// GET /characters/{character_id}/search/
    /// </summary>
    public class CharacterSearchResult: SearchInfoResult
    {
        public List<long> structure { get; set; } = new List<long>();
    }
}
