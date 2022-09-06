using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class IdentitySso
    {
        public IdentitySso() { }
        public IdentitySso(string accesstoken, string tokentype, string refreshtoken, int tokenexpiresin)
        {
            access_token = accesstoken;
            refresh_token = refreshtoken;
            //TokenExpires = DateTime.UtcNow.AddSeconds(tokenexpiresin - 10);
        }

        [Key]
        ///[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong id { get; set; }

        /// <summary>
        /// Ид запроса при добавлении
        /// </summary>
        public string pipe_request { get; set; }

        public ESsoStatus status { get; set; }
        /// <summary>
        /// Когда добавлено
        /// </summary>
        public DateTime added { get; set; }

        /// Данные токена
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public DateTime expire { get; set; }
        public bool Expired()
        {
            return DateTime.UtcNow < expire.AddSeconds(10);
        }
        /// <summary>
        /// Привелегии доступа
        /// </summary>
        public List<string> token_scopes { get; set; }
        /// <summary>
        /// Привелегии доступа. Сериализация
        /// </summary>
        //public string TokenScopesStr { get; set; }
        public string character_owner_hash { get; set; }
        /// <summary>
        /// Последняя проверка владельца и статус токена
        /// </summary>
        public DateTime last_owner_and_status_update { get; set; }

        #region Ref
        public ulong accountId { get; set; }
        public virtual Account account { get; set; }

        /// <summary>
        /// Персонаж Eve Online
        /// </summary>
        public int character_id { get; set; }
        public string character_name { get; set; }
        public int corporation_id { get; set; }
        public string corporation_name { get; set; }
        public int alliance_id { get; set; }
        public bool is_ceo { get; set; }

        #endregion
    }

    public enum ESsoStatus
    {
        [Display(Name = "Инициализируется")]
        Initialized = 0,
        [Display(Name = "Потеряно владение")]
        LosedOwner = 1,
        [Display(Name = "Активирован")]
        Active = 2,
        [Display(Name = "Заблокирован")]
        Blocked = 3,
        [Display(Name = "Удален пользователем")]
        ManualRemoved = 4,
        [Display(Name = "Приостанолен, добавлением нового")]
        Paused = 5
    }
}
