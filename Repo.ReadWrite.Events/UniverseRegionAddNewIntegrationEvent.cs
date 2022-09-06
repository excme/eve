

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class UniverseRegionUpdatedIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public long Region_id { get; set; }
        public UniverseRegionUpdatedIntegrationEvent(long region_id)
        {
            Region_id = region_id;
        }
        public UniverseRegionUpdatedIntegrationEvent()
        {

        }
    }
}
