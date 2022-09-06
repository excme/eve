using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Результат GET /characters/{character_id}/notifications/contacts/
    /// </summary>
    public class CharacterNotificationsContactsResult:List<CharacterNotificationsContactsResult.CharacterNotificationsContactsItem>, ISsoResult
    {
        public class CharacterNotificationsContactsItem
        {
            public int notification_id { get; set; }
            public int sender_character_id { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime send_date { get; set; }
            public float standing_level { get; set; }
            public string message { get; set; }
        }
    }
}
