using DevExtreme.AspNet.Data;

namespace eveDirect.Api.Public.Areas.Alliance.Data
{
    public class AllianceCurrentCharactersRequest
    {
        public int id { get; set; }
        public DataSourceLoadOptionsBase lo { get; set; }
    }
}
