using System;
using System.Linq;

namespace eveDirect.Shared.CompareObjects.TypeComparers
{
    /// <summary>
    /// Common functionality for all Type Comparers
    /// </summary>
    public abstract class BaseTypeComparer : BaseComparer
    {
        /// <summary>
        /// A reference to the root comparer as newed up by the RootComparerFactory
        /// </summary>
        public RootComparer RootComparer { get; set; }

        /// <summary>
        /// Protected constructor that references the root comparer
        /// </summary>
        /// <param name="rootComparer"></param>
        protected BaseTypeComparer(RootComparer rootComparer)
        {
            RootComparer = rootComparer;
        }


        /// <summary>
        /// If true the type comparer will handle the comparison for the type
        /// </summary>
        /// <param name="type1">The type of the first object</param>
        /// <param name="type2">The type of the second object</param>
        /// <returns></returns>
        public abstract bool IsTypeMatch(Type type1, Type type2);

        /// <summary>
        /// Compare the two objects
        /// </summary>
        public abstract bool CompareType(CompareParms parms);

        /// <summary>
        /// Обнолвение значения Object1 на Object2
        /// </summary>
        /// <param name="parms"></param>
        public virtual void UpdateValue(CompareParms parms)
        {
            //parms.ParentObject1.GetType().GetProperty(parms.CleanPropertyName).SetValue(parms.ParentObject1, parms.Object2);
            StaticTypeComparer.UpdateValue(parms);
        }
    }
    public static class StaticTypeComparer
    {
        public static void UpdateValue(CompareParms parms)
        {
            if(parms.Object2 != null && parms.ParentObject1 != null)
                parms.ParentObject1.GetType().GetProperty(parms.CleanPropertyName).SetValue(parms.ParentObject1, parms.Object2);
        }
    }
}
