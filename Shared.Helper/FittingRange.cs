using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eveDirect.Shared.Helper
{
    public static class FittingRange
    {
        public static bool ForKillmail(int flag)
        {
            return killmailsFitting.Contains(flag);
        }
        static List<int> killmailsFitting { get; set; }
        static FittingRange()
        {
            killmailsFitting = new List<int>();
            // Highs
            killmailsFitting.AddRange(Enumerable.Range(27, 8));
            // Mids
            killmailsFitting.AddRange(Enumerable.Range(19, 8));
            // Lows
            killmailsFitting.AddRange(Enumerable.Range(11, 8));
            // Drones
            killmailsFitting.AddRange(Enumerable.Range(87, 1));
            // Rigs
            killmailsFitting.AddRange(Enumerable.Range(92, 6));
            // Subs
            killmailsFitting.AddRange(Enumerable.Range(125, 7));
        }
    }
}
