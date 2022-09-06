using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class FleetsLogic : BaseLogic
    {
        public FleetsLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// GET /fleets/{fleet_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <returns></returns>
        public EsiResponse<FleetInfoResult> Settings(long fleet_id)
            => Execute<FleetInfoResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/fleets/{fleet_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /fleets/{fleet_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="motd"></param>
        /// <param name="is_free_move"></param>
        /// <returns></returns>
        public EsiResponse<string> UpdateSettings(long fleet_id, string motd = null, bool? is_free_move = null)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Put, "/fleets/{fleet_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() }
                },
                body: BuildUpdateSettingsObject(motd, is_free_move),
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/fleet/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterFleetInfoResult> FleetInfo()
            => Execute<CharacterFleetInfoResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/fleet/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /fleets/{fleet_id}/members/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <returns></returns>
        public EsiResponse<FleetMembersResult> Members(long fleet_id)
            => Execute<FleetMembersResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/fleets/{fleet_id}/members/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /fleets/{fleet_id}/members/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="character_id"></param>
        /// <param name="role"></param>
        /// <param name="wing_id"></param>
        /// <param name="squad_id"></param>
        /// <returns></returns>
        public EsiResponse<string> InviteCharacter(long fleet_id, int character_id, FleetRole role, long wing_id = 0, long squad_id = 0)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/fleets/{fleet_id}/members/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() }
                },
                body: BuildFleetInviteObject(character_id, role, wing_id, squad_id),
                token: _data.AccessToken);

        /// <summary>
        /// Put /fleets/{fleet_id}/members/{member_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="member_id"></param>
        /// <param name="role"></param>
        /// <param name="wing_id"></param>
        /// <param name="squad_id"></param>
        /// <returns></returns>
        public EsiResponse<string> MoveCharacter(long fleet_id, int member_id, FleetRole role, long wing_id = 0, long squad_id = 0)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Put, "/fleets/{fleet_id}/members/{member_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() },
                    { "member_id", member_id.ToString() }
                },
                body: BuildFleetInviteObject(character_id, role, wing_id, squad_id),
                token: _data.AccessToken);

        /// <summary>
        /// Delete /fleets/{fleet_id}/members/{member_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public EsiResponse<string> KickCharacter(long fleet_id, int member_id)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Delete, "/fleets/{fleet_id}/members/{member_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() },
                    { "member_id", member_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// Get /fleets/{fleet_id}/wings/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <returns></returns>
        public EsiResponse<FleetWingsResult> Wings(long fleet_id)
            => Execute<FleetWingsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/fleets/{fleet_id}/wings/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// Post /fleets/{fleet_id}/wings/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <returns></returns>
        public EsiResponse<FleetAddingResult> CreateWing(long fleet_id)
            => Execute<FleetAddingResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/fleets/{fleet_id}/wings/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// Put /fleets/{fleet_id}/wings/{wing_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="wing_id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public EsiResponse<string> RenameWing(long fleet_id, long wing_id, string name)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Put, "/fleets/{fleet_id}/wings/{wing_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() },
                    { "wing_id", wing_id.ToString() }
                },
                body: new
                {
                    name
                },
                token: _data.AccessToken);

        /// <summary>
        /// Delete /fleets/{fleet_id}/wings/{wing_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="wing_id"></param>
        /// <returns></returns>
        public EsiResponse<string> DeleteWing(long fleet_id, long wing_id)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Delete, "/fleets/{fleet_id}/wings/{wing_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() },
                    { "wing_id", wing_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// Post /fleets/{fleet_id}/wings/{wing_id}/squads/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="wing_id"></param>
        /// <returns></returns>
        public EsiResponse<FleetAddingSquadResult> CreateSquad(long fleet_id, long wing_id)
            => Execute<FleetAddingSquadResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/fleets/{fleet_id}/wings/{wing_id}/squads/",
                replacements: new Dictionary<string, string>()
                {
                    { "fleet_id", fleet_id.ToString() },
                    { "wing_id", wing_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// Put /fleets/{fleet_id}/squads/{squad_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="squad_id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public EsiResponse<string> RenameSquad(long fleet_id, long squad_id, string name)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Put, "/fleets/{fleet_id}/squads/{squad_id}/", replacements: new Dictionary<string, string>()
            {
                { "fleet_id", fleet_id.ToString() },
                { "squad_id", squad_id.ToString() }
            }, body: new
            {
                name
            }, token: _data.AccessToken);

        /// <summary>
        /// Delete /fleets/{fleet_id}/squads/{squad_id}/
        /// </summary>
        /// <param name="fleet_id"></param>
        /// <param name="squad_id"></param>
        /// <returns></returns>
        public EsiResponse<string> DeleteSquad(long fleet_id, long squad_id)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Delete, "/fleets/{fleet_id}/squads/{squad_id}/", replacements: new Dictionary<string, string>()
            {
                { "fleet_id", fleet_id.ToString() },
                { "squad_id", squad_id.ToString() }
            }, token: _data.AccessToken);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="motd"></param>
        /// <param name="is_free_move"></param>
        /// <returns></returns>
        private static dynamic BuildUpdateSettingsObject(string motd, bool? is_free_move)
        {
            dynamic body = null;

            if (motd != null)
                body = new { motd };
            if (is_free_move != null)
                body = new { is_free_move };
            if (motd != null && is_free_move != null)
                body = new { motd, is_free_move };

            return body;
        }

        /// <summary>
        /// Dynamically builds the required structure for a fleet invite or move
        /// </summary>
        /// <param name="character_id"></param>
        /// <param name="role"></param>
        /// <param name="wing_id"></param>
        /// <param name="squad_id"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private static dynamic BuildFleetInviteObject(int character_id, FleetRole role, long wing_id, long squad_id)
        {
            dynamic body = null;

            if (role == FleetRole.FleetCommander)
                body = new { character_id, role = role.ToEsiValue() };

            else if (role == FleetRole.WingCommander)
                body = new { character_id, role = role.ToEsiValue(), wing_id };

            else if (role == FleetRole.SquadCommander || role == FleetRole.SquadMember)
                body = new { character_id, role = role.ToEsiValue(), wing_id, squad_id };

            return body;
        }
    }
}