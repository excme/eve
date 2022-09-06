using eveDirect.Shared.EventBus.Events;

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Новые killmails в войне
    /// </summary>
    public class WarKillmailsUpdatedIntegrationEvent: IntegrationEvent
    {
        public int War_id { get; set; }
        public WarKillmailsUpdatedIntegrationEvent(int war_id)
        {
            War_id = war_id;
        }
        public WarKillmailsUpdatedIntegrationEvent()
        {

        }
    }
}
