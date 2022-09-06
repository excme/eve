using eveDirect.Shared.EsiConnector.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class SovereigntyLogic : BaseLogic
    {
        public SovereigntyLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger) : base(client, config, logger) { }

        /// <summary>
        /// /sovereignty/campaigns/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<SovereigntyCampaignsResult> Campaigns()
            => Execute<SovereigntyCampaignsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/sovereignty/campaigns/");

        /// <summary>
        /// /sovereignty/map/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<SovereigntyMapsResult> Systems()
            => Execute<SovereigntyMapsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/sovereignty/map/");

        /// <summary>
        /// /sovereignty/structures/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<SovereigntyStructuresResult> Structures()
            => Execute<SovereigntyStructuresResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/sovereignty/structures/");
    }
}