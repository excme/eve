using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CharacterAfterUpdatedFactionIntegrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Faction_id { get; set; }
        public int Character_id { get; set; }
        public CharacterAfterUpdatedFactionIntegrationEvent(int character_id, int faction_id)
        {
            Character_id = character_id;
            Faction_id = faction_id;
        }
        public CharacterAfterUpdatedFactionIntegrationEvent()
        {

        }
    }
}
