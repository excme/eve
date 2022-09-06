using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class MarketOrderModel
    {
        public long i { get; set; }
        public long li { get; set; }
        public DateTime iss { get; set; }
        public double p { get; set; }
        public int vr { get; set; }
        public int vt { get; set; }
        public int vm { get; set; }
        public int d { get; set; }
    }
}
