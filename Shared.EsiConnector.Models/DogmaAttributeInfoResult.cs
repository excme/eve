namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /dogma/attributes/{attribute_id}/
    /// </summary>
    public class DogmaAttributeInfoResult
    {
        public int attribute_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int icon_id { get; set; }
        public float default_value { get; set; }
        public bool published { get; set; }
        public string display_name { get; set; }
        public int unit_id { get; set; }
        public bool stackable { get; set; }
        public bool high_is_good { get; set; }
    }
}
