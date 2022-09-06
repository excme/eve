using System;
using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/contacts/
    /// </summary>
    public class CharacterContactsResult:List<CharacterContactsResult.ContactsItem>
    {
        public class ContactsItem: CorporationContactsResult.ContactsItem
        {
            public bool is_blocked { get; set; }
        }
    }
    /// <summary>
    /// GET /corporations/{corporation_id}/contacts/
    /// </summary>
    public class CorporationContactsResult : List<CorporationContactsResult.ContactsItem>
    {
        public class ContactsItem: AllianceContactsResult.AllianceContactsItem
        {
            public bool is_watched { get; set; }
        }
    }
    /// <summary>
    /// GET /alliances/{alliance_id}/contacts/
    /// </summary>
    public class AllianceContactsResult : List<AllianceContactsResult.AllianceContactsItem> {
        public class AllianceContactsItem
        {
            public float standing { get; set; }
            public EContactType contact_type { get; set; }
            public int contact_id { get; set; }
            public List<long> label_ids { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EContactType : byte
        {
            character = 1,
            corporation = 2,
            alliance = 3,
            faction = 4
        }
    }
}
