using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class SkillsLogic : BaseLogic
    {
        public SkillsLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /characters/{character_id}/attributes/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterAttributesResult> Attributes()
            => Execute<CharacterAttributesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/attributes/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/skills/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterSkillsResult> List()
            => Execute<CharacterSkillsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/skills/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/skillqueue/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterSkillqueueResult> Queue()
            => Execute<CharacterSkillqueueResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/skillqueue/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);
    }
}