using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/icons/
    /// </summary>
    public class CorporationIconsResult
    {
        public string px64x64 { get; set; }
        public string px128x128 { get; set; }
        public string px256x256 { get; set; }
    }
}
