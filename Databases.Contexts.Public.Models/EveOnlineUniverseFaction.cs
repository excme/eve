using eveDirect.Shared.EsiConnector.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseFaction:UniverseFactionsResult.UniverseFactionsItem
    {
        [Key]
        public new int faction_id { get; set; }

        // Names
        public string dename { get; set; }
        public string enname { get; set; }
        public string frname { get; set; }
        public string janame { get; set; }
        public string runame { get; set; }
        public string zhname { get; set; }
        public string koname { get; set; }

        // Desc
        public string dedescription { get; set; }
        public string endescription { get; set; }
        public string frdescription { get; set; }
        public string jadescription { get; set; }
        public string rudescription { get; set; }
        public string zhdescription { get; set; }
        public string kodescription { get; set; }

    }
}