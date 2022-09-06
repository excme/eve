using eveDirect.Shared.EsiConnector.Enumerations;
using System;

namespace eveDirect.Shared.EsiConnector
{
    public class EsiConfig
    {
        public string EsiUrl { get; set; }
        public DataSource DataSource { get; set; }
        public string ClientId { get; set; }
        public string SecretKey { get; set; }
        public string CallbackUrl { get; set; }
        //public string UserAgent => "eve.zone";
        public string UserAgent => Guid.NewGuid().ToString();

        public string ProxyAddr { get; set; }
        public string ProxyPort { get; set; }
        public string ProxyUser { get; set; }
        public string ProxyPass { get; set; }
    }
}
