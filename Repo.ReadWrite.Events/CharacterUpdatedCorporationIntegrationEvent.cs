namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Персонаж изменил корпорацию
    /// </summary>
    public class CharacterAfterUpdatedCorporationIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Corporation_id { get; set; }
        public int Character_id { get; set; }
        public CharacterAfterUpdatedCorporationIntegrationEvent(int character_id, int corporation_id)
        {
            Character_id = character_id;
            Corporation_id = corporation_id;
        }
        public CharacterAfterUpdatedCorporationIntegrationEvent()
        {

        }
    }
}
