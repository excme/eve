using eveDirect.Shared.EsiConnector.Models;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class StatusLogic : BaseLogic
    {
        public StatusLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger) : base(client, config, logger) { }

        public EsiResponse<StatusResult> Retrieve()
            => Execute<StatusResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/status/");
    }
}