using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eveDirect.Repo.PublicReadOnly.Models
{
    public class ContractModel
    {
        public int i { get; set; }
        public double p { get; set; }
        public double v { get; set; }
        public int t { get; set; }
        public string a { get; set; }
        public long l { get; set; }
        public double b { get; set; }
        public double r { get; set; }
        public double c { get; set; }
        public int k { get; set; }
        public DateTime? s { get; set; }
        public int d { get; set; }
    }
    public class ContractData : ContractModel
    {
        public IEnumerable<int> types { get; set; }
        public int region_id { get; set; }
    }
}
