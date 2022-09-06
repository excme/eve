using System;
using System.Collections.Generic;
using System.Text;
//using Newtonsoft.Json;

namespace eveDirect.Shared.Helper
{
    public static class TextGeneric
    {
        //public static string JsonFormat(this string json)
        //{
        //    var parsedJson = JsonConvert.DeserializeObject(json);
        //    return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        //}

        public static int ToInt32(this string text)
        {
            return Convert.ToInt32(text);
        }

        public static int ToInt32(this object text)
        {
            return Convert.ToInt32(text);
        }

        public static long ToInt64(this int num)
        {
            return Convert.ToInt64(num);
        }

        public static long ToInt64(this object num)
        {
            return Convert.ToInt64(num);
        }

        //public static ulong ToInt64(this int num)
        //{
        //    return Convert.ToUInt64(num);
        //}

        //public static ulong ToInt64(this long num)
        //{
        //    return Convert.ToUInt64(num);
        //}

        public static long ToInt64(this string text)
        {
            return Convert.ToInt64(text);
        }

        public static DateTime ToDateTime(this object val)
        {
            return Convert.ToDateTime(val);
        }

        public static double ToDouble(this object val)
        {
            return Convert.ToDouble(val);
        }

        public static bool ToBoolean(this object val)
        {
            return Convert.ToBoolean(val);
        }

        public static string GetTokenMask(this string text)
        {
            if(text != null)
                return text.Substring(0, 5) + "-*****-*****-*****";

            return "";
        }
    }
}
