using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /insurance/prices/
    /// </summary>
    public class InsurancePricesResult : List<InsurancePricesResult.InsurancePricesItem>, ISsoResult
    {
        public class Level
        {
            public float cost { get; set; }
            public float payout { get; set; }
            public string name { get; set; }
        }

        public class InsurancePricesItem
        {
            public int type_id { get; set; }
            public List<Level> levels { get; set; } = new List<Level>();
        }
    }
}
