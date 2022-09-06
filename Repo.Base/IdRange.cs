using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.BaseRepo
{
    public class IdRanges : List<IdRanges.IdRangeItem> { 
        public class IdRangeItem
        {
            [JsonPropertyName("f")]
            public long from { get; set; }
            [JsonPropertyName("t")]
            public long to { get; set; }
        }
    }
}
