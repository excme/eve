

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CorporationMemberCountUpdatedIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Corporation_id { get; set; }
        public int Member_Count { get; set; }
        public CorporationMemberCountUpdatedIntegrationEvent(int corporation_id, int member_count)
        {
            Corporation_id = corporation_id;
            Member_Count = member_count;
        }
        public CorporationMemberCountUpdatedIntegrationEvent()
        {

        }
    }
}
