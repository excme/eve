namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /universe/graphics/{graphic_id}/
    /// </summary>
    public class UniverseGraphicInfoResult: ISsoResult
    {
        public int graphic_id { get; set; }
        public string sof_race_name { get; set; }
        public string sof_fation_name { get; set; }
        public string sof_dna { get; set; }
        public string sof_hull_name { get; set; }
        public string collision_file { get; set; }
        public string graphic_file { get; set; }
        public string icon_folder { get; set; }
    }
}
