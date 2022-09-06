namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/portrait/
    /// </summary>
    public class ImagesResult
    {
        public string px512x512 { get; set; }
        public string px256x256 { get; set; }
        public string px128x128 { get; set; }
        public string px64x64 { get; set; }
    }
}
