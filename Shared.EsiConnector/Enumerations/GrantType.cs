//using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace eveDirect.Shared.EsiConnector.Enumerations
{
    public enum GrantType
    {
        [EnumMember(Value="authorization_code")] /**/ AuthorizationCode,
        [EnumMember(Value="refresh_token")]      /**/ RefreshToken
    }
}
