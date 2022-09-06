using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.Helper
{
    public static class TimeExtensions
    {
        public static TimeSpan StripMilliseconds(this TimeSpan time)
        {
            return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
        }
    }
}
