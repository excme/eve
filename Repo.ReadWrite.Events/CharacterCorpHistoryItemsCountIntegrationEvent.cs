using eveDirect.Shared.EventBus.Events;

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CharacterCorpHistoryItemsCountIntegrationEvent: IntegrationEvent
    {
        public int Total_Count { get; set; }
        public CharacterCorpHistoryItemsCountIntegrationEvent()
        {

        }
        public CharacterCorpHistoryItemsCountIntegrationEvent(int total_Count)
        {
            Total_Count = total_Count;
        }
    }
}
