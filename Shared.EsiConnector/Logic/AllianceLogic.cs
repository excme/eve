using eveDirect.Shared.EsiConnector.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class AllianceLogic : BaseLogic
    {
        public AllianceLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger) : base(client, config, logger) { }

        /// <summary>
        /// /alliances/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<AlliancesResult> All()
            => Execute<AlliancesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/alliances/");

        /// <summary>
        /// /alliances/{alliance_id}/
        /// </summary>
        /// <param name="allianceId"></param>
        /// <returns></returns>
        public EsiResponse<AllianceInfoResult> Information(int alliance_id)
            => Execute<AllianceInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/alliances/{alliance_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "alliance_id", alliance_id.ToString() }
                });

        /// <summary>
        /// /alliances/{alliance_id}/corporations/
        /// </summary>
        /// <param name="alliance_id"></param>
        /// <returns></returns>
        public EsiResponse<AlliancesCorporationsResult> Corporations(int alliance_id)
            => Execute<AlliancesCorporationsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/alliances/{alliance_id}/corporations/",
                replacements: new Dictionary<string, string>()
                {
                    { "alliance_id", alliance_id.ToString() }
                });

        /// <summary>
        /// /alliances/{alliance_id}/icons/
        /// </summary>
        /// <param name="alliance_id"></param>
        /// <returns></returns>
        public EsiResponse<AllianceIconResult> Icons(int alliance_id)
            => Execute<AllianceIconResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/alliances/{alliance_id}/icons/",
                replacements: new Dictionary<string, string>()
                {
                    { "alliance_id", alliance_id.ToString() }
                });
    }
}