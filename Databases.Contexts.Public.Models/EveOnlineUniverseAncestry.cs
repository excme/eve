using eveDirect.Shared.EsiConnector.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineUniverseAncestry : UniverseAncestriesResult.UniverseAncestriesItem
    {
        [Key]
        public new int id { get; set; }

        // Names
        public string dename { get; set; }
        public string enname { get; set; }
        public string frname { get; set; }
        public string janame { get; set; }
        public string runame { get; set; }
        public string zhname { get; set; }
        public string koname { get; set; }

        // Desc
        public string dedescription { get; set; }
        public string endescription { get; set; }
        public string frdescription { get; set; }
        public string jadescription { get; set; }
        public string rudescription { get; set; }
        public string zhdescription { get; set; }
        public string kodescription { get; set; }

        // Short Desc
        public string deshort_description { get; set; }
        public string enshort_description { get; set; }
        public string frshort_description { get; set; }
        public string jashort_description { get; set; }
        public string rushort_description { get; set; }
        public string zhshort_description { get; set; }
        public string koshort_description { get; set; }

    }
}
