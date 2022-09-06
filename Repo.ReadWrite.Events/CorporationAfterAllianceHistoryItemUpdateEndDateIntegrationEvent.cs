using System;

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// После Установки даты окончания участия в альянсе у корпорации
    /// </summary>
    public class CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent: Shared.EventBus.Events.IntegrationEvent
    {
        public int Record_id { get; set; }
        public int Corporation_id { get; set; }
        public DateTime? OnDate { get; set; }
        public bool New_Created { get; set; }
        public CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent()
        {

        }
        public CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent(int record_id, int corporation_id, DateTime? onDate, bool new_Created = false)
        {
            Record_id = record_id;
            Corporation_id = corporation_id;
            OnDate = onDate;
            New_Created = new_Created;
        }
    }
}
