

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CharacterAfterUpdatedAllianceIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Alliance_id { get; set; }
        public int Character_id { get; set; }
        public CharacterAfterUpdatedAllianceIntegrationEvent(int character_id, int alliance_id)
        {
            Character_id = character_id;
            Alliance_id = alliance_id;
        }
        public CharacterAfterUpdatedAllianceIntegrationEvent()
        {

        }
    }
}
