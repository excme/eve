using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseMoon : EveOnlineUniverseSystemLocation
    {
        [NotMapped]
        public int moon_id
        {
            get
            {
                return location_id;
            }
            set
            {
                location_id = value;
            }
        }

        public int? moon_planetId { get; set; }
        public virtual EveOnlineUniversePlanet moon_planet { get; set; }

        //public int planet_id { get; set; }

        //public virtual EveOnlineUniversePlanet planet { get; set; }
        //public Guid? planetId { get; set; }
    }
}