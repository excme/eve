using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Repo.ReadWrite
{
    public class Cached_Character
    {
        //public Guid character_guid { get; set; }
        public int character_id { get; set; }
        public string character_name { get; set; }
        public Guid sso_id { get; set; }
        public int character_corporation_id { get; set; }
        public Guid owner_account_Guid { get; set; }
    }
}
