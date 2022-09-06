using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseAsteroidBelt : EveOnlineUniverseSystemLocation
    {
        [NotMapped]
        public int asteroid_belt_id
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

        public int? asteroidBelt_planetId { get; set; }
        public virtual EveOnlineUniversePlanet asteroidBelt_planet { get; set; }
    }
}