using eveDirect.Shared.EsiConnector.Models.SSO;
using eveDirect.Shared.EsiConnector.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class WalletLogic : BaseLogic
    {
        public WalletLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /characters/{character_id}/wallet/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<double> CharacterWallet()
            => Execute<double>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/wallet/", replacements: new Dictionary<string, string>()
            {
                { "character_id", character_id.ToString() }
            }, token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/wallet/journal/
        /// </summary>
        /// <param name="from_id"></param>
        /// <returns></returns>
        public EsiResponse<CharacterWalletJournalResult> CharacterJournal(int page = 1)
            => Execute<CharacterWalletJournalResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/wallet/journal/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);


        /// <summary>
        /// /characters/{character_id}/wallet/transactions/
        /// </summary>
        /// <param name="from_id"></param>
        /// <returns></returns>
        public EsiResponse<WalletTransactionsCharacterResult> CharacterTransactions(int page = 1)
            => Execute<WalletTransactionsCharacterResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/wallet/transactions/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/wallets/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationWalletsResult> CorporationWallets()
            => Execute<CorporationWalletsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/wallets/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/wallets/{division}/journal/
        /// </summary>
        /// <param name="division"></param>
        /// <param name="from_id"></param>
        /// <returns></returns>
        public EsiResponse<CorporationWalletJournalResult> CorporationJournal(int division, int page = 1)
            => Execute<CorporationWalletJournalResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/wallets/{division}/journal/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() },
                    { "division", division.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/wallets/{division}/transactions/
        /// </summary>
        /// <param name="division"></param>
        /// <param name="from_id"></param>
        /// <returns></returns>
        public EsiResponse<WalletTransactionsCorporationResult> CorporationTransactions(int division, int page = 1)
            => Execute<WalletTransactionsCorporationResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/wallets/{division}/transactions/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() },
                    { "division", division.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);
    }
}