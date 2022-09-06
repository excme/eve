using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class FactionWarfareLogic : BaseLogic
    {
        public FactionWarfareLogic(HttpClient client, EsiConfig config, ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /fw/wars/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<FwWarsResult> Wars()
            => Execute<FwWarsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/fw/wars/");

        /// <summary>
        /// /fw/stats/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<FwStatsResult> Stats()
            => Execute<FwStatsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/fw/stats/");

        /// <summary>
        /// /fw/systems/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<FwSystemsResult> Systems()
            => Execute<FwSystemsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/fw/systems/");

        /// <summary>
        /// fw/leaderboards/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<TotalFwLeaderboardsResult> Leaderboads()
            => Execute<TotalFwLeaderboardsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/fw/leaderboards/");

        /// <summary>
        /// /fw/leaderboards/corporations/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationFwLeaderboardsResult> LeaderboardsForCorporations()
            => Execute<CorporationFwLeaderboardsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/fw/leaderboards/corporations/");

        /// <summary>
        /// /fw/leaderboards/characters/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterFwLeaderboardsResult> LeaderboardsForCharacters()
            => Execute<CharacterFwLeaderboardsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/fw/leaderboards/characters/");

        /// <summary>
        /// /corporations/{corporation_id}/fw/stats/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationFwStatsResult> StatsForCorporation()
            => Execute<CorporationFwStatsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/fw/stats/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/fw/stats/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterFwStatsResult> StatsForCharacter()
            => Execute<CharacterFwStatsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/fw/stats/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);
    }
}