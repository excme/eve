namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/structures/{structure_id}/
    /// </summary>
    public class UniverseStructureInfoResult: ISsoResult
    {
        public string name { get; set; }
        public int solar_system_id { get; set; }
        public int type_id { get; set; }
        public Position position { get; set; }
        public int owner_id { get; set; }
    }
}
