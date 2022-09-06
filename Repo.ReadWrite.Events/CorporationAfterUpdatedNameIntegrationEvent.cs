namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CorporationAfterUpdatedNameIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Corporation_id { get; set; }
        /// <summary>
        /// Только созданная корпорация
        /// </summary>
        public bool New_created { get; set; }
        public CorporationAfterUpdatedNameIntegrationEvent(int corporation_id, bool new_created=false)
        {
            Corporation_id = corporation_id;
            New_created = new_created;
        }
        public CorporationAfterUpdatedNameIntegrationEvent()
        {

        }
    }
}
