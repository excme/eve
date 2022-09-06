
using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CorporationCeoUpdatedIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Corporation_id { get; set; }
        public int Ceo_Id { get; set; }
        public CorporationCeoUpdatedIntegrationEvent(int corporation_id, int ceo_id)
        {
            Corporation_id = corporation_id;
            Ceo_Id = ceo_id;
        }
        public CorporationCeoUpdatedIntegrationEvent()
        {

        }
    }
}
