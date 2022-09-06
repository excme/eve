using System;

namespace eveDirect.Databases.Contexts.Public.Models
{
    /// <summary>
    /// Период членства персонажа в альянсе
    /// </summary>
    public class EveDirectCharacterAllianceHistory : EveDirectCharacterAllianceHistoryData
    {
        public int id { get; set; }
    }

    public class EveDirectCharacterAllianceHistoryData
    {
        public int character_id { get; set; }
        public int alliance_id { get; set; }
        public int corporation_id { get; set; }

        /// <summary>
        /// Record_Id corporation allyHistory
        /// </summary>
        public int allyHistory_recordId { get; set; }
        /// <summary>
        /// Record_Id character corpHistory
        /// </summary>
        public int corpHistory_recordId { get; set; }

        public DateTime start { get; set; }
        public DateTime? end { get; set; }
    }
}
