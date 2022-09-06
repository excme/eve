

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class MarketOrderAfterAddIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public long Order_id { get; set; }
        public MarketOrderAfterAddIntegrationEvent()
        {

        }
        public MarketOrderAfterAddIntegrationEvent(long order_id)
        {
            Order_id = order_id;
        }
    }
}
