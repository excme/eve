using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace eveDirect.Shared.Helper
{
    public static class FormatIskGeneric
    {
        private static string[] formatIskIndexes = new string[] {"", "k", "m", "b", "t", "tt", "ttt" };

        public static string FormatIsk(this double value)
        {
            return FormatIsk(Convert.ToInt64(value));
        }

        public static string FormatIsk(this long value)
        {
            if(value == 0)
            {
                return "0.00";
            }
            if (value < 10000) {
                return value.ToString("n");
            }
            int iskIndex = 0;
            while (value > 999.99) {
            value /= 1000;
                ++iskIndex;
            }

            var f = new NumberFormatInfo { NumberGroupSeparator = " " };
            var v = value.ToString("n", f) + formatIskIndexes[iskIndex];
            return value >= 0 ? v : "-" + v ;
        }

        public static string FormatIskWithPlus(this long value)
        {
            var v = FormatIsk(value);
            return v[0] != '-' ? "+" + v : v;
        }
    }
}
