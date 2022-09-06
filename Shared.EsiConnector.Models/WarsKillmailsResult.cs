using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /wars/{war_id}/killmails/
    /// </summary>
    public class WarKillmailsResult : List<WarKillmailsResult.KillMailItem>, ISsoResult
    {
        public class KillMailItem
        {
            public int killmail_id { get; set; }
            public string killmail_hash { get; set; }
        }
    }
}
