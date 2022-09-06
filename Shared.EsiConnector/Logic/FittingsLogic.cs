using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class FittingsLogic : BaseLogic
    {
        public FittingsLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /characters/{character_id}/fittings/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterFittingsResult> List()
            =>  Execute<CharacterFittingsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/fittings/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// Post /characters/{character_id}/fittings/
        /// </summary>
        /// <param name="fitting"></param>
        /// <returns></returns>
        public EsiResponse<CharacterFittingAddingResult> Add(object fitting)
            =>  Execute<CharacterFittingAddingResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/characters/{character_id}/fittings/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                body: fitting,
                token: _data.AccessToken);

        /// <summary>
        /// Delete /characters/{character_id}/fittings/{fitting_id}/
        /// </summary>
        /// <param name="fitting_id"></param>
        /// <returns></returns>
        public EsiResponse<string> Delete(int fitting_id)
            =>  Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Delete, "/characters/{character_id}/fittings/{fitting_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "fitting_id", fitting_id.ToString() }
                },
                token: _data.AccessToken);
    }
}