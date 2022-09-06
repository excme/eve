using System;

namespace eveDirect.Repo.ReadWrite
{
    public class Cached_Corporation
    {
        //public Guid corporation_guid { get; set; }
        public int corporation_id { get; set; }
        public string corporation_name { get; set; }
        public Guid sso_id { get; set; }
        /// <summary>
        /// Guid аккаунта руководителя
        /// </summary>
        public Guid owner_sso_id { get; set; }
    }
}
