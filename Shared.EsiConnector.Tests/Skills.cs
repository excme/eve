using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Skills : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/skillqueue/
        /// </summary>
        [Fact]
        public void CharacterSkillqueueResult() {
            ExecuteAndOutput(connector.Skills.Queue());
        }

        /// <summary>
        /// GET /characters/{character_id}/skills/
        /// </summary>
        [Fact]
        public void CharacterSkillsResult() {
            ExecuteAndOutput(connector.Skills.List());
        }

        /// <summary>
        /// GET /characters/{character_id}/attributes/
        /// </summary>
        [Fact]
        public void CharacterAttributesResult() {
            ExecuteAndOutput(connector.Skills.Attributes());
        }
    }
}
