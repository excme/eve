using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /alliances/{alliance_id}/icons/
    /// </summary>
    public class AllianceIconResult
    {
        public string px128x128 { get; set; }
        public string px64x64 { get; set; }
    }
}
