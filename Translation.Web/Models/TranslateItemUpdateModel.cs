using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Translation.Web.Models
{
    public class TranslateItemUpdateModel
    {
        public int id { get; set; }
        public string description { get; set; } 
        public string reference { get; set; }
        public string value { get; set; }
        public string ru_val { get; set; }
        public bool can_edit { get; internal set; }
    }
}
