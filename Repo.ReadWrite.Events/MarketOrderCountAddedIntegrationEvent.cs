using System;

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Событие. Количество добавленный ордеров
    /// </summary>
    public class MarketOrderCountAddedIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public MarketOrderCountAddedIntegrationEvent()
        {

        }

        public int Count { get; set; }
        public DateTime OnDate { get; set; }

        public MarketOrderCountAddedIntegrationEvent(DateTime onDate, int count)
        {
            Count = count;
            OnDate = onDate;
        }
    }
}
