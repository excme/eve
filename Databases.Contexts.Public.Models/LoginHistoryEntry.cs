using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    /// <summary>
    /// Запись в истории авторизации
    /// </summary>
    public class LoginHistoryEntry
    {
        public ulong Id { get; set; }

        /// <summary>
        /// Время авторизации
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Удачность аторизации
        /// </summary>
        public bool IsSuccessed { get; set; }

        /// <summary>
        /// Ip клиента, с которого была авторизация
        /// </summary>
        public string Ip { get; set; }

        public ulong accountId { get; set; }
        public virtual Account account { get; set; }
    }
}
