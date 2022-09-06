using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class CharacterLogic : BaseLogic
    {
        public CharacterLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /characters/affiliation/
        /// </summary>
        /// <param name="characterIds">dynamic = long</param>
        /// <returns></returns>
        public EsiResponse<CharacterAffiliationResult> Affiliation(List< int> character_ids)
            => Execute<CharacterAffiliationResult>(_client, _config, RequestSecurity.Public, RequestMethod.Post, "/characters/affiliation/",
                body: character_ids.ToArray());

        /// <summary>
        /// /characters/names/
        /// </summary>
        /// <param name="characterIds"></param>
        /// <returns></returns>
        public EsiResponse<CharacterNameResult> Names(params int[] character_ids)
            => Execute<CharacterNameResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/characters/names/",
                parameters: new string[]
                {
                    $"character_ids={string.Join(",", character_ids)}"
                });

        /// <summary>
        /// /characters/{character_id}/
        /// </summary>
        /// <param name="character_id"></param>
        /// <returns></returns>
        public EsiResponse<CharacterInfoResult> Information(int character_id)
            => Execute<CharacterInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/characters/{character_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                });

        /// <summary>
        /// /characters/{character_id}/agents_research/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterAgentsResearchResult> AgentsResearch()
            => Execute<CharacterAgentsResearchResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/agents_research/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/blueprints/
        /// </summary>
        /// <param name="page">Which page of results to return</param>
        /// <returns></returns>
        public EsiResponse<CharacterBlueprintsResult> Blueprints(int page = 1)
            => Execute<CharacterBlueprintsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/characters/{character_id}/blueprints/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                });

        /// <summary>
        /// /characters/{character_id}/chat_channels/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterChatChannelResult> ChatChannels()
            => Execute<CharacterChatChannelResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/chat_channels/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/corporationhistory/
        /// </summary>
        /// <param name="character_id"></param>
        /// <returns></returns>
        public EsiResponse<CharacterCorporationHistoryResult> CorporationHistory(int character_id)
            => Execute<CharacterCorporationHistoryResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/characters/{character_id}/corporationhistory/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                });

        /// <summary>
        /// /characters/{character_id}/cspa/
        /// </summary>
        /// <param name="character_ids">The target characters to calculate the charge for</param>
        /// <returns></returns>
        public EsiResponse<float> CalculateCSPA(object character_ids)
            => Execute<float>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/characters/{character_id}/cspa/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                body: character_ids,
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/fatigue/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterFatigueResult> Fatigue()
            => Execute<CharacterFatigueResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/fatigue/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/medals/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterMedalsResult> Medals()
            => Execute<CharacterMedalsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/medals/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/notifications/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterNotificationsResult> Notifications()
            => Execute<CharacterNotificationsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/notifications/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/notifications/contacts/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterNotificationsContactsResult> ContactNotifications()
            => Execute<CharacterNotificationsContactsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/notifications/contacts/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/portrait/
        /// </summary>
        /// <param name="character_id"></param>
        /// <returns></returns>
        public EsiResponse<ImagesResult> Portrait(int character_id)
            => Execute<ImagesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/characters/{character_id}/portrait/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                });

        /// <summary>
        /// /characters/{character_id}/roles/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterRolesResult> Roles()
            => Execute<CharacterRolesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/roles/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/standings/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<StandingsResult> Standings()
            => Execute<StandingsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/standings/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/stats/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterStatResult> Stats()
            => Execute<CharacterStatResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/stats/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/titles/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterTitlesResult> Titles()
            => Execute<CharacterTitlesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/titles/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);
    }
}