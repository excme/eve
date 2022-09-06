using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.Models
{
    public class ColumnTypes
    {
        /// <summary>
        /// для DateTime только дата
        /// </summary>
        public string onlyDate { get; set; }
    }

    /// <summary>
    /// Отправка названий специфичных columnTypes в зависимости от движка субд
    /// </summary>
    public static class DatabaseColumnTypesSpec
    {
        public static ColumnTypes Get(string databaseProviderName)
        {
            return databaseProviderName switch
            {
                // https://www.tutorialspoint.com/postgresql/postgresql_data_types.htm
                // https://www.npgsql.org/doc/types/basic.html
                "Npgsql.EntityFrameworkCore.PostgreSQL" => new ColumnTypes() { onlyDate = "date" },

                _ => throw new ArgumentException(databaseProviderName),
            };
        }
    }
}
