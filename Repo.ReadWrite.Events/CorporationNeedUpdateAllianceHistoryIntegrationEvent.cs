namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CorporationNeedUpdateAllianceHistoryIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Corporation_id { get; set; }
        public CorporationNeedUpdateAllianceHistoryIntegrationEvent(int corporation_id)
        {
            Corporation_id = corporation_id;
        }
        public CorporationNeedUpdateAllianceHistoryIntegrationEvent()
        {

        }
    }
}
