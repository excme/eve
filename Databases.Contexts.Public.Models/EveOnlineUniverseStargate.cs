using System.ComponentModel.DataAnnotations.Schema;
using static eveDirect.Shared.EsiConnector.Models.UniverseStargateInfoResult;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseStargate: EveOnlineUniverseSystemLocation
    {
        [NotMapped]
        public int stargate_id
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

        [NotMapped]
        public Destination destination
        {
            get
            {
                return new Destination() { system_id = destination_system_id, stargate_id = destination_stargate_id };
            }
            set
            {
                if (value != null)
                {
                    destination_system_id = value.system_id;
                    destination_stargate_id = value.stargate_id;
                }
            }
        }
        public int destination_system_id { get; set; }
        public int destination_stargate_id { get; set; }
    }
}