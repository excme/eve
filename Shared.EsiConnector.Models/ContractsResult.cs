using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /contracts/public/{region_id}/
    /// </summary>
    public class ContractsResult:List<ContractsResult.Contract>, ISsoResult
    {
        public class Contract
        {
            public double buyout { get; set; }
            public double collateral { get; set; }
            public int contract_id { get; set; }
            public DateTime? date_expired { get; set; }
            public DateTime? date_issued { get; set; }
            public int days_to_complete { get; set; }
            public int duration_days { get; set; }
            public long end_location_id { get; set; }
            public bool for_corporation { get; set; }
            public int issuer_corporation_id { get; set; }
            public int issuer_id { get; set; }
            public double price { get; set; }
            public double reward { get; set; }
            public long start_location_id { get; set; }
            public string title { get; set; }
            public EType type { get; set; }
            public double volume { get; set; }
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EType : byte
        {
            unknown = 1,
            item_exchange = 2,
            auction = 3,
            courier = 4,
            loan = 5
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EStatus : byte
        {
            outstanding = 1,
            in_progress = 2,
            finished_issuer = 3,
            finished_contractor = 4,
            finished = 5,
            cancelled = 6,
            rejected = 7,
            failed = 8,
            deleted = 9,
            reversed = 10
        }
        //[JsonConverter(typeof(StringEnumConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(eveDirect.Shared.Helper.JsonStringEnumMemberConverter))]
        public enum EAvailability:byte
        {
            //[JsonProperty(PropertyName = "public")]
            [EnumMember(Value = "public")]
            _public = 1,
            personal = 2,
            corporation = 3,
            alliance = 4
        }
    }
}
