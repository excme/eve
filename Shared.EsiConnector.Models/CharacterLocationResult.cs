using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/location/
    /// </summary>
    public class CharacterLocationResult: ISsoResult
    {
        public int solar_system_id { get; set; }
        public long structure_id { get; set; }
        public int station_id { get;set; }

        public bool IsInOpenSpace()
        {
            return station_id == 0 && structure_id == 0;
        }
    }
}
