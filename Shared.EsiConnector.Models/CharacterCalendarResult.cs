using System;
using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/calendar/
    /// </summary>
    public class CharacterCalendarResult:List<CharacterCalendarResult.CharacterCalendarItem>
    {
        public class CharacterCalendarItem
        {
            public int event_id { get; set; }
            public DateTime event_date { get; set; }
            public string title { get; set; }
            public int importance { get; set; }
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
