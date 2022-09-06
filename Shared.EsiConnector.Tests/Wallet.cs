using Xunit;

namespace eveDirect.EsiConnector.Tests
{
    public class Wallet : BaseConnector
    {
        /// <summary>
        /// GET /characters/{character_id}/wallet/
        /// </summary>
        [Fact]
        public void CharacterWallet()
        {
            //ExecuteAndOutput(connector.Wallet.CharacterWallet());
        }

        /// <summary>
        /// GET /characters/{character_id}/wallet/journal/
        /// </summary>
        [Fact]
        public void CharacterWalletJournalResult()
        {
            ExecuteAndOutput(connector.Wallet.CharacterJournal(characterId));
        }

        /// <summary>
        /// GET /characters/{character_id}/wallet/transactions/
        /// </summary>
        [Fact]
        public void CharacterWalletTransactionsResult()
        {
            ExecuteAndOutput(connector.Wallet.CharacterTransactions(characterId));
        }
        
        /// <summary>
        /// GET /corporations/{corporation_id}/wallets/{division}/journal/
        /// </summary>
        [Fact]
        public void CorporationWalletsJournalResult()
        {
            var divisionId = 1;
            ExecuteAndOutput(connector.Wallet.CorporationJournal(corporationId, divisionId));
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/wallets/
        /// </summary>
        [Fact]
        public void CorporationWalletsResult()
        {
            ExecuteAndOutput(connector.Wallet.CorporationWallets());
        }

        /// <summary>
        /// GET /corporations/{corporation_id}/wallets/{division}/transactions/
        /// </summary>
        [Fact]
        public void CorporationWalletsTransactionsResult()
        {
            var divisionId = 1;
            ExecuteAndOutput(connector.Wallet.CorporationTransactions(divisionId));
        }
    }
}
