using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector
{
    public static class ERequestCachePeriod
    {
        public const int HOUR1 = 3600;
        public const int SECOND5 = 5;
        public const int SECOND30 = 30;
        public const int MINUTES10 = 600;
        public const int DAY1 = 86400;
        public const int MINUTES5 = 300;
        public const int MINUTES2 = 120;
        public const int MINUTES1 = 60;
        public const int MINUTES30 = 1800;
        public const int MINUTES20 = 1200;
        public const int DAYS14 = 1209600;
        public const int HOURS12 = 43200;
        public static readonly TimeSpan DailyTimeExpire = TimeSpan.FromHours(11).Add(TimeSpan.FromMinutes(5));
    }
}
