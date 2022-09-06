using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.Helper
{
    public static class ExceptionGeneric
    {
        public static string GetFullMessage(this Exception ex)
        {
            return ex.InnerException == null
                 ? ex.Message + " " + ex.StackTrace
                 : ex.Message + " " + ex.StackTrace + " --> " + ex.InnerException.GetFullMessage();
        }
    }
}
