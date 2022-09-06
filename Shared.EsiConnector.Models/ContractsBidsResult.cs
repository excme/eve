using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// Get /contracts/public/bids/{contract_id}/
    /// </summary>
    public class ContractsBidsResult:List<ContractsBidsResult.ContractsBidsItem>,  ISsoResult
    {
        public class ContractsBidsItem
        {
            public int bid_id { get; set; }
            public DateTime? date_bid { get; set; }
            public float amount { get; set; }
        }
    }
    /// <summary>
    /// GET /characters/{character_id}/contracts/{contract_id}/bids/
    /// GET /corporations/{corporation_id}/contracts/{contract_id}/bids/
    /// </summary>
    public class CharCorpContractsBidsResult : List<CharCorpContractsBidsResult.ContractsBidsItem>, ISsoResult
    {
        public class ContractsBidsItem : ContractsBidsResult.ContractsBidsItem
        {
            public int bidder_id { get; set; }
        }
    }
}
