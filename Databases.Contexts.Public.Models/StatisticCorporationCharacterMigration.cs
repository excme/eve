using System;

namespace eveDirect.Databases.Contexts.Public.Models
{
    /// <summary>
    /// Запись ежедневной статистики миграции персонажей в корпорации
    /// </summary>
    public class StatisticCorporationCharacterMigration
    {
        public int id { get; set; }

        public int corporation_id { get; set; }
       
        public DateTime date { get; set; }

        public int count { get; set; }
        public int _in { get; set; }
        public int _out { get; set; }
    }
}
