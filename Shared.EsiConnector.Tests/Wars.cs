using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Wars : BaseConnector
    {
        int[] callingWars = { 582353 };

        /// <summary>
        /// GET /wars/{war_id}/killmails/
        /// </summary>
        [Fact]
        public void WarsKillmailsResult()
        {
            foreach (var warId in callingWars)
            {
                ExecuteAndOutput(connector.Wars.Kills(warId));
            }
        }

        /// <summary>
        /// GET /wars/{war_id}/
        /// </summary>
        [Fact]
        public void WarInfoResult()
        {
            foreach (var warId in callingWars)
            {
                ExecuteAndOutput(connector.Wars.Information(warId));
            }
        }

        /// <summary>
        /// GET /wars/
        /// </summary>
        [Fact]
        public void WarsResult()
        {
            ExecuteAndOutput(connector.Wars.All());
        }
    }
}
