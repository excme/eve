using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace eveDirect.Clients.Web.Infrastructure
{
    public class API
    {
        public static class CommonPrivate
        {
            public static string baseUri(IConfiguration configuration, IWebHostEnvironment environment)
            {
                return "http://" + (environment.IsDevelopment() ? "localhost:5004" : $"{configuration["Addresses:ApiCommonPrivate"]}");
            }
            public static string GetCharacterIdRanges(string baseUri) => $"{baseUri}/chars/ir";
            public static string GetCorporationIdRanges(string baseUri) => $"{baseUri}/corps/ir";
            public static string GetAllianceIdRanges(string baseUri) => $"{baseUri}/allies/ir";
            public static string GetContractIdRanges(string baseUri) => $"{baseUri}/contracts/ir";
            public static string GetOrderIdRanges(string baseUri) => $"{baseUri}/orders/ir";
            //public static string ExportTranslate(string baseUri) => baseUri+"/export/{0}.json";
        }
    }
}
