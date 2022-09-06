namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Запрос. Необходимо обновить публичную информацию у корпорации
    /// </summary>
    public class CorporationNeedUpdatePublicInfoIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public CorporationNeedUpdatePublicInfoIntegrationEvent()
        {

        }
        public CorporationNeedUpdatePublicInfoIntegrationEvent(int corporation_id)
        {
            Corporation_id = corporation_id;
        }

        public int Corporation_id { get; private set; }
    }
}
