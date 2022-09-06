using System;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class CharacterMigrationRootItem
    {
        /// <summary>
        /// record_id
        /// </summary>
        public int i { get; set; }
        /// <summary>
        /// prev_corp_id
        /// </summary>
        public int c { get; set; }
        /// <summary>
        /// character_name
        /// </summary>
        //public string n { get; set; }
        /// <summary>
        /// Character birthday
        /// </summary>
        public DateTime h { get; set; }
        /// <summary>
        /// character_id
        /// </summary>
        public int o { get; set; }
        /// <summary>
        /// is_deleted
        /// </summary>
        public bool? d { get; set; }
        /// <summary>
        /// start_date
        /// </summary>
        public DateTime s { get; set; }
        /// <summary>
        /// corporation_id
        /// </summary>
        public int a { get; set; }
        /// <summary>
        /// next_corp_id
        /// </summary>
        public int b { get; set; }
        /// <summary>
        /// end_date
        /// </summary>
        public DateTime? e { get; set; }
    }
}
