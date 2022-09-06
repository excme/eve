namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class ContractChangeStatusIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Contract_id { get; set;}
        public ContractChangeStatusIntegrationEvent(int contract_id)
        {
            Contract_id = contract_id;
        }
        public ContractChangeStatusIntegrationEvent()
        {

        }
    }
}
