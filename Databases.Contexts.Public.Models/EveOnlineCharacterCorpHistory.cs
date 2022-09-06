using eveDirect.Shared.EsiConnector.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    /// <summary>
    /// История корпораций персонажей
    /// </summary>
    public class EveOnlineCharacterCorpHistory : CharacterCorporationHistoryResult.CharacterCorporationHistoryItem
    {
        [Key]
        public new int record_id { get; set; }
        public int character_id { get; set; }

        //public int alliance_id { get; set; }
        public DateTime? end_date { get; set; }

        public int next_corp_id { get; set; }
        //public int next_ally_id { get; set; }

        public int prev_corp_id { get; set; }
        //public int prev_ally_id { get; set; }

        /// <summary>
        /// Есть миграции помимо новорожденной
        /// </summary>
        public bool just_newborn { get; set; }

        /// <summary>
        /// Учтено в статистике
        /// </summary>
        public bool instat { get; set; }

        /// <summary>
        /// Завершено определение альянсов в этот период
        /// </summary>
        public bool allyComplete { get; set; }

        public override string ToString()
        {
            return $"{record_id} - {corporation_id}";
        }
    }
}