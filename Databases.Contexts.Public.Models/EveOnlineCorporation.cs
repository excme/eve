using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineCorporation : CorporationInfoResult
    {
        [Key]
        public int corporation_id { get; set; }

        /// <summary>
        /// Является ли корпорация ncp
        /// </summary>
        public bool ncp { get; set; }

        /// <summary>
        /// Дата кэша публичной информации
        /// </summary>
        //public DateTime? last_update_publicInfo { get; set; }

        /// <summary>
        /// Последняя проверка истории альянсов
        /// </summary>
        public DateTime? lastUpdate_allianceHistory { get; set; }
        
        /// <summary>
        /// История альянсов
        /// </summary>
        //public CorporationAllianceHistoryResult allianceHistory { get; set; }

        /// <summary>
        /// История миграций персонажей-членов
        /// </summary>
        public List<int> membersMigrations { get; set; }

        public virtual EveOnlineCorporationPreview preview { get; set; }
    }
}
