using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Contracts : BaseConnector
    {
        /// <summary>
        /// /contracts/public/{region_id}/
        /// </summary>
        [Fact]
        public void PublicContracts()
        {
            var region_id = 10000051;
            ExecuteAndOutput(connector.Contracts.Contracts(region_id));
        }

        /// <summary>
        /// /contracts/public/items/{contract_id}/
        /// </summary>
        [Fact]
        public void PublicContractItems()
        {
            int contractId = 127416559;
            ExecuteAndOutput(connector.Contracts.ContractItems(contractId));
        }

        /// <summary>
        /// /contracts/public/bids/{contract_id}/
        /// </summary>
        [Fact]
        public void PublicContractBids() {
            int contractId = 127416559;
            ExecuteAndOutput(connector.Contracts.ContractBids(contractId));
        }

        /// <summary>
        /// GET /characters/{character_id}/contracts/
        /// </summary>
        [Fact]
        public void CharacterContractsResult()
        {
            ExecuteAndOutput(connector.Contracts.CharacterContracts());
        }

        /// <summary>
        /// GET /characters/{character_id}/contracts/{contract_id}/items/
        /// </summary>
        [Fact]
        public void CharacterContractsItemsResult()
        {
            int contractId = 127416559;
            ExecuteAndOutput(connector.Contracts.CharacterContractItems(contractId));
        }

        /// <summary>
        /// GET /characters/{character_id}/contracts/{contract_id}/bids/
        /// </summary>
        [Fact]
        public void CharacterContractsBidsResult()
        {
            int contractId = 127416559;
            ExecuteAndOutput(connector.Contracts.CharacterContractBids(contractId));
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/contracts/
        /// </summary>
        [Fact]
        public void CorporationContractsResult()
        {
            ExecuteAndOutput(connector.Contracts.CorporationContracts());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/contracts/{contract_id}/items/
        /// </summary>
        [Fact]
        public void CorporationContractsItemsResult()
        {
            int contractId = 127304518;
            ExecuteAndOutput(connector.Contracts.CorporationContractItems(contractId));
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/contracts/{contract_id}/bids/
        /// </summary>
        [Fact]
        public void CorporationContractsBidsResult()
        {
            int contractId = 127304518;
            ExecuteAndOutput(connector.Contracts.CorporationContractBids(contractId));
        }
    }
}
