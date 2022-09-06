namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    
    /// <summary>
    /// Обновленная информация по войне
    /// </summary>
    public class WarUpdateInfoIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int War_id { get; set; }
        public WarUpdateInfoIntegrationEvent(int war_id)
        {
            War_id = war_id;
        }
        public WarUpdateInfoIntegrationEvent()
        {

        }
    }
}
