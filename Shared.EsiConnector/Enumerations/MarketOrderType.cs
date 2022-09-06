//using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace eveDirect.Shared.EsiConnector.Enumerations
{
    //[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum MarketOrderType
    {
        [EnumMember(Value="all")]  /**/ All,
        [EnumMember(Value="buy")]  /**/ Buy,
        [EnumMember(Value="sell")] /**/ Sell
    }
}
