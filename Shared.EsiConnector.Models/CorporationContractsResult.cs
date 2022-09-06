using System;
using System.Collections.Generic;
using System.Text;
using static eveDirect.Shared.EsiConnector.Models.ContractsResult;
using static eveDirect.Shared.EsiConnector.Models.CorporationContractsResult;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/contracts/
    /// </summary>
    public class CorporationContractsResult:List<CorporationContractItem>, ISsoResult
    {
        public string Args(long owner_id = 0, params string[] args)
        {
            throw new NotImplementedException();
        }

        public int PeriodExpire()
        {
            throw new NotImplementedException();
        }

        public class CorporationContractItem : CharacterContractsResult.CharacterContractItem
        {
            
        }
    }
}
