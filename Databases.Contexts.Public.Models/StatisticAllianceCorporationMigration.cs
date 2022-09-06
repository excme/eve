using System;

namespace eveDirect.Databases.Contexts.Public.Models
{
    /// <summary>
    /// Запись ежедневной статистики миграции корпораций в альянсах
    /// </summary>
    public class StatisticAllianceCorporationMigration
    {
        public int id { get; set; }
        public int alliance_id { get; set; }

        public DateTime date { get; set; }

        public int count { get; set; }
        public int _in { get; set; }
        public int _out { get; set; }
    }
}
