using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.Helper
{
    public static class DoubleGeneric
    {
        public static double GetValue(this double? value)
        {
            return value.HasValue ? value.Value : 0;
        }

        public static string ToTSql(this double value)
        {
            return value.ToString().Replace(",", ".");
        }
    }
}
