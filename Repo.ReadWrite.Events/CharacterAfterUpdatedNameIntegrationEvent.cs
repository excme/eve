namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CharacterAfterUpdatedNameIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Character_id { get; set; }
        public CharacterAfterUpdatedNameIntegrationEvent(int character_id)
        {
            Character_id = character_id;
        }
        public CharacterAfterUpdatedNameIntegrationEvent()
        {

        }
    }
}
