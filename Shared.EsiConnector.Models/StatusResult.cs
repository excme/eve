using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /status/
    /// </summary>
    public class StatusResult: ISsoResult
    {
        public DateTime start_time { get; set; }
        public int players { get; set; }
        public string server_version { get; set; }
        public bool vip { get; set; }
    }
}
