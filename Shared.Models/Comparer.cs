using eveDirect.Shared.CompareObjects;
using eveDirect.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eveDirect.Shared
{
    public static class Comparer
    {
        static CompareLogic compareLogic { get; set; }
        static ComparisonConfig defaultComparisonConfig { get; set; }
        static Comparer()
        {
            defaultComparisonConfig = new ComparisonConfig()
            {
                CompareProperties = true,
                MakeUpdatesValue = true,
                CompareFields = false,
                IgnoreObjectTypes = true,
                IgnoreCollectionOrder = true,
                MaxMillisecondsDateDifference = 60 * 1000,
                AttributesToIgnore = new List<Type>() { /*typeof(NotMappedAttribute),*/ typeof(CustomCompareIgnoreAttribute) },
                MaxStructDepth = 5,
                MaxDifferences = int.MaxValue,
                DoublePrecision = .0001
            };
            
            compareLogic = new CompareLogic();
            compareLogic.Config = defaultComparisonConfig;
        }
        public static ComparisonResult UpdateProperties<TEntity, TEntityBase>(
            this TEntity entity, 
            TEntityBase connectionResult, 
            bool makeUpdates = true, 
            CompareLogic _compareLogic = null, 
            Dictionary<Type, IEnumerable<string>> collectionSpec = null)
            where TEntityBase : class
            where TEntity : TEntityBase
        {
            // Загрузка настроек или по-умолчанию
            if (_compareLogic != null)
                compareLogic = _compareLogic;

            if (collectionSpec?.Any() ?? false)
                compareLogic.Config.CollectionMatchingSpec = collectionSpec;

            // Выполнения сравнения
            // *** В nullable свойствах тип Object1TypeName и Object2TypeName будет зависить от значения. Если value is null, то тип тоже будет null
            var compareResult = compareLogic.Compare(entity, connectionResult);
            if (makeUpdates && !compareResult.AreEqual)
            {
                foreach (var change in compareResult.Differences)
                {
                    try
                    {
                        typeof(TEntity)
                            .GetProperty(change.PropertyName)
                            .SetValue(entity, change.Object2);
                    }
                    catch
                    {

                    }
                }
            }

            // Возрат default настроек
            if (_compareLogic != null || (collectionSpec?.Any() ?? false))
                compareLogic.Config = defaultComparisonConfig;

            return compareResult;
        }
        public static ComparisonResult UpdateProperties_NonBased<TEntity, TEntityBase>(
            this TEntity entity,
            TEntityBase connectionResult,
            bool makeUpdates = true,
            CompareLogic _compareLogic = null,
            Dictionary<Type, IEnumerable<string>> collectionSpec = null)
            where TEntityBase : class
            where TEntity : class
        {
            // Загрузка настроек или по-умолчанию
            if (_compareLogic != null)
                compareLogic = _compareLogic;

            if (collectionSpec?.Any() ?? false)
                compareLogic.Config.CollectionMatchingSpec = collectionSpec;

            compareLogic.Config.MakeUpdatesValue = makeUpdates;

            // Выполнения сравнения
            // *** В nullable свойствах тип Object1TypeName и Object2TypeName будет зависить от значения. Если value is null, то тип тоже будет null
            var compareResult = compareLogic.Compare(entity, connectionResult);
            if (makeUpdates && !compareResult.AreEqual)
            {
                foreach (var change in compareResult.Differences)
                {
                    typeof(TEntity).GetProperty(change.PropertyName).SetValue(entity, change.Object2);
                }
            }

            // Возрат default настроек
            if (_compareLogic != null || (collectionSpec?.Any() ?? false))
                compareLogic.Config = defaultComparisonConfig;

            return compareResult;
        }
    }
}
