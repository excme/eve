using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /wars/{war_id}/
    /// </summary>
    public class WarInfoResult: ISsoResult
    {
        public int war_id { get; set; }
        public DateTime declared { get; set; }
        public DateTime? started { get; set; }
        public DateTime? retracted { get; set; }
        public DateTime? finished { get; set; }
        public bool mutual { get; set; }
        public bool open_for_allies { get; set; }
        public Participant aggressor { get; set; }
        public Participant defender { get; set; }
        public List<Ally> allies { get; set; } = new List<Ally>();

        public class Participant
        {
            public int ships_killed { get; set; }
            public float isk_destroyed { get; set; }
            public int? alliance_id { get; set; }
            public int? corporation_id { get; set; }
        }

        public class Ally
        {
            public int? alliance_id { get; set; }
            public int? corporation_id { get; set; }
        }
    }
}
