using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Translation.Web.Models
{
    public class TransationItemModel
    {
        public int id { get; set; }

        public string key { get; set; }
        public string reference { get; set; }
        public string description { get; set; }

        public string ru_value { get; set; }
        public bool ru_approval { get; set; }
        public string en_value { get; set; }
        public bool en_approval { get; set; }
        public string ge_value { get; set; }
        public bool ge_approval { get; set; }
        public string fr_value { get; set; }
        public bool fr_approval { get; set; }
        public string ja_value { get; set; }
        public bool ja_approval { get; set; }
        public string ko_value { get; set; }
        public bool ko_approval { get; set; }
        public string zh_value { get; set; }
        public bool zh_approval { get; set; }
    }
}
