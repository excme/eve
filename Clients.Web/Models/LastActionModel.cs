using System;

namespace eveDirect.Clients.Web.Models
{
    /// <summary>
    /// Модель последних действий
    /// </summary>
    public class LastActionModel
    {
        public long i { get; set; }

        public long item_id { get; set; }
        public ELastActionType type { get; set; }
        public DateTime? dt { get; set; }

        public System.Dynamic.ExpandoObject data { get; set; }
    }

    public enum ELastActionType : byte
    {
        // Warface
        Killmail = 1,
        War = 4,

        // Market
        Contract  = 2,
        Order = 3,

        // Character
        char_New = 5,
        char_Migration = 6,

        // Corporation
        corp_New = 7,
        corp_Migration = 8,

        // Alliance
        ally_New = 9
    }
}
