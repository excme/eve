using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/mail/
    /// </summary>
    public class CharacterMailsResult:List<CharacterMailsResult.CharacterMailsItem>
    {
        public class CharacterMailsItem
        {
            public int mail_id { get; set; }
            public string subject { get; set; }
            public int from { get; set; }
            [Column(TypeName = "smalldatetime")]
            public DateTime timestamp { get; set; }
            public List<int> labels { get; set; }
            public List<Recipient> recipients { get; set; }
            public bool is_read { get; set; }
            public class Recipient
            {
                public ERecipientType recipient_type { get; set; }
                public int recipient_id { get; set; }
            }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ERecipientType : byte
        {
            alliance = 1,
            character = 2,
            corporation = 3,
            mailing_list = 4
        }
    }
}
