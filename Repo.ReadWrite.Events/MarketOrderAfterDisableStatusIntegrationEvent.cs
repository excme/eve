namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class MarketOrderAfterDisableStatusIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public long Order_id { get; set; }
        public MarketOrderAfterDisableStatusIntegrationEvent()
        {

        }
        public MarketOrderAfterDisableStatusIntegrationEvent(long order_id)
        {
            Order_id = order_id;
        }
    }
}
