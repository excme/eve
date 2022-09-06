using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/fittings/
    /// </summary>
    public class FleetInfoResult
    {
        public string motd { get; set; }
        public bool is_free_move { get; set; }
        public bool is_registered { get; set; }
        public bool is_voice_enabled { get; set; }
    }
}
