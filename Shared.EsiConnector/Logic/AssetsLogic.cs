using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class AssetsLogic : BaseLogic
    {
        public AssetsLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /characters/{character_id}/assets/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CharacterAssetsResult> ForCharacter(int page = 1)
            =>  Execute<CharacterAssetsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/assets/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/assets/locations/
        /// </summary>
        /// <param name="item_ids"></param>
        /// <returns></returns>
        public EsiResponse<AssetLocationResult> LocationsForCharacter(params long[] item_ids)
            =>  Execute<AssetLocationResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/characters/{character_id}/assets/locations/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                body: item_ids,
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/assets/names/
        /// </summary>
        /// <param name="item_ids"></param>
        /// <returns></returns>
        public EsiResponse<AssetNameResult> NamesForCharacter(params long[] item_ids)
            =>  Execute<AssetNameResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/characters/{character_id}/assets/names/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                body: item_ids,
                token: _data.AccessToken);


        /// <summary>
        /// /corporations/{corporation_id}/assets/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationAssetsResult> ForCorporation(int page = 1)
            =>  Execute<CorporationAssetsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/assets/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/assets/locations/
        /// </summary>
        /// <param name="item_ids"></param>
        /// <returns></returns>
        public EsiResponse<AssetLocationResult> LocationsForCorporation(params long[] item_ids)
            =>  Execute<AssetLocationResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/corporations/{corporation_id}/assets/locations/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                body: item_ids,
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/assets/names/
        /// </summary>
        /// <param name="item_ids"></param>
        /// <returns></returns>
        public EsiResponse<AssetNameResult> NamesForCorporation(params long[] item_ids)
            =>  Execute<AssetNameResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/corporations/{corporation_id}/assets/names/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                body: item_ids,
                token: _data.AccessToken);
    }
}