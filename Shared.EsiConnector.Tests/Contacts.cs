using Xunit;
namespace eveDirect.EsiConnector.Tests
{
    public class Contacts : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/contacts/
        /// </summary>
        [Fact]
        public void CharacterContactsResult()
        {
            ExecuteAndOutput(connector.Contacts.ListForCharacter());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/contacts/
        /// </summary>
        [Fact]
        public void CorporationContactsResult()
        {
            ExecuteAndOutput(connector.Contacts.ListForCorporation());
        }

        /// <summary>
        /// GET /alliances/{alliance_id}/contacts/
        /// </summary>
        [Fact]
        public void AlliancesContactsResult()
        {
            ExecuteAndOutput(connector.Contacts.ListForAlliance(allinceId));
        }

        /// <summary>
        /// GET /characters/{character_id}/contacts/labels/
        /// </summary>
        [Fact]
        public void CharacterContactsLabelsResult()
        {
            ExecuteAndOutput(connector.Contacts.LabelsForCharacter());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/contacts/labels/
        /// </summary>
        [Fact]
        public void CorporationContactsLabelsResult()
        {
            ExecuteAndOutput(connector.Contacts.LabelsForCorporation());
        }

        /// <summary>
        /// GET /alliances/{alliance_id}/contacts/labels/
        /// </summary>
        [Fact]
        public void AllianceContactsLabelsResult()
        {
            ExecuteAndOutput(connector.Contacts.LabelsForAlliance());
        }
    }
}
