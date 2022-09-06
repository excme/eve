using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/
    /// </summary>
    public class CorporationInfoResult : ISsoResult
    {
        public string name { get; set; }
        public string ticker { get; set; }
        public int member_count { get; set; }
        public int ceo_id { get; set; }
        public float tax_rate { get; set; }
        public int creator_id { get; set; }
        public int alliance_id { get; set; }
        public string description { get; set; }
        public DateTime date_founded { get; set; }
        public string url { get; set; }
        public int home_station_id { get; set; }
        public long shares { get; set; }
        public int faction_id { get; set; }
        public bool war_eligible { get; set; }
    }
}
