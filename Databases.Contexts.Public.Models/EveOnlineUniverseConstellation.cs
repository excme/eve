using eveDirect.Shared.EsiConnector.Models;
using System.ComponentModel.DataAnnotations;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseConstellation:UniverseConstellationInfoResult
    {
        public EveOnlineUniverseConstellation()
        {

        }
        public EveOnlineUniverseConstellation(UniverseConstellationInfoResult universeConstellation)
        {
            constellation_id = universeConstellation.constellation_id;
            name = universeConstellation.name;
            region_id = universeConstellation.region_id;
            systems = universeConstellation.systems;
            position = universeConstellation.position;
        }
        [Key]
        public new int constellation_id { get; set; }

        //// Names
        //public string deName { get; set; }
        //public string enName { get; set; }
        //public string frName { get; set; }
        //public string jaName { get; set; }
        //public string ruName { get; set; }
        //public string zhName { get; set; }
        //public string koName { get; set; }

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