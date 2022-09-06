using System.ComponentModel.DataAnnotations.Schema;
using eveDirect.Shared.EsiConnector.Models;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseStar: EveOnlineUniverseSystemLocation
    {
        [NotMapped]
        public int star_id { get { return location_id; } set { location_id = value; } }

        public long age { get; set; }
        public float luminosity { get; set; }
        public long radius { get; set; }
        public UniverseStarInfoResult.ESpectralClass spectral_class { get; set; }
        public int temperature { get; set; }
    }
}