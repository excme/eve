using System.Collections.Generic;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/divisions/
    /// </summary>
    public class CorporationDivisionsResult:ISsoResult
    {
        public class Hangar
        {
            public int division { get; set; }
            public string name { get; set; }
        }
        public class Wallet
        {
            public int division { get; set; }
            public string name { get; set; }
        }
        public List<Hangar> hangar { get; set; }
        public List<Wallet> wallet { get; set; }
        
    }
}
