using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace eveDirect.Shared.Helper
{
    public static class StringGeneric
    {
        static public string UpperCaseFirstCharacter(this string text)
        {
            return Regex.Replace(text, "^[a-z]", m => m.Value.ToUpper());
        }
    }
}
