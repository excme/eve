using eveDirect.Shared.EsiConnector.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineCorporationAllianceHistory : CorporationAllianceHistoryResult.CorporationAllianceHistoryItem
    {
        [Key]
        public new int record_id { get; set; }
        public int corporation_id { get; set; }

        public int next_ally_id { get; set; }
        public DateTime? end_date { get; set; }

        public int prev_ally_id { get; set; }

        /// <summary>
        /// Учтено в статистике
        /// </summary>
        public bool instat { get; set; }

        public override string ToString()
        {
            return $"{record_id} - {alliance_id}";
        }
    }

    public class EveOnlineCorporationMemberAllianceHistory
    {
        public int id { get; set; }
        public int record_id { get; set; }
        public int character_id { get; set; }
    }
}
