

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class ContractBidChangeStatusIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Contract_id { get; set; }
        public int Bid_id { get; set; }
        public ContractBidChangeStatusIntegrationEvent()
        {

        }
        public ContractBidChangeStatusIntegrationEvent(int contract_id, int bid_id)
        {
            Contract_id = contract_id;
            Bid_id = bid_id;
        }
    }
}
