using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Universe : BaseConnector
    {
        /// <summary>
        /// GET /universe/planets/{planet_id}/
        /// </summary>
        [Fact]
        public void UniversePlanetInfoResult()
        {
            var planetId = 40265703;
            ExecuteAndOutput(connector.Universe.Planet(planetId));
        }

        /// <summary>
        /// GET /universe/stations/{station_id}/
        /// </summary>
        [Fact]
        public void UniverseStationResult()
        {
            var stationId = 60002959;
            ExecuteAndOutput(connector.Universe.Station(stationId));
        }

        /// <summary>
        /// GET /universe/structures/{structure_id}/
        /// </summary>
        [Fact]
        public void UniverseStructureInfoResult()
        {
            var structureId = 1023967281185;
            ExecuteAndOutput(connector.Universe.Structure(structureId));
        }

        /// <summary>
        /// GET /universe/systems/{system_id}/
        /// </summary>
        [Fact]
        public void UniverseSystemInfoResult()
        {
            var systemId = 30000003;
            ExecuteAndOutput(connector.Universe.System(systemId));
        }

        /// <summary>
        /// GET /universe/systems/
        /// </summary>
        [Fact]
        public void UniverseSystemsResult()
        {
            ExecuteAndOutput(connector.Universe.Systems());
        }

        /// <summary>
        /// GET /universe/types/{type_id}/
        /// </summary>
        [Fact]
        public void UniverseTypeInfoResult()
        {
            var typeId = 891;
            ExecuteAndOutput(connector.Universe.Type(typeId));
        }

        /// <summary>
        /// GET /universe/types/
        /// </summary>
        [Fact]
        public void UniverseTypesResult()
        {
            ExecuteAndOutput(connector.Universe.Types());
        }

        /// <summary>
        /// GET /universe/groups/{group_id}/
        /// </summary>
        [Fact]
        public void UniverseGroupInfoResult()
        {
            var groupId = 1327;
            ExecuteAndOutput(connector.Universe.Group(groupId));
        }

        /// <summary>
        /// GET /universe/groups/
        /// </summary>
        [Fact]
        public void UniverseGroupsResult()
        {
            ExecuteAndOutput(connector.Universe.Groups());
        }

        /// <summary>
        /// GET /universe/categories/{category_id}/
        /// </summary>
        [Fact]
        public void UniverseCategoryInfoResult()
        {
            var categoryId = 26;
            ExecuteAndOutput(connector.Universe.Category(categoryId));
        }

        /// <summary>
        /// GET /universe/categories/
        /// </summary>
        [Fact]
        public void UniverseCategoriesResult()
        {
            ExecuteAndOutput(connector.Universe.Categories());
        }

        /// <summary>
        /// GET /universe/structures/
        /// </summary>
        [Fact]
        public void UniverseStructuresResult()
        {
            ExecuteAndOutput(connector.Universe.Structures());
        }

        /// <summary>
        /// GET /universe/races/
        /// </summary>
        [Fact]
        public void UniverseRacesResult()
        {
            ExecuteAndOutput(connector.Universe.Races());
        }

        /// <summary>
        /// GET /universe/factions/
        /// </summary>
        [Fact]
        public void UniverseFactionsResult()
        {
            ExecuteAndOutput(connector.Universe.Factions());
        }

        /// <summary>
        /// GET /universe/bloodlines/
        /// </summary>
        [Fact]
        public void UniverseBloodlinesResult()
        {
            ExecuteAndOutput(connector.Universe.Bloodlines());
        }

        /// <summary>
        /// GET /universe/regions/
        /// </summary>
        [Fact]
        public void UniverseRegionsResult()
        {
            ExecuteAndOutput(connector.Universe.Regions());
        }

        /// <summary>
        /// GET /universe/regions/{region_id}/
        /// </summary>
        [Fact]
        public void UniverseRegionInfoResult()
        {
            var regionId = 11000021;
            ExecuteAndOutput(connector.Universe.Region(regionId));
        }

        /// <summary>
        /// GET /universe/constellations/
        /// </summary>
        [Fact]
        public void UniverseConstellationsResult()
        {
            ExecuteAndOutput(connector.Universe.Constellations());
        }

        /// <summary>
        /// GET /universe/constellations/{constellation_id}/
        /// </summary>
        [Fact]
        public void UniverseConstellationInfoResult()
        {
            var constellationId = 20000004;
            ExecuteAndOutput(connector.Universe.Constellation(constellationId));
        }

        /// <summary>
        /// GET /universe/moons/{moon_id}/
        /// </summary>
        [Fact]
        public void UniverseMoonInfoResult()
        {
            var moonId = 40000042;
            ExecuteAndOutput(connector.Universe.Moon(moonId));
        }

        /// <summary>
        /// GET /universe/stargates/{stargate_id}/
        /// </summary>
        [Fact]
        public void UniverseStargateInfoResult()
        {
            var stargateId = 50000342;
            ExecuteAndOutput(connector.Universe.Stargate(stargateId));
        }

        /// <summary>
        /// GET /universe/graphics/
        /// </summary>
        [Fact]
        public void UniverseGraphicsResult()
        {
            ExecuteAndOutput(connector.Universe.Graphics());
        }

        /// <summary>
        /// GET /universe/graphics/{graphic_id}/
        /// </summary>
        [Fact]
        public void UniverseGraphicInfoResult()
        {
            var graphicId = 20469;
            ExecuteAndOutput(connector.Universe.Graphic(graphicId));
        }

        /// <summary>
        /// GET /universe/system_jumps/
        /// </summary>
        [Fact]
        public void UniverseSystemJumpsResult()
        {
            ExecuteAndOutput(connector.Universe.Jumps());
        }

        /// <summary>
        /// GET /universe/system_kills/
        /// </summary>
        [Fact]
        public void UniverseSystemKillsResult()
        {
            ExecuteAndOutput(connector.Universe.Kills());
        }

        /// <summary>
        /// GET /universe/stars/{star_id}/
        /// </summary>
        [Fact]
        public void UniverseStarInfoResult()
        {
            var starId = 40000040;
            ExecuteAndOutput(connector.Universe.Star(starId));
        }

        /// <summary>
        /// GET /universe/ancestries/
        /// </summary>
        [Fact]
        public void UniverseAncestriesResult()
        {
            ExecuteAndOutput(connector.Universe.Ancestries());
        }
    }
}
