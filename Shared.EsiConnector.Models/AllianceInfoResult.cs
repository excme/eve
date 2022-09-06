using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /alliances/{alliance_id}/
    /// </summary>
    public class AllianceInfoResult:ISsoResult
    {
        public string name { get; set; }
        public string ticker { get; set; }
        public int creator_id { get; set; }
        public int creator_corporation_id { get; set; }
        public int executor_corporation_id { get; set; }
        public DateTime date_founded { get; set; }
        public int faction_id { get; set; }
    }
}
