using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.CompareObjects.TypeComparers
{
    /// <summary>
    /// Compare Double values with the ability to specify the precision
    /// </summary>
    public class DoubleComparer : BaseTypeComparer
    {
        /// <summary>
        /// Constructor that takes a root comparer
        /// </summary>
        /// <param name="rootComparer"></param>
        public DoubleComparer(RootComparer rootComparer) : base(rootComparer)
        {
        }

        /// <summary>
        /// Returns true if both types are double
        /// </summary>
        /// <param name="type1"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        public override bool IsTypeMatch(Type type1, Type type2)
        {
            return TypeHelper.IsDouble(type1) && TypeHelper.IsDouble(type2);
        }

        /// <summary>
        /// Compare two doubles
        /// </summary>
        /// <param name="parms"></param>
        public override bool CompareType(CompareParms parms)
        {
            //This should never happen, null check happens one level up
            if (parms.Object1 == null || parms.Object2 == null)
                return true;

            Double double1 = (Double)parms.Object1;
            Double double2 = (Double)parms.Object2;

            if (Math.Abs(double1 - double2) > parms.Config.DoublePrecision)
            {
                AddDifference(parms);
                return false;
            }

            return true;
        }

        public override void UpdateValue(CompareParms parms)
        {
            throw new NotImplementedException();
        }
    }
}
