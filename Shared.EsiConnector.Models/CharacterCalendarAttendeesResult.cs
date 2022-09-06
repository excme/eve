using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/calendar/{event_id}/attendees/
    /// </summary>
    public class CharacterCalendarAttendeesResult:List<CharacterCalendarAttendeesResult.CharacterCalendarAttendeesItem>
    {
        public class CharacterCalendarAttendeesItem
        {
            public int character_id { get; set; }
            public EEventResponse event_response { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EEventResponse : byte
        {
            declined = 1,
            not_responded = 2,
            accepted = 3,
            tentative = 4
        }
    }
}
