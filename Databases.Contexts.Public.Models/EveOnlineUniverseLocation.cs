using eveDirect.Shared.EsiConnector.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseLocation 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        /// <summary>
        /// Родительская локация. 
        /// У луны - это планета
        /// </summary>
        public long parent_id { get; set; }

        public long? region_id { get; set; }
        public long? constellation_id { get; set; }
        public long? system_id { get; set; }
        public long? planet_id { get; set; }
        // Для структур
        public int? type_id { get; set; }
        // Для структур
        public int? owner_id { get; set; }
        // Для систем и ниже
        public double? security_status { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string name { get; set; }

        public EUniverseLocationType type { get; set; }
        public Position position { get; set; } = new Position();

        // Доп. информация о внутренностях
        public virtual UniverseRegionInfoResult regionInfo { get; set; }
        public virtual UniverseConstellationInfoResult constellationInfo { get; set; }
        public virtual UniverseSystemInfoResult systemInfo { get; set; }
        public virtual UniversePlanetInfoResult planetInfo { get; set; }
        public virtual UniverseStargateInfoResult stargateInfo { get; set; }
        public virtual UniverseMoonInfoResult moonInfo { get; set; }
        public virtual UniverseStarInfoResult starInfo { get; set; } 
        public virtual UniverseStationInfoResult stationInfo { get; set; } 
        public virtual UniverseAsteroidBeltInfoResult asteroidBeltInfo { get; set; } 
        public virtual CorporationStructuresResult.CorporationStructuresItem structureInfo { get; set; }
    }
    public class RegionInfo : UniverseRegionInfoResult
    {

    }
    public class ConstellationInfo: UniverseConstellationInfoResult {

    }
    public class SystemInfo : UniverseSystemInfoResult
    {

    }
    public class PlanetInfo : UniversePlanetInfoResult
    {

    }
    public class StargateInfo : UniverseStargateInfoResult
    {

    }
    public class MoonInfo : UniverseMoonInfoResult
    {

    }
    public class StarInfo : UniverseStarInfoResult
    {

    }
    public class StationInfo : UniverseStationInfoResult
    {

    }
    public class AsteroidBeltInfo : UniverseAsteroidBeltInfoResult
    {

    }
    public enum EUniverseLocationType : byte
    {
        Unknown = 0,
        Region = 1,
        Constellation = 2,
        System = 3,
        Planet = 4,
        Stargate = 5,
        Moon = 6,
        Star = 7,
        Station = 8,
        AsteroidBelt = 9,
        Structure = 10
    }
}
