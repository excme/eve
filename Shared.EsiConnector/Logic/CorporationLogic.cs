using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class CorporationLogic : BaseLogic
    {
        public CorporationLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /corporations/{corporation_id}/
        /// </summary>
        /// <param name="corporation_id"></param>
        /// <returns></returns>
        public EsiResponse<CorporationInfoResult> Information(int corporation_id)
            => Execute<CorporationInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/corporations/{corporation_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                });

        /// <summary>
        /// /corporations/{corporation_id}/alliancehistory/
        /// </summary>
        /// <param name="corporation_id"></param>
        /// <returns></returns>
        public EsiResponse<CorporationAllianceHistoryResult> AllianceHistory(int corporation_id)
            => Execute<CorporationAllianceHistoryResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/corporations/{corporation_id}/alliancehistory/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                });

        /// <summary>
        /// /corporations/{corporation_id}/members/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationMembersResult> Members()
            => Execute<CorporationMembersResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/members/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/roles/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationRolesResult> Roles()
            => Execute<CorporationRolesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/roles/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/roles/history/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationRolesHistoryResult> RolesHistory()
            => Execute<CorporationRolesHistoryResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/roles/history/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/icons/
        /// </summary>
        /// <param name="corporationId"></param>
        /// <returns></returns>
        public EsiResponse<CorporationIconResult> Icons(int corporation_id)
            => Execute<CorporationIconResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/corporations/{corporation_id}/icons/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                });

        /// <summary>
        /// /corporations/npccorps/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationNpccorpsResult> NpcCorps()
            => Execute<CorporationNpccorpsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/corporations/npccorps/");

        /// <summary>
        /// /corporations/{corporation_id}/structures/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationStructuresResult> Structures(int page = 1)
            => Execute<CorporationStructuresResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/structures/",
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
        /// /corporations/{corporation_id}/structures/{structure_id}/
        /// </summary>
        /// <param name="structure_id"></param>
        /// <returns></returns>
        public EsiResponse<string> UpdateStructureVulnerability(long structure_id, object new_schedule)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Put, "/corporations/{corporation_id}/structures/{structure_id}/",
                replacements: new Dictionary<string, string>() {
                    { "corporation_id", corporation_id.ToString() },
                    { "structure_id", structure_id.ToString() }
                },
                body: new_schedule,
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/membertracking/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationMembertrackingResult> MemberTracking()
            => Execute<CorporationMembertrackingResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/membertracking/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/divisions/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationDivisionsResult> Divisions()
            => Execute<CorporationDivisionsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/divisions/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/members/limit/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<int> MemberLimit()
            => Execute<int>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/members/limit/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/titles/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationTitlesResult> Titles()
            => Execute<CorporationTitlesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/titles/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/members/titles/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationMembersTitlesResult> MemberTitles()
            => Execute<CorporationMembersTitlesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/members/titles/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/blueprints/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationBlueprintsResult> Blueprints(int page = 1)
            => Execute<CorporationBlueprintsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/blueprints/",
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
        /// /corporations/{corporation_id}/standings/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<StandingsResult> Standings(int page = 1)
            => Execute<StandingsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/standings/",
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
        /// /corporations/{corporation_id}/starbases/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationStarbasesResult> Starbases(int page = 1)
            => Execute<CorporationStarbasesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/starbases/",
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
        /// /corporations/{corporation_id}/starbases/{starbase_id}/
        /// </summary>
        /// <param name="starbase_id"></param>
        /// <param name="system_id"></param>
        /// <returns></returns>
        public EsiResponse<CorporationStarbasesInfoResult> Starbase(long starbase_id, int system_id)
            => Execute<CorporationStarbasesInfoResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/starbases/{starbase_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() },
                    { "starbase_id", starbase_id.ToString() }
                },
                parameters: new string[]
                {
                    $"system_id={system_id}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/containers/logs/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationContainersLogResult> ContainerLogs(int page = 1)
            => Execute<CorporationContainersLogResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/containers/logs/",
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
        /// /corporations/{corporation_id}/facilities/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationFacilitiesResult> Facilities()
            => Execute<CorporationFacilitiesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/facilities/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/medals/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationMedalsResult> Medals(int page = 1)
            => Execute<CorporationMedalsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/medals/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                }, parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/medals/issued/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationMedalsIssuedResult> MedalsIssued(int page = 1)
            => Execute<CorporationMedalsIssuedResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/medals/issued/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        ///// <summary>
        ///// /corporations/{corporation_id}/outposts/
        ///// </summary>
        ///// <param name="page"></param>
        ///// <returns></returns>
        //public EsiResponse<CorporationOutpostsResult> Outposts(int page = 1)
        //    => Execute<CorporationOutpostsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/outposts/",
        //        replacements: new Dictionary<string, string>()
        //        {
        //            { "corporation_id", corporation_id.ToString() }
        //        },
        //        parameters: new string[]
        //        {
        //            $"page={page}"
        //        },
        //        token: _data.Token);

        ///// <summary>
        ///// /corporations/{corporation_id}/outposts/{outpost_id}/
        ///// </summary>
        ///// <param name="outpost_id"></param>
        ///// <returns></returns>
        //public EsiResponse<CorporationOutpostsInfoResult> Outpost(int outpost_id)
        //    => Execute<CorporationOutpostsInfoResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/outposts/{outpost_id}/",
        //        replacements: new Dictionary<string, string>()
        //        {
        //            { "corporation_id", corporation_id.ToString() },
        //            { "outpost_id", outpost_id.ToString() }
        //        },
        //        token: _data.Token);

        /// <summary>
        /// /corporations/{corporation_id}/shareholders/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationShareholdersResult> Shareholders(int page = 1)
            => Execute<CorporationShareholdersResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/shareholders/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                }, parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);
    }
}