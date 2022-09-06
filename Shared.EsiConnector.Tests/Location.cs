using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Location:BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/location/
        /// </summary>
        [Fact]
        public void CharacterLocationResult()
        {
            ExecuteAndOutput(connector.Location.Location());
        }
        /// <summary>
        /// GET /characters/{character_id}/ship/
        /// </summary>
        [Fact]
        public void CharacterShipResult()
        {
            ExecuteAndOutput(connector.Location.Ship());
        }
        /// <summary>
        /// GET /characters/{character_id}/online/
        /// </summary>
        [Fact]
        public void CharacterOnlineResult()
        {
            ExecuteAndOutput(connector.Location.Online());
        }

    }
}
