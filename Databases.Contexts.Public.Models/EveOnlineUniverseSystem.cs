using eveDirect.Shared.EsiConnector.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseSystem:UniverseSystemInfoResult
    {
        public EveOnlineUniverseSystem()
        {

        }
        public EveOnlineUniverseSystem(UniverseSystemInfoResult universeSystem)
        {
            constellation_id = universeSystem.constellation_id;
            security_class = universeSystem.security_class;
            security_status = universeSystem.security_status;
            system_id = universeSystem.system_id;
            name = universeSystem.name;
            star_id = universeSystem.star_id;
            planets = universeSystem.planets;
            stargates = universeSystem.stargates;
            stations = universeSystem.stations;
            position = universeSystem.position;
        }
        [Key]
        public new int system_id { get; set; }

        //Names
        //public string deName { get; set; }
        //public string enName { get; set; }
        //public string frName { get; set; }
        //public string jaName { get; set; }
        //public string ruName { get; set; }
        //public string zhName { get; set; }
        //public string koName { get; set; }

        [NotMapped]
        public new Position position
        {
            get
            {
                return new Position() { x = pX, y = pY, z = pZ };
            }
            set
            {
                if (value != null)
                {
                    pX = value.x;
                    pY = value.y;
                    pZ = value.z;
                }
            }
        }
        public float pX { get; set; }
        public float pY { get; set; }
        public float pZ { get; set; }
    }
}