using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using eveDirect.Shared.EsiConnector.Models;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineCharacter : CharacterInfoResult
    {
        [Key]
        public int character_id { get; set; }
        //public bool npc { get; set; }

        /// <summary>
        /// Последняя проверка публичной информации
        /// </summary>
        //public DateTime last_update_publicInfo { get; set; }

        /// <summary>
        /// Последняя проверка отношения к объединениям
        /// </summary>
        //public DateTime last_update_affiliation { get; set; }

        /// <summary>
        /// Последнее обновление корпораций
        /// </summary>
        //public DateTime? lastUpdate_corpHistory { get; set; }

        /// <summary>
        /// История корпораций
        /// </summary>
        //public virtual CharacterCorporationHistoryResult corpHistory { get; set; }

        public virtual ICollection<int> killmails { get; set; }

        public virtual EveOnlineCharacterPreview preview { get; set; }
        /// <summary>
        /// Количество записей истории корпораций
        /// </summary>
        public int corpHistoryCount { get; set; }

        /// <summary>
        /// Последнее обновление историю корпораций
        /// </summary>
        public DateTime? lastupdate_corphistory { get; set; }

        /// <summary>
        /// Менял корпорацию после рождения
        /// </summary>
        //public bool anyCorpMigrations { get; set; }
    }
}
