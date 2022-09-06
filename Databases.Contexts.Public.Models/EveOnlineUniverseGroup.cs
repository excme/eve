using eveDirect.Shared.EsiConnector.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseGroup : UniverseGroupInfoResult
    {
        [Key]
        public new int group_id { get; set; }

        //Names
        public string dename { get; set; }
        public string enname { get; set; }
        public string frname { get; set; }
        public string janame { get; set; }
        public string runame { get; set; }
        public string zhname { get; set; }
        public string koname { get; set; }
    }
}