

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Новая война
    /// </summary>
    public class WarAddNewIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int War_Id { get; set; }
        public WarAddNewIntegrationEvent(int war_id)
        {
            War_Id = war_id;
        }
        public WarAddNewIntegrationEvent()
        {

        }
    }
}
