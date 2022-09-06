using eveDirect.Shared.EsiConnector.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class WarsLogic : BaseLogic
    {
        public WarsLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger) : base(client, config, logger) { }

        /// <summary>
        /// /wars/
        /// </summary>
        /// <param name="max_war_id">Only return wars with ID smaller than this</param>
        public EsiResponse<WarsResult> All(long max_war_id)
        {
            var parameters = new List<string>();

            if (max_war_id > 0)
                parameters.Add($"max_war_id={max_war_id}");

            var response = Execute<WarsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/wars/",
                parameters: parameters.ToArray());

            return response;
        }
        /// <summary>
        /// /wars/
        /// </summary>
        /// <param name="max_war_id">Only return wars with ID smaller than this</param>
        public EsiResponse<WarsResult> All()
        {
            return All(0);
        }

        /// <summary>
        /// /wars/{warId}/
        /// </summary>
        /// <param name="war_id"></param>
        /// <returns></returns>
        public EsiResponse<WarInfoResult> Information(int war_id)
            => Execute<WarInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/wars/{war_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "war_id", war_id.ToString() }
                });

        /// <summary>
        /// /wars/{warId}/killmails/
        /// </summary>
        /// <param name="war_id"></param>
        /// <param name="page"></param>
        public EsiResponse<WarKillmailsResult> Kills(int war_id, int page = 1)
            => Execute<WarKillmailsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/wars/{war_id}/killmails/",
                replacements: new Dictionary<string, string>()
                {
                    { "war_id", war_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                });
    }
}