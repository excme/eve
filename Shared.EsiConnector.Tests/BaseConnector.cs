using eveDirect.Shared.EsiConnector;
using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.EsiConnector.Models.SSO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace eveDirect.EsiConnector.Tests
{
    public class BaseConnector
    {
        public EsiClient connector{ get; set; }
        IConfigurationRoot conf { get; set; }
        public BaseConnector(bool needAuth = false)
        {
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            conf = confBuilder.Build();

            IOptions<EsiConfig> config = Options.Create(new EsiConfig() { 
                EsiUrl = "https://esi.evetech.net/",
                DataSource = DataSource.tranquility,
                ClientId = conf.GetValue<string>("Sso:clientId"),
                SecretKey = conf.GetValue<string>("Sso:secret"),
                CallbackUrl = ""
            });
            connector = new EsiClient(config);

            // Auth
            if (needAuth)
            {
                SsoToken token = connector.SSO.GetToken(GrantType.AuthorizationCode, characterRefreshToken);
                AuthorizedCharacterData auth_char = connector.SSO.Verify(token);
                connector.SetCharacterData(auth_char);
            }
        }
        
        internal int characterId { get => 95848227; }
        internal int corporationId { get => 98043813; }
        internal int allinceId { get => 99003500; }
        string characterRefreshToken { get => "GQPNUPjGYlMGgrZ38n8XJq8n7-Rud-gNG6hk9Z9-WUoPOabcFA6RMT0kS8sgNmX2a8sJwRAlm1wVSsQM3_KdqfEj9L3IfkwpxbBbyzPa1xNFO-ug9OzurirdXELagyuBRRHnEMKUuEynkQ2lnCkeI51ENnZO2NCDPXNuZ2UU9OmoMt2vCnhcbTFsOOeRtOmqfQYGUY732aeXRkLCGOE9BwXhZ5pmmkBfhGxIH8vaaA2Rw5USd3ZWVKS9FhYzdG0eXOEebfLch2TkDx3MBaY9heQtYNk5E5OinyCHdTpavXGfRR7nHbrH-bFkYYSQuztKtMe0mEOL1iOewXLl5KMkrM0pOzYqk6dGawTfyJiYWdigkBwn2_www8aHMdVbFAbD34CP5AebcSm88pe-Syma0w2"; }

        internal void ExecuteAndOutput<T>(EsiResponse<T> request) where T : class
        {
            var responseDeserialize = ExecuteAndReturn(request);

            Console.Write(JsonConvert.SerializeObject(responseDeserialize, Formatting.Indented));
        }
        internal T ExecuteAndReturn<T>(EsiResponse<T> request) where T : class
        {
            var response = request;
            var responseDeserialize = JsonConvert.DeserializeObject<T>(response.Message);

            return response.Data;
        }
    }
}
