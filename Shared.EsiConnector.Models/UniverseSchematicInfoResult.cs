namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/schematics/{schematic_id}/
    /// </summary>
    public class UniverseSchematicInfoResult : ISsoResult
    {
        public string schematic_name { get; set; }
        public int cycle_time { get; set; }
    }
}
