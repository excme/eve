namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CorporationAfterAddIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Corporation_id { get; set; }
        /// <summary>
        /// Новгосозданная корпорация, найденная через SearchByIdJob
        /// </summary>
        public bool New_created { get; set; }
        public CorporationAfterAddIntegrationEvent(int corporation_id, bool new_created = false)
        {
            Corporation_id = corporation_id;
            New_created = new_created;
        }
        public CorporationAfterAddIntegrationEvent()
        {

        }
    }
}
