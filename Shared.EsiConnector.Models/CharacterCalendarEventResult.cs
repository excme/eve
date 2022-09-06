using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/calendar/{event_id}/
    /// </summary>
    public class CharacterCalendarEventResult
    {
        public int event_id { get; set; }
        public int owner_id { get; set; }
        public string owner_name { get; set; }
        public DateTime date { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public int importance { get; set; }
        public string response { get; set; }
        public string text { get; set; }
        public EOwnerType owner_type { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EOwnerType : byte
        {
            eve_server = 1,
            corporation = 2,
            faction = 3,
            character = 4,
            alliance = 5
        }
    }
}
