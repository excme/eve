using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class KillmailsLogic : BaseLogic
    {
        public KillmailsLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /characters/{character_id}/killmails/recent/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<KillmailsRecentResult> ForCharacter(int page = 1)
            =>  Execute<KillmailsRecentResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/killmails/recent/",
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
        /// /corporations/{corporation_id}/killmails/recent/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<KillmailsRecentResult> ForCorporation(int page = 1)
            =>  Execute<KillmailsRecentResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/killmails/recent/",
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
        /// /killmails/{killmail_id}/{killmail_hash}/
        /// </summary>
        /// <param name="killmail_hash">The killmail hash for verification</param>
        /// <param name="killmail_id">The killmail ID to be queried</param>
        /// <returns></returns>
        public EsiResponse<KillMailInfoResult> Information(int killmail_id, string killmail_hash)
            =>  Execute<KillMailInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/killmails/{killmail_id}/{killmail_hash}/",
                replacements: new Dictionary<string, string>()
                {
                    { "killmail_id", killmail_id.ToString() },
                    { "killmail_hash", killmail_hash.ToString() }
                });
    }
}