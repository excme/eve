using eveDirect.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using eveDirect.Databases.Contexts;
using eveDirect.Databases.Contexts.Public.Models;

namespace eveDirect.Repo.ReadWrite
{
    public partial class ReadWriteRepo : IReadWrite
    {
        /// <summary>
        /// Выбор всех значений из колонки таблицы
        /// </summary>
        /// <returns></returns>
        public List<T2> Db_SelectColumn<T1, T2>(Expression<Func<T1, T2>> select, Expression<Func<T1, bool>> where = null, bool distinct = false)
            where T1 : class
        {
            using var _context = new PublicContext(_options);
            var source = _context.Set<T1>().AsNoTracking().AsQueryable();

            if (where != null)
                source = source.Where(where);

            if (distinct)
                source = source.Distinct();

            return source.Select(select).ToList();
        }

        /// <summary>
        /// Получение макс или мин зн-я из столбца таблицы БД
        /// </summary>
        public T2 Db_SelectColumn_MaxMinValue<T1, T2>(
            Expression<Func<T1, T2>> max = null, 
            Expression<Func<T1, T2>> min = null, 
            Expression<Func<T1, bool>> where = null) 
            where T1 : class
        {
            if (max == null && min == null)
                throw new NullReferenceException($"{nameof(max)} && {nameof(min)} == null");

            using var _context = new PublicContext(_options);
            var source = _context.Set<T1>().AsNoTracking().AsQueryable();

            if (where != null)
                source = source.Where(where);

            T2 task = default;
            if (max != null)
                task = source.Max(max);
            else
                task = source.Min(min);

            return task;
        }

        /// <summary>
        /// Получение ячейки по условию
        /// </summary>
        public T1 Db_SelectRow<T1>(Expression<Func<T1, bool>> where)
            where T1 : class
        {
            using var _context = new PublicContext(_options);
            var source = _context.Set<T1>().AsNoTracking().AsQueryable();

            return source.FirstOrDefault(where);
        }

        public int Db_CountRow<T1>(Expression<Func<T1, bool>> where)
            where T1 : class
        {
            using var _context = new PublicContext(_options);
            return _context.Set<T1>().Count();
        }

        public void CheckPoint_Upsert(string name, int value = 0)
        {
            using var _context = new PublicContext(_options);

            _context.EveDirectCheckPoints.Upsert(new EveDirectCheckPoint()
            {
                checkpointName = name,
                value = value
            })
            .On(v => v.checkpointName)
            .WhenMatched(v => new EveDirectCheckPoint
            {
                value = value,
            })
            .Run();
        }
    }
}
