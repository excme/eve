namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Персонаж завершил участие в альянсе
    /// </summary>
    public class CharacterBeforeChangeAllianceIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Alliance_id { get; set; }
        public int Character_id { get; set; }
        public CharacterBeforeChangeAllianceIntegrationEvent(int character_id, int alliance_id)
        {
            Character_id = character_id;
            Alliance_id = alliance_id;
        }
        public CharacterBeforeChangeAllianceIntegrationEvent()
        {

        }
    }
}
