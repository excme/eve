

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class ContractItemChangeStatusIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Contract_id { get; set; }
        public long Item_id { get; set; }
        public ContractItemChangeStatusIntegrationEvent()
        {

        }
        public ContractItemChangeStatusIntegrationEvent(int contract_id, long item_id)
        {
            Contract_id = contract_id;
            Item_id = item_id;
        }
    }
}
