using Xunit;

namespace eveDirect.EsiConnector.Tests
{

    public class Clones : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/clones/
        /// </summary>
        [Fact]
        public void CharacterClonesResult()
        {
            ExecuteAndOutput(connector.Clones.List());
        }

        /// <summary>
        /// GET /characters/{character_id}/implants/
        /// </summary>
        [Fact]
        public void CharacterImplantsResult()
        {
            ExecuteAndOutput(connector.Clones.Implants());
        }
    }
}
