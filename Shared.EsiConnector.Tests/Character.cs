using System.Collections.Generic;
using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Character : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/stats/
        /// </summary>
        [Fact]
        public void CharacterStatResult()
        {
            ExecuteAndOutput(connector.Character.Stats());
        }

        /// <summary>
        /// GET /characters/{character_id}/
        /// </summary>
        [Fact]
        public void CharacterInfoResult()
        {
            ExecuteAndOutput(connector.Character.Information(10));
        }

        /// <summary>
        /// POST /characters/affiliation/
        /// </summary>
        [Fact]
        public void CharacterAffiliationResult()
        {
            ExecuteAndOutput(connector.Character.Affiliation(new List<int>() { characterId }));
        }

        /// <summary>
        /// POST /characters/{character_id}/cspa/
        /// </summary>
        [Fact]
        public void CharactersCspaResult()
        {
            //int anotherId = 90522832;
            //ExecuteAndOutput(connector.Character.CalculateCSPA(anotherId));
        }

        /// <summary>
        /// GET /characters/names/
        /// </summary>
        [Fact]
        public void CharacterNameResult()
        {
            ExecuteAndOutput(connector.Character.Names(characterId));
        }

        /// <summary>
        /// GET /characters/{character_id}/portrait/
        /// </summary>
        [Fact]
        public void CharacterPortraitResult()
        {
            ExecuteAndOutput(connector.Character.Portrait(characterId));
        }

        /// <summary>
        /// GET /characters/{character_id}/corporationhistory/
        /// </summary>
        [Fact]
        public void CharacterCorporationHistoryResult()
        {
            ExecuteAndOutput(connector.Character.CorporationHistory(characterId));
        }

        /// <summary>
        /// GET /characters/{character_id}/chat_channels/
        /// </summary>
        [Fact]
        public void CharacterChatChannelResult()
        {
            ExecuteAndOutput(connector.Character.ChatChannels());
        }

        /// <summary>
        /// GET /characters/{character_id}/medals/
        /// </summary>
        [Fact]
        public void CharacterMedalsResult()
        {
            ExecuteAndOutput(connector.Character.Medals());
        }

        /// <summary>
        /// GET /characters/{character_id}/standings/
        /// </summary>
        [Fact]
        public void CharacterStandingsResult()
        {
            ExecuteAndOutput(connector.Character.Standings());
        }

        /// <summary>
        /// GET /characters/{character_id}/agents_research/
        /// </summary>
        [Fact]
        public void CharacterAgentsResearchResult()
        {
            ExecuteAndOutput(connector.Character.AgentsResearch());
        }

        /// <summary>
        /// GET /characters/{character_id}/blueprints/
        /// </summary>
        [Fact]
        public void CharacterBlueprintsResult()
        {
            ExecuteAndOutput(connector.Character.Blueprints());
        }

        /// <summary>
        /// GET /characters/{character_id}/fatigue/
        /// </summary>
        [Fact]
        public void CharacterFatigueResult()
        {
            ExecuteAndOutput(connector.Character.Fatigue());
        }

        /// <summary>
        /// GET /characters/{character_id}/notifications/contacts/
        /// </summary>
        [Fact]
        public void CharacterNotificationsContactsResult()
        {
            ExecuteAndOutput(connector.Character.ContactNotifications());
        }

        /// <summary>
        /// GET /characters/{character_id}/notifications/
        /// </summary>
        [Fact]
        public void CharacterNotificationsResult()
        {
            ExecuteAndOutput(connector.Character.Notifications());
        }

        /// <summary>
        /// GET /characters/{character_id}/roles/
        /// </summary>
        [Fact]
        public void CharacterRolesResult()
        {
            ExecuteAndOutput(connector.Character.Roles());
        }

        /// <summary>
        /// GET /characters/{character_id}/titles/
        /// </summary>
        [Fact]
        public void CharacterTitlesResult()
        {
            ExecuteAndOutput(connector.Character.Titles());
        }
    }
}
