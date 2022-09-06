using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class PlanetaryInteractionLogic : BaseLogic
    {
        public PlanetaryInteractionLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /characters/{character_id}/planets/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterPlanetsResult> Planets()
            => Execute<CharacterPlanetsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/planets/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/planets/{planet_id}/
        /// </summary>
        /// <param name="planet_id"></param>
        /// <returns></returns>
        public EsiResponse<CharactersPlanetColonyResult> Planet(int planet_id)
            => Execute<CharactersPlanetColonyResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/planets/{planet_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "planet_id", planet_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/customs_offices/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationCustomOfficesResult> CorporationCustomsOffices()
            => Execute<CorporationCustomOfficesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/customs_offices/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /universe/schematics/{schematic_id}/
        /// </summary>
        /// <param name="schematic_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseSchematicInfoResult> SchematicInformation(int schematic_id)
            => Execute<UniverseSchematicInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/schematics/{schematic_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "schematic_id", schematic_id.ToString() }
                });
    }
}