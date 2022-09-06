using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Opportunities : BaseConnector
    {
        /// <summary>
        /// GET /opportunities/groups/
        /// </summary>
        [Fact]
        public void OpportunitiesGroupsResult()
        {
            ExecuteAndOutput(connector.Opportunities.Groups());
        }

        /// <summary>
        /// GET /opportunities/groups/{group_id}/
        /// </summary>
        [Fact]
        public void OpportunitiesGroupInfoResult()
        {
            int groupId = 117;
            ExecuteAndOutput(connector.Opportunities.Group(groupId));
        }

        /// <summary>
        /// GET /opportunities/tasks/
        /// </summary>
        [Fact]
        public void OpportunitiesTasksResult()
        {
            ExecuteAndOutput(connector.Opportunities.Tasks());
        }

        /// <summary>
        /// GET /opportunities/tasks/{task_id}/
        /// </summary>
        [Fact]
        public void OpportunitiesTaskInfoResult() {
            var taskId = 72;
            ExecuteAndOutput(connector.Opportunities.Task(taskId));
        }

        /// <summary>
        /// GET /characters/{character_id}/opportunities/
        /// </summary>
        [Fact]
        public void CharacterOpportunitiesResult() {
            ExecuteAndOutput(connector.Opportunities.CharacterCompletedTasks());
        }


    }
}
