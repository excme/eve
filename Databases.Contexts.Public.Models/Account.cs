using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    /// <summary>
    /// Аккаунт пользователя ресурса
    /// </summary>
    public class Account : IdentityUser<ulong>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override ulong Id { get; set; }

        /// <summary>
        /// Статус аккаунта
        /// </summary>
        public EAccountStatus status { get; set; }

        /// <summary>
        /// Время создания аккаунта
        /// </summary>
        public DateTime сreated { get; set; }

        /// <summary>
        /// История авторизаций
        /// </summary>
        public virtual List<LoginHistoryEntry> loginhist { get; set; }
    }
    public enum EAccountStatus : byte
    {
        /// <summary>
        /// Создан
        /// </summary>
        Created = 1,
        /// <summary>
        /// Активирован
        /// </summary>
        Activated = 3,
        /// <summary>
        /// Заблокирован
        /// </summary>
        Blocked = 4
    }
}
