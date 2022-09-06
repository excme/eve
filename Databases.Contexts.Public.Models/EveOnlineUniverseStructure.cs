using eveDirect.Shared.EsiConnector.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    //public class EveOnlineUniverseStructure : UniverseStructureInfoResult
    //{
    //    [Key]
    //    public long structure_id { get; set; }
    //    public int corporation_id { get; set; }
    //    public DateTime? lastUpdate { get; set; }

    //    [NotMapped]
    //    public new Position position
    //    {
    //        get
    //        {
    //            return new Position() { x = pX, y = pY, z = pZ };
    //        }
    //        set
    //        {
    //            if (value != null)
    //            {
    //                pX = value.x;
    //                pY = value.y;
    //                pZ = value.z;
    //            }
    //        }
    //    }
    //    public float pX { get; set; }
    //    public float pY { get; set; }
    //    public float pZ { get; set; }
    //}
}