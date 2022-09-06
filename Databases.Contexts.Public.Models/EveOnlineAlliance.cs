using eveDirect.Shared.EsiConnector.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineAlliance : AllianceInfoResult
    {
        /// <summary>
        /// Ид альянса
        /// </summary>
        [Key]
        public int alliance_id { get; set; }

        public int corps_count { get; set; }
        /// <summary>
        /// Coonector.Alliance.GetAll - возвращает список АКТИВНЫХ альянсов
        /// </summary>
        public bool active { get; set; }

        /// <summary>
        /// Время последнего обновления публичной информации
        /// </summary>
        //public DateTime? last_info_updated { get; set; }
        /// <summary>
        /// Время последнего обновления списка корпораций
        /// </summary>
        // public DateTime? last_corps_list_update { get; set; }

        /// <summary>
        /// История миграций корпораций-членов
        /// </summary>
        //public List<int> membersMigrations { get; set; }

        public virtual EveOnlineAlliancePreview preview { get; set; }
    }
}
