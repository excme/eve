
using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class ContractItemAddNewIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Contract_id { get; set; }
        public long Item_id { get; set; }
        public ContractItemAddNewIntegrationEvent()
        {

        }
        public ContractItemAddNewIntegrationEvent(int contract_id, long item_id)
        {
            Contract_id = contract_id;
            Item_id = item_id;
        }
    }
}
