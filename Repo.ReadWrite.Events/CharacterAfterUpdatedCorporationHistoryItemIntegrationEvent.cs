using System;

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Уведомление, что у персонажа добавилась или обновилась запись в истории корпорации
    /// </summary>
    public class CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Character_Id { get; set; }
        public bool New_Created { get; set; }
        public DateTime? OnDate { get; set; }

        public CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent(int character_id, DateTime? onDate, bool new_created = false)
        {
            Character_Id = character_id;
            New_Created = new_created;
            OnDate = onDate;
        }
        public CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent() { }
    }
}
