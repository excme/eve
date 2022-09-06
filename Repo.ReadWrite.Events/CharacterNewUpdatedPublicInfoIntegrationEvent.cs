namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Персонаж, который после добавления в базу по ид, были полученых его публичные данные
    /// </summary>
    public class CharacterNewUpdatedPublicInfoIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Character_id { get; set; }
        public bool New_Created { get; set; }
        public CharacterNewUpdatedPublicInfoIntegrationEvent(int character_id, bool new_Created = false)
        {
            Character_id = character_id;
            New_Created = new_Created;
        }
        public CharacterNewUpdatedPublicInfoIntegrationEvent() { }
    }
}
