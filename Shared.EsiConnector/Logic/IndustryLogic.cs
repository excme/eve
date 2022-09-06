using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class IndustryLogic : BaseLogic
    {
        public IndustryLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /industry/facilities/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<IndustryFacilitiesResult> Facilities()
            => Execute<IndustryFacilitiesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/industry/facilities/");

        /// <summary>
        /// /industry/systems/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<IndustrySystemsResult> Systems()
            => Execute<IndustrySystemsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/industry/systems/");

        /// <summary>
        /// /characters/{character_id}/industry/jobs/
        /// </summary>
        /// <param name="include_completed"></param>
        /// <returns></returns>
        public EsiResponse<CharacterIndustryJobsResult> JobsForCharacter(bool include_completed = false)
            => Execute<CharacterIndustryJobsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/industry/jobs/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                parameters: new string[]
                {
                    $"include_completed={include_completed}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/mining/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CharacterIndustryMiningResult> MiningLedger(int page = 1)
            => Execute<CharacterIndustryMiningResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/mining/",
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
        /// /corporation/{corporation_id}/mining/observers/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationIndustryMiningObserversResult> Observers(int page = 1)
            => Execute<CorporationIndustryMiningObserversResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporation/{corporation_id}/mining/observers/",
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
        /// /corporation/{corporation_id}/mining/observers/{observer_id}/
        /// </summary>
        /// <param name="observer_id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationIndustryMiningObserverItemResult> ObservedMining(long observer_id, int page = 1)
            => Execute<CorporationIndustryMiningObserverItemResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporation/{corporation_id}/mining/observers/{observer_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() },
                    { "observer_id", observer_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/industry/jobs/
        /// </summary>
        /// <param name="include_completed"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationIndustryJobsResult> JobsForCorporation(bool include_completed = false, int page = 1)
            => Execute<CorporationIndustryJobsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/industry/jobs/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                parameters: new string[]
                {
                    $"include_completed={include_completed}",
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporation/{corporation_id}/mining/extractions/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationIndustryMiningExtractionsResult> Extractions()
            => Execute<CorporationIndustryMiningExtractionsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporation/{corporation_id}/mining/extractions/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);
    }
}