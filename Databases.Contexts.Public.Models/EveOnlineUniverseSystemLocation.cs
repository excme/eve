using eveDirect.Shared.EsiConnector.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public abstract class EveOnlineUniverseSystemLocation
    {
        [Key]
        public int location_id { get; set; }
        public string name { get; set; }

        public int? systemId { get; set; }
        public virtual EveOnlineUniverseSystem system { get; set; }

        public int? type_id { get; set; }

        [NotMapped]
        public Position position
        {
            get
            {
                return new Position() { x = pX, y = pY, z = pZ };
            }
            set
            {
                pX = value.x;
                pY = value.y;
                pZ = value.z;
            }
        }
        public float pX { get; set; }
        public float pY { get; set; }
        public float pZ { get; set; }
    }
}