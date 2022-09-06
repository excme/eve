using System;
using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    public class CharacterCorporationHistoryResult : List<CharacterCorporationHistoryResult.CharacterCorporationHistoryItem>, ISsoResult
    {
        public class CharacterCorporationHistoryItem
        {
            public DateTime start_date { get; set; }
            public int corporation_id { get; set; }
            public bool? is_deleted { get; set; }
            public int record_id { get; set; }

            public override string ToString()
            {
                return $"{record_id}|{corporation_id}|{start_date}";
            }
        }
    }
}
