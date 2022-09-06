using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class PlanetaryInteraction : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/planets/
        /// </summary>
        [Fact]
        public void CharacterPlanetsResult()
        {
            ExecuteAndOutput(connector.PlanetaryInteraction.Planets());
        }

        /// <summary>
        /// GET /characters/{character_id}/planets/{planet_id}/
        /// </summary>
        [Fact]
        public void CharactersPlanetColonyResult()
        {
            var planetId = 40265703;
            ExecuteAndOutput(connector.PlanetaryInteraction.Planet(planetId));
        }

        /// <summary>
        /// GET /universe/schematics/{schematic_id}/
        /// </summary>
        [Fact]
        public void UniverseSchematicInfoResult()
        {
            int schematicId = 128;
            ExecuteAndOutput(connector.PlanetaryInteraction.SchematicInformation(schematicId));
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/customs_offices/
        /// </summary>
        [Fact]
        public void CorporationCustomOfficesResult()
        {
            ExecuteAndOutput(connector.PlanetaryInteraction.CorporationCustomsOffices());
        }
    }
}
