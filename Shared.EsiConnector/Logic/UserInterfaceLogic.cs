using System.Net.Http;
using System.Threading.Tasks;
using eveDirect.Shared.EsiConnector.Models.SSO;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class UserInterfaceLogic : BaseLogic
    {
        public UserInterfaceLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /ui/openwindow/marketdetails/
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        public EsiResponse<string> MarketDetails(int type_id)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/ui/openwindow/marketdetails/",
                parameters: new string[]
                {
                    $"type_id={type_id}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /ui/openwindow/contract/
        /// </summary>
        /// <param name="contract_id"></param>
        /// <returns></returns>
        public EsiResponse<string> Contract(int contract_id)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/ui/openwindow/contract/",
                parameters: new string[]
                {
                    $"contract_id={contract_id}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /ui/openwindow/information/
        /// </summary>
        /// <param name="target_id"></param>
        /// <returns></returns>
        public EsiResponse<string> Information(int target_id)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/ui/openwindow/information/",
                parameters: new string[]
                {
                    $"target_id={target_id}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /ui/autopilot/waypoint/
        /// </summary>
        /// <param name="destination_id"></param>
        /// <param name="add_to_beginning"></param>
        /// <param name="clear_other_waypoints"></param>
        /// <returns></returns>
        public EsiResponse<string> Waypoint(long destination_id, bool add_to_beginning = false, bool clear_other_waypoints = false)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/ui/autopilot/waypoint/",
                parameters: new string[]
                {
                    $"destination_id={destination_id}",
                    $"add_to_beginning={add_to_beginning}",
                    $"clear_other_waypoints={clear_other_waypoints}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /ui/openwindow/newmail/
        /// </summary>
        /// <param name="subject">max length: 1000</param>
        /// <param name="body">max length: 10000</param>
        /// <param name="recipients">max: 50; this can be any of the following id types: character, corporation, alliance, mailing list; only multiple character ids can be specified</param>
        /// <param name="to_mailing_list_id"></param>
        /// <param name="to_corp_or_alliance_id"></param>
        /// <returns></returns>
        public EsiResponse<string> NewMail(string subject, string body, int[] recipients)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/ui/openwindow/newmail/",
                body: new
                {
                    subject,
                    body,
                    recipients
                },
                token: _data.AccessToken);
    }
}