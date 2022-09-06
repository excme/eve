using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/clones/
    /// </summary>
    public class CharacterClonesResult : ISsoResult
    {
        public class HomeLocation
        {
            public long location_id { get; set; }
            public ELocationType location_type { get; set; }
        }
        public class JumpClone
        {
            public int jump_clone_id { get; set; }
            public long location_id { get; set; }
            public ELocationType location_type { get; set; }
            public List<int> implants { get; set; }
            public string name { get; set; }
        }
        public HomeLocation home_location { get; set; }
        public List<JumpClone> jump_clones { get; set; }
        public DateTime? last_clone_jump_date { get; set; }
        public DateTime? last_station_change_date { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum ELocationType : byte
        {
            station = 1,
            structure = 2
        }
    }
}
