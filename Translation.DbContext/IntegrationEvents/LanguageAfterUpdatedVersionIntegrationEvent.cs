using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Shared.EventBus.Events;

namespace eveDirect.Translation.DbContext.IntegrationEvents
{
    public class LanguageAfterUpdatedVersionIntegrationEvent : IntegrationEvent
    {
        public string Lang { get; set; }
        public LanguageAfterUpdatedVersionIntegrationEvent()
        {

        }
        public LanguageAfterUpdatedVersionIntegrationEvent(string lang)
        {
            Lang = lang;
        }
    }
}
