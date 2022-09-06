using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace eveDirect.Shared.Models
{
    //public static class PropertyChangeTracker<TEntity> where TEntity : class
    //{
    //    public static IEnumerable<ChangeLog> GetChanges(object oldEntry, object newEntry, bool ignoreTypeCompare = true)
    //    {
    //        PropertyInfo[] oldProperties, newProperties = default;
    //        Type oldType, newType = default;

    //        // Разница между объектов
    //        List<ChangeLog> differences = new List<ChangeLog>();
    //        oldType = oldEntry.GetType();

    //        // Проверка на совпадение типов объектов
    //        if (!ignoreTypeCompare)
    //        {
    //            newType = newEntry.GetType();

    //            //Types don't match, cannot log changes
    //            if (oldType != newType)
    //                return differences;
    //        }

    //        // Получение свойств объектов, за вычение игнорируемых
    //        oldProperties = oldType.GetProperties().Where(x => !Attribute.IsDefined(x, typeof(CustomCompareIgnoreAttribute)) && x.PropertyType.IsPublic).ToArray();
    //        newProperties = newType.GetProperties();

    //        // Получение ключа класса
    //        var primaryKey = oldProperties
    //            .Where(x => Attribute.IsDefined(x, typeof(KeyAttribute)))
    //            .First()
    //            .GetValue(oldEntry)
    //            .ToString();
    //        // Имя класса
    //        var className = oldEntry.GetType().Name;
            
    //        // Проверка свойств по начальному объекту
    //        foreach (var oldProperty in oldProperties)
    //        {
    //            var matchingProperty = newProperties.FirstOrDefault(x => x.Name == oldProperty.Name
    //                                                            && x.PropertyType == oldProperty.PropertyType);

    //            if (matchingProperty == null)
    //                continue;

    //            var oldValue = oldProperty.GetValue(oldEntry).ToString();
    //            var newValue = matchingProperty.GetValue(newEntry).ToString();
    //            if (oldValue != newValue)
    //            {
    //                differences.Add(new ChangeLog()
    //                {
    //                    PrimaryKey = primaryKey,
    //                    ClassName = className,
    //                    PropertyName = matchingProperty.Name,
    //                    OldValue = oldProperty.GetValue(oldEntry).ToString(),
    //                    NewValue = matchingProperty.GetValue(newEntry).ToString()
    //                });
    //            }
    //        }

    //        return differences;
    //    }
    //}

    //public class ChangeLog
    //{
    //    public string ClassName { get; set; }
    //    public string PropertyName { get; set; }
    //    public string PrimaryKey { get; set; }
    //    public string OldValue { get; set; }
    //    public string NewValue { get; set; }
    //    //public DateTime DateChanged { get; set; }
    //}
    /// <summary>
    /// Аттрибут, которым помечаются свойства класса, которые необходимо игнорировать при 
    /// </summary>
    public class CustomCompareIgnoreAttribute : Attribute { }
}
