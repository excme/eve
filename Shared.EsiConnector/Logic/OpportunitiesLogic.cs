using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;
using eveDirect.Shared.EsiConnector.Models;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class OpportunitiesLogic : BaseLogic
    {
        public OpportunitiesLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /opportunities/groups/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<OpportunitiesGroupsResult> Groups()
            =>  Execute<OpportunitiesGroupsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/opportunities/groups/");

        /// <summary>
        /// /opportunities/groups/{group_id}/
        /// </summary>
        /// <param name="group_id"></param>
        /// <returns></returns>
        public EsiResponse<OpportunitiesGroupInfoResult> Group(int group_id)
            =>  Execute<OpportunitiesGroupInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/opportunities/groups/{group_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "group_id", group_id.ToString() }
                });

        /// <summary>
        /// /opportunities/tasks/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<OpportunitiesTasksResult> Tasks()
            =>  Execute<OpportunitiesTasksResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/opportunities/tasks/");

        /// <summary>
        /// /opportunities/tasks/{task_id}/
        /// </summary>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public EsiResponse<OpportunitiesTaskInfoResult> Task(int task_id)
            =>  Execute<OpportunitiesTaskInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/opportunities/tasks/{task_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "task_id", task_id.ToString() }
                });

        /// <summary>
        /// /characters/{character_id}/opportunities/
        /// </summary>
        /// <param name="character_id"></param>
        /// <returns></returns>
        public EsiResponse<CharacterOpportunitiesResult> CharacterCompletedTasks()
            =>  Execute<CharacterOpportunitiesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/opportunities/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);
    }
}