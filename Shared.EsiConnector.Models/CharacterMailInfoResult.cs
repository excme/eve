using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/mail/{mail_id}/
    /// </summary>
    public class CharacterMailInfoResult
    {
        public class Recipient
        {
            public ERecipientType recipient_type { get; set; }
            public int recipient_id { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ERecipientType : byte
        {
            alliance = 1,
            character = 2,
            corporation = 3,
            mailing_list = 4
        }
        public string subject { get; set; }
        public int from { get; set; }
        public DateTime timestamp { get; set; }
        public List<Recipient> recipients { get; set; } = new List<Recipient>();
        public string body { get; set; }
        public List<int> labels { get; set; }
        public bool read { get; set; }
    }
}
