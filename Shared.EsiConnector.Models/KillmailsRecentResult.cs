using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/killmails/recent/
    /// GET /characters/{character_id}/killmails/recent/
    /// </summary>
    public class KillmailsRecentResult:List<KillmailsRecentResult.KillMailBase>, ISsoResult
    {
        public class KillMailBase
        {
            public virtual int killmail_id { get; set; }
            /// <summary>
            /// Хэш в sha1, поэтому длина 40
            /// </summary>
            public string killmail_hash { get; set; }
        }
    }
}
