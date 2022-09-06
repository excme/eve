using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Fittings:BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/fittings/
        /// </summary>
        [Fact]
        public void CharacterFittingsResult()
        {
            ExecuteAndOutput(connector.Fittings.List());
        }
    }
}
