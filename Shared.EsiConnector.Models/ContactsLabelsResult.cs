using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/contacts/labels/
    /// GET /corporations/{corporation_id}/contacts/labels/
    /// GET /alliances/{alliance_id}/contacts/labels/
    /// </summary>
    public class ContactsLabelsResult:List<ContactsLabelsResult.ContactsLabelsItem>
    {
        public class ContactsLabelsItem
        {
            public long label_id { get; set; }
            public string label_name { get; set; }
        }
    }
}
