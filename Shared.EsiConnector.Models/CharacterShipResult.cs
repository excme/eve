namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/ship/
    /// </summary>
    public class CharacterShipResult
    {
        public int ship_type_id { get; set; }
        public string ship_name { get; set; }
        public long ship_item_id { get; set; }
    }
}
