namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class AllianceAfterAddIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Alliance_id { get; set; }
        public bool New_Created { get; set; }
        public AllianceAfterAddIntegrationEvent(int alliance_id, bool new_Created)
        {
            Alliance_id = alliance_id;
            New_Created = new_Created;
        }
        public AllianceAfterAddIntegrationEvent()
        {

        }
    }
}
