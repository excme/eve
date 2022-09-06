using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class AlliancePreview
    {
        public string ticker { get; set; }
        public int faction_id { get; set; }
        public int executor_corporation_id { get; set; }
        public DateTime date_founded { get; set; }
        public int creator_id { get; set; }
        public int creator_corporation_id { get; set; }
        public int corps_count { get; set; }
    }
}
