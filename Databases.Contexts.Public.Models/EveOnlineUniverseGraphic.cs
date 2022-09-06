namespace eveDirect.Databases.Contexts.Public.Models
{
    using eveDirect.Shared.EsiConnector.Models;
    using System.ComponentModel.DataAnnotations;
    public class EveOnlineUniverseGraphic : UniverseGraphicInfoResult
    {
        [Key]
        public new int graphic_id { get; set; }
    }
}
