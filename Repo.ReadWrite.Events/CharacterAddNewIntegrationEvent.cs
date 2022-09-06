namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Событие. Только созданный персонаж
    /// </summary>
    public class CharacterAfterAddIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Character_id { get; set; }
        public bool New_Created { get; set; }
        public CharacterAfterAddIntegrationEvent(int character_id, bool new_created = false)
        {
            Character_id = character_id;
            New_Created = new_created;
        }
        public CharacterAfterAddIntegrationEvent() { }
    }
}
