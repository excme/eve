using eveDirect.Shared.EsiConnector.Models;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class IncursionsLogic : BaseLogic
    {
        public IncursionsLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger) : base(client, config, logger) { }

        /// <summary>
        /// /incursions/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<IncursionResult> All()
            => Execute<IncursionResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/incursions/");
    }
}