using eveDirect.Shared.EsiConnector.Models.SSO;
using eveDirect.Shared.EsiConnector.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class UniverseLogic : BaseLogic
    {
        public UniverseLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger) : base(client, config, logger) { }

        /// <summary>
        /// /universe/bloodlines/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseBloodlinesResult> Bloodlines(ELanguages lang = ELanguages.en_us)
            =>  Execute<UniverseBloodlinesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/bloodlines/", parameters: new string[] { $"language={lang.ToArg()}" });

        /// <summary>
        /// /universe/categories/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseCategoriesResult> Categories()
            =>  Execute<UniverseCategoriesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/categories/");

        /// <summary>
        /// /universe/categories/{category_id}/
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseCategoryInfoResult> Category(int category_id, ELanguages lang = ELanguages.en_us)
            =>  Execute<UniverseCategoryInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/categories/{category_id}/", 
                replacements: new Dictionary<string, string>() {{ "category_id", category_id.ToString() }},
                parameters: new string[] { $"language={lang.ToArg()}" }
            );

        /// <summary>
        /// /universe/constellations/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseConstellationsResult> Constellations()
            =>  Execute<UniverseConstellationsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/constellations/");

        /// <summary>
        /// /universe/constellations/{constellation_id}/
        /// </summary>
        /// <param name="constellation_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseConstellationInfoResult> Constellation(int constellation_id)
            =>  Execute<UniverseConstellationInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/constellations/{constellation_id}/", replacements: new Dictionary<string, string>()
            {
                { "constellation_id", constellation_id.ToString() }
            });

        /// <summary>
        /// /universe/factions/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseFactionsResult> Factions(ELanguages lang = ELanguages.en_us)
            =>  Execute<UniverseFactionsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/factions/",
                parameters: new string[] { $"language={lang.ToArg()}" }
            );

        /// <summary>
        /// /universe/graphics/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseGraphicsResult> Graphics()
            =>  Execute<UniverseGraphicsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/graphics/");

        /// <summary>
        /// /universe/graphics/{graphic_id}/
        /// </summary>
        /// <param name="graphic_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseGraphicInfoResult> Graphic(int graphic_id)
            =>  Execute<UniverseGraphicInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/graphics/{graphic_id}/", 
                replacements: new Dictionary<string, string>()
            {
                { "graphic_id", graphic_id.ToString() }
            });

        /// <summary>
        /// /universe/groups/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseGroupsResult> Groups(int page = 1)
            =>  Execute<UniverseGroupsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/groups/", parameters: new string[]
                {
                    $"page={page}"
                });

        /// <summary>
        /// /universe/groups/{group_id}/
        /// </summary>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseGroupInfoResult> Group(int group_id, ELanguages lang = ELanguages.en_us)
            =>  Execute<UniverseGroupInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/groups/{group_id}/",
                replacements: new Dictionary<string, string>() { { "group_id", group_id.ToString() } },
                parameters: new string[]
                {
                    $"language={lang.ToArg()}"
                });

        /// <summary>
        /// /universe/moons/{moon_id}/
        /// </summary>
        /// <param name="moon_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseMoonInfoResult> Moon(int moon_id)
            =>  Execute<UniverseMoonInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/moons/{moon_id}/", replacements: new Dictionary<string, string>()
            {
                { "moon_id", moon_id.ToString() }
            });

        /// <summary>
        /// Post /universe/names/
        /// </summary>
        /// <param name="any_ids">The ids to resolve; Supported IDs for resolving are: Characters, Corporations, Alliances, Stations, Solar Systems, Constellations, Regions, Types.</param>
        /// <returns></returns>
        public EsiResponse<UniverseNamesResult> Names(List<int> any_ids)
            =>  Execute<UniverseNamesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Post, "/universe/names/", body: any_ids.ToArray());

        /// <summary>
        /// /universe/ids/
        /// </summary>
        /// <param name="names">Resolve a set of names to IDs in the following categories: agents, alliances, characters, constellations, corporations factions, inventory_types, regions, stations, and systems. Only exact matches will be returned. All names searched for are cached for 12 hours.</param>
        /// <returns></returns>
        public EsiResponse<UniverseIdResult> IDs(List<string> names)
            =>  Execute<UniverseIdResult>(_client, _config, RequestSecurity.Public, RequestMethod.Post, "/universe/ids/", body: names.ToArray());

        /// <summary>
        /// /universe/planets/{planet_id}/
        /// </summary>
        /// <param name="planet_id"></param>
        /// <returns></returns>
        public EsiResponse<UniversePlanetInfoResult> Planet(int planet_id)
            =>  Execute<UniversePlanetInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/planets/{planet_id}/", replacements: new Dictionary<string, string>()
            {
                { "planet_id", planet_id.ToString() }
            });

        /// <summary>
        /// /universe/races/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseRacesResult> Races(ELanguages lang = ELanguages.en_us)
            =>  Execute<UniverseRacesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/races/", parameters: new string[]{
                    $"language={lang.ToArg()}"
        });

        /// <summary>
        /// /universe/regions/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseRegionsResult> Regions()
            =>  Execute<UniverseRegionsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/regions/");

        /// <summary>
        /// /universe/regions/{region_id}/
        /// </summary>
        /// <param name="region_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseRegionInfoResult> Region(int region_id)
            =>  Execute<UniverseRegionInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/regions/{region_id}/", replacements: new Dictionary<string, string>()
            {
                { "region_id", region_id.ToString() }
            });

        /// <summary>
        /// /universe/stations/{station_id}/
        /// </summary>
        /// <param name="station_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseStationInfoResult> Station(int station_id)
            =>  Execute<UniverseStationInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/stations/{station_id}/", replacements: new Dictionary<string, string>()
            {
                { "station_id", station_id.ToString() }
            });

        /// <summary>
        /// /universe/structures/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseStructuresResult> Structures()
            =>  Execute<UniverseStructuresResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/structures/");

        /// <summary>
        /// /universe/structures/{structure_id}/
        /// </summary>
        /// <param name="structure_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseStructureInfoResult> Structure(long structure_id)
            =>  Execute<UniverseStructureInfoResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/universe/structures/{structure_id}/", replacements: new Dictionary<string, string>()
            {
                { "structure_id", structure_id.ToString() }
            }, token: _data.AccessToken);

        /// <summary>
        /// /universe/systems/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseSystemsResult> Systems()
            =>  Execute<UniverseSystemsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/systems/");

        /// <summary>
        /// /universe/systems/{system_id}/
        /// </summary>
        /// <param name="system_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseSystemInfoResult> System(int system_id)
            =>  Execute<UniverseSystemInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/systems/{system_id}/", replacements: new Dictionary<string, string>()
            {
                { "system_id", system_id.ToString() }
            });

        /// <summary>
        /// /universe/types/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseTypesResult> Types(int page = 1)
            =>  Execute<UniverseTypesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/types/",
                parameters: new string[]
                {
                    $"page={page}"
                });

        /// <summary>
        /// /universe/types/{type_id}/
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseTypeInfoResult> Type(int type_id, ELanguages lang = ELanguages.en_us)
            =>  Execute<UniverseTypeInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/types/{type_id}/", 
                replacements: new Dictionary<string, string>(){{ "type_id", type_id.ToString()} },
                parameters: new string[] { $"language={lang.ToArg()}" }
                );

        /// <summary>
        /// /universe/stargates/{stargate_id}/
        /// </summary>
        /// <param name="stargate_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseStargateInfoResult> Stargate(int stargate_id)
            =>  Execute<UniverseStargateInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/stargates/{stargate_id}/", replacements: new Dictionary<string, string>()
            {
                { "stargate_id", stargate_id.ToString() }
            });

        /// <summary>
        /// /universe/system_jumps/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseSystemJumpsResult> Jumps()
            =>  Execute<UniverseSystemJumpsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/system_jumps/");

        /// <summary>
        /// /universe/system_kills/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseSystemKillsResult> Kills()
            =>  Execute<UniverseSystemKillsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/system_kills/");

        /// <summary>
        /// /universe/stars/{star_id}/
        /// </summary>
        /// <param name="star_id"></param>
        /// <returns></returns>
        public EsiResponse<UniverseStarInfoResult> Star(int star_id)
            =>  Execute<UniverseStarInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/stars/{star_id}/", replacements: new Dictionary<string, string>()
            {
                { "star_id", star_id.ToString() }
            });

        /// <summary>
        /// /universe/ancestries/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseAncestriesResult> Ancestries(ELanguages lang = ELanguages.en_us)
            =>  Execute<UniverseAncestriesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/ancestries/", parameters: new string[] { $"language={lang.ToArg()}" });

        /// <summary>
        /// /universe/asteroid_belts/{asteroid_belt_id}/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<UniverseAsteroidBeltInfoResult> AsteroidBelt(int asteroid_belt_id)
            =>  Execute<UniverseAsteroidBeltInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/universe/asteroid_belts/{asteroid_belt_id}/", replacements: new Dictionary<string, string>()
            {
                { "asteroid_belt_id", asteroid_belt_id.ToString() }
            });
    }
}
