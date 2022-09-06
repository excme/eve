

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CharacterNeedUpdateAffilationIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Character_id { get; set; }
        public CharacterNeedUpdateAffilationIntegrationEvent(int character_id)
        {
            Character_id = character_id;
        }
        public CharacterNeedUpdateAffilationIntegrationEvent()
        {

        }
    }
}
