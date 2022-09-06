using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    public class CharacterIpdateSecurityStatusIntergrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public int Character_id { get; set; }
        public CharacterIpdateSecurityStatusIntergrationEvent(int character_id)
        {
            Character_id = character_id;
        }
        public CharacterIpdateSecurityStatusIntergrationEvent()
        {

        }
    }
}
