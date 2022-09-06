using eveDirect.Shared.EsiConnector.Models;
using System.ComponentModel.DataAnnotations;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineContractBid : ContractsBidsResult.ContractsBidsItem
    {
        [Key]
        public new int bid_id { get; set; }
        public int contract_id { get; set; }
        public virtual EveOnlineContract contract { get; set; }
        public bool isDisable { get; set; }
        public EveOnlineContractBid()
        {

        }
        public EveOnlineContractBid(ContractsBidsResult.ContractsBidsItem data)
        {
            bid_id = data.bid_id;
            date_bid = data.date_bid;
            amount = data.amount;
        }
    }
}