using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Get /corporations/{corporation_id}/icons/
    /// </summary>
    public class CorporationIconResult
    {
        public string px256x256 { get; set; }
        public string px128x128 { get; set; }
        public string px64x64 { get; set; }
    }
}
