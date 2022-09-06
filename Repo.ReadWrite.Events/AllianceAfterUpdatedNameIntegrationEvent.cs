namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Событие. Альянс после обновления названия
    /// </summary>
    public class AllianceAfterUpdatedNameIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Alliance_id { get; set; }
        /// <summary>
        /// Только созданная корпорация
        /// </summary>
        public bool New_Created { get; set; }
        public AllianceAfterUpdatedNameIntegrationEvent(int alliance_id, bool new_created = false)
        {
            Alliance_id = alliance_id;
            New_Created = new_created;
        }
        public AllianceAfterUpdatedNameIntegrationEvent()
        {

        }
    }
}
