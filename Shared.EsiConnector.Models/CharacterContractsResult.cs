using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static eveDirect.Shared.EsiConnector.Models.CharacterContractsResult;
using static eveDirect.Shared.EsiConnector.Models.ContractsResult;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/contracts/
    /// </summary>
    public class CharacterContractsResult:List<CharacterContractItem>, ISsoResult
    {
        public class CharacterContractItem: Contract
        {
            public int acceptor_id { get; set; }
            public int assignee_id { get; set; }
            public EAvailability availability { get; set; }
            public DateTime? date_accepted { get; set; }
            public DateTime? date_completed { get; set; }
            public EStatus status { get; set; }
        }
    }
}
