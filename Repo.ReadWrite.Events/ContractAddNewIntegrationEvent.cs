

using System;

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class ContractAddNewIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Contract_id { get; set; }

        // Desc
        public double Volume { get; set; }
        public byte Type { get; set; }
        public int Region_Id { get; set; }
        public DateTime? OnDate { get; set; }

        public ContractAddNewIntegrationEvent(int contract_id, int region_id, byte type, double volume, DateTime? onDate)
        {
            Contract_id = contract_id;
            Volume = volume;
            Type = type;
            Region_Id = region_id;
            OnDate = onDate;
        }
        public ContractAddNewIntegrationEvent() { }
    }
}
