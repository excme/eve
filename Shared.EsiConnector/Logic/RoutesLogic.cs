using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.EsiConnector.Models;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class RoutesLogic : BaseLogic
    {
        public RoutesLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger) : base(client, config, logger) { }

        /// <summary>
        /// /route/{origin}/{destination}/
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="flag"></param>
        /// <param name="avoid"></param>
        /// <param name="connections"></param>
        /// <returns></returns>
        public EsiResponse<RouteInfoResult> Map(
            int origin, 
            int destination, 
            RoutesFlag flag = RoutesFlag.Shortest, 
            int[] avoid = null, 
            int[] connections = null)
        {
            var parameters = new List<string>() { $"flag={flag.ToEsiValue()}" };

            if (avoid != null)
                parameters.Add($"&avoid={string.Join(",", avoid)}");

            if (connections != null)
                parameters.Add($"&connections={string.Join(",", connections)}");

            var response = Execute<RouteInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/route/{origin}/{destination}/",
                replacements: new Dictionary<string, string>()
                {
                    { "origin", origin.ToString() },
                    { "destination", destination.ToString() }
                },
                parameters: parameters.ToArray());

            return response;
        }
    }
}