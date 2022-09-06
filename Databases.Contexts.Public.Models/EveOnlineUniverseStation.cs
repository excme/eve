using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseStation: EveOnlineUniverseSystemLocation
    {
        [NotMapped]
        public int station_id
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

        public float reprocessing_efficiency { get; set; }
        public float reprocessing_stations_take { get; set; }
        public float max_dockable_ship_volume { get; set; }
        public float office_rental_cost { get; set; }

        public List<string> services { get; set; }

        public int owner { get; set; }
        public int race_id { get; set; }
    }
}