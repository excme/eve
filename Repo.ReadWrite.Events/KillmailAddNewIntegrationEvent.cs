

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class KillmailAddNewIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Killmail_Id { get; set; }
        public KillmailAddNewIntegrationEvent()
        {

        }
        public KillmailAddNewIntegrationEvent(int killmail_id)
        {
            Killmail_Id = killmail_id;
        }
    }
}
