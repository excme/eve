using eveDirect.Shared.EsiConnector.Enumerations;
using Xunit;
namespace eveDirect.EsiConnector.Tests
{
    public class Calendar : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/calendar/
        /// </summary>
        [Fact]
        public void CharacterCalendarResult()
        {
            ExecuteAndOutput(connector.Calendar.Events());
        }
        /// <summary>
        /// GET /characters/{character_id}/calendar/{event_id}/
        /// </summary>
        [Fact]
        public void CharacterCalendarEventResult()
        {
            int eventid = 1603160;
            ExecuteAndOutput(connector.Calendar.Event(eventid));
        }
        /// <summary>
        /// PUT /characters/{character_id}/calendar/{event_id}/
        /// </summary>
        [Fact]
        public void Character()
        {
            int eventid = 1603160;
            ExecuteAndOutput(connector.Calendar.Respond(eventid, EventResponse.Accepted));
        }
        /// <summary>
        /// GET /characters/{character_id}/calendar/{event_id}/attendees/
        /// </summary>
        [Fact]
        public void CharacterCalendarAttendeesResult()
        {
            int eventid = 1603160;
            ExecuteAndOutput(connector.Calendar.Responses(eventid));
        }
    }
}
