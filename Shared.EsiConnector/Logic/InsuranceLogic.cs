using eveDirect.Shared.EsiConnector.Models;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class InsuranceLogic : BaseLogic
    {
        public InsuranceLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger) : base(client, config, logger) { }

        /// <summary>
        /// /insurance/prices/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<InsurancePricesResult> Levels()
            => Execute<InsurancePricesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/insurance/prices/");
    }
}