using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public  class IdentityConnectionStringToDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong Id { get; set; }

        /// <summary>
        /// Подключение к БД
        /// </summary>
        public string ConnectionStr { get; set; }

        /// <summary>
        /// Ид корпорации или персонажа
        /// </summary>
        public int owner_id { get; set; }

        public EOwnerType owner_type { get; set; }

        public virtual Account Account { get; set; }
        public ulong AccountId { get; set; }

        public DateTime Updated { get; set; }
        public EConnectionStringStatus Status { get; set; }
    }
    public enum EOwnerType : byte
    {
        character = 1,
        corporation = 2,
        alliance = 3
    }
    public enum EConnectionStringStatus : byte
    {
        disabled = 0,
        actual = 1
    }
}
