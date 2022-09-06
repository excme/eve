using eveDirect.Shared.EsiConnector.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseRegion : UniverseRegionInfoResult
    {
        [Key]
        public new int region_id { get; set; }

        // Names
        //public string deName { get; set; }
        //public string enName { get; set; }
        //public string frName { get; set; }
        //public string jaName { get; set; }
        //public string ruName { get; set; }
        //public string zhName { get; set; }
        //public string koName { get; set; }

    }
}