using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.Helper
{
    public static class ListGeneric
    {
        public static List<T> ToList<T>(this IEnumerator<T> source)
        {
            List<T> to = new List<T>();
            while (source.MoveNext()){ to.Add(source.Current); }
            return to;
        }
    }
}
