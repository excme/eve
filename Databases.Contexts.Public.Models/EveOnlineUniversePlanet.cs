using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniversePlanet: EveOnlineUniverseSystemLocation
    {
        [NotMapped]
        public int planet_id
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

        public virtual List<EveOnlineUniverseMoon> moons { get; set; } = new List<EveOnlineUniverseMoon>();
        public virtual List<EveOnlineUniverseAsteroidBelt> asteroid_belts { get; set; } = new List<EveOnlineUniverseAsteroidBelt>();
    }
}