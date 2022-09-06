using eveDirect.Shared.CompareObjects;
using eveDirect.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace eveDirect.Databases.Tests.Compare
{
    public class BaseCompare
    {
        private static Random random = new Random();
        /// <summary>
        /// Базовый метод теста сравнения по properties
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TEntityRequestResult"></typeparam>
        /// <param name="entity"></param>
        /// <param name="requestResult"></param>
        /// <param name="areEqual">Сравневаемые объекты должны быть равны</param>
        /// <param name="doUpdates">Выполнять приравнивание свойств после сравнения</param>
        /// <param name="userMsg1"></param>
        /// <param name="userMsg2"></param>
        protected void MakeCompare<TEntity, TEntityRequestResult>(TEntity entity, TEntityRequestResult requestResult, bool areEqual = false, bool doUpdates = true, string userMsg1 = "", string userMsg2 = "", Dictionary<Type, IEnumerable<string>> collectionSpec = null)
            where TEntity : class, TEntityRequestResult
            where TEntityRequestResult : class
        {
            // Без обновления свойств 
            ComparisonResult comparing1 = entity.UpdateProperties(requestResult, false, collectionSpec: collectionSpec);

            if(!areEqual)
                Assert.False(comparing1.AreEqual, userMsg1);
            else
                Assert.True(comparing1.AreEqual, userMsg1);

            CompareInner(comparing1, areEqual);

            // Обновление свойств по requestResult
            if (doUpdates)
            {
                ComparisonResult comparing2 = entity.UpdateProperties(requestResult);
                CompareInner(comparing2, true);
            }

            void CompareInner(ComparisonResult _comparing, bool _areEqual)
            {
                // Проверка по различиям
                foreach (var change in _comparing.Differences)
                {
                    var val1 = typeof(TEntity).GetProperty(change.PropertyName).GetValue(entity);
                    //var val1 = change.Object1;
                    var val2 = typeof(TEntityRequestResult).GetProperty(change.PropertyName).GetValue(requestResult);
                    //var val2 = change.Object2;

                    if (_areEqual)
                        Assert.True(val1.Equals(val2), $"{change.PropertyName}: {val1} == {val2} {userMsg2}");
                    else
                        Assert.False(val1.Equals(val2), $"{change.PropertyName}: {val1} != {val2} {userMsg2}");
                }
            }
        }

        /// <summary>
        /// Выставление значений
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TEntityRequestResult"></typeparam>
        /// <param name="entity"></param>
        /// <param name="requestResult"></param>
        protected void GenerateValues<TEntity, TEntityRequestResult>(ref TEntity entity, ref TEntityRequestResult requestResult)
            where TEntity: class, TEntityRequestResult, new()
            where TEntityRequestResult: class, new()
        {
            var neededProperties = typeof(TEntityRequestResult).GetProperties();

            foreach(var property in neededProperties)
            {
                // Получение реального типа свойства. Бывает прямое получение или через Nullable
                var typeName = property.PropertyType.GenericTypeArguments.Any() ? property.PropertyType.GenericTypeArguments[0].Name : property.PropertyType.Name;

                var values = typeName switch
                {
                    "String" => new vals ("value1", "value2"),
                    "Int32" => new vals(100, 200),
                    "Int64" => new vals(long.MaxValue - 100, long.MaxValue - 200),
                    "DateTime" => new vals(new DateTime(2017,1,1), new DateTime(2018, 1, 1)),
                    "Double" => new vals(0.014000000000000002, 0.015000000000000002),

                    _ => new vals(null, null),
                };

                if (values.val1 != null)
                {
                    typeof(TEntity).GetProperty(property.Name).SetValue(entity, values.val1);
                    typeof(TEntityRequestResult).GetProperty(property.Name).SetValue(requestResult, values.val2);
                }
            }
        }

        string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public class vals
        {
            public vals(object _val1, object _val2)
            {
                val1 = _val1;
                val2 = _val2;
            }
            public object val1 { get; set; }
            public object val2 { get; set; }
        }
    }
}
