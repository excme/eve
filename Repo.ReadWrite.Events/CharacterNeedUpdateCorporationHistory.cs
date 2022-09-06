namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Запрос обновления у персонажа истории корпораций
    /// </summary>
    public class CharacterNeedUpdateCorporationHistoryIntehrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Character_Id { get; set; }
        public CharacterNeedUpdateCorporationHistoryIntehrationEvent()
        {

        }
        public CharacterNeedUpdateCorporationHistoryIntehrationEvent(int character_id)
        {
            Character_Id = character_id;
        }
    }
}
