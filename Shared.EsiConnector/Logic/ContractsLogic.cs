using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class ContractsLogic : BaseLogic
    {
        public ContractsLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /contracts/public/{region_id}/
        /// </summary>
        /// <param name="region_id"></param>
        /// <returns></returns>
        public EsiResponse<ContractsResult> Contracts(int region_id, int page = 1)
            => Execute<ContractsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/contracts/public/{region_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "region_id", region_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                });

        /// <summary>
        /// /contracts/public/items/{contract_id}/
        /// </summary>
        /// <param name="contract_id"></param>
        /// <returns></returns>
        public EsiResponse<ContractsItemsResult> ContractItems(int contract_id, int page = 1)
            => Execute<ContractsItemsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/contracts/public/items/{contract_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "contract_id", contract_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                });

        /// <summary>
        /// /contracts/public/bids/{contract_id}/
        /// </summary>
        /// <param name="contract_id"></param>
        /// <returns></returns>
        public EsiResponse<ContractsBidsResult> ContractBids(int contract_id, int page = 1)
            => Execute<ContractsBidsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/contracts/public/bids/{contract_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "contract_id", contract_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                });

        /// <summary>
        /// /characters/{character_id}/contracts/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterContractsResult> CharacterContracts(int page = 1)
            => Execute<CharacterContractsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/contracts/",
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
        /// /characters/{character_id}/contracts/{contract_id}/items/
        /// </summary>
        /// <param name="contract_id"></param>
        /// <returns></returns>
        public EsiResponse<CharacterContractsItemsResult> CharacterContractItems(int contract_id, int page = 1)
            => Execute<CharacterContractsItemsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/contracts/{contract_id}/items/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "contract_id", contract_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/contracts/{contract_id}/bids/
        /// </summary>
        /// <param name="contract_id"></param>
        /// <returns></returns>
        public EsiResponse<CharCorpContractsBidsResult> CharacterContractBids(int contract_id, int page = 1)
            => Execute<CharCorpContractsBidsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/contracts/{contract_id}/bids/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "contract_id", contract_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/contracts/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationContractsResult> CorporationContracts(int page = 1)
            => Execute<CorporationContractsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/contracts/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/contracts/{contract_id}/items/
        /// </summary>
        /// <param name="contract_id"></param>
        /// <returns></returns>
        public EsiResponse<CorporationContractsItemsRresult> CorporationContractItems(int contract_id, int page = 1)
            => Execute<CorporationContractsItemsRresult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/contracts/{contract_id}/items/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() },
                    { "contract_id", contract_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/contracts/{contract_id}/bids/
        /// </summary>
        /// <param name="contract_id"></param>
        /// <returns></returns>
        public EsiResponse<CharCorpContractsBidsResult> CorporationContractBids(int contract_id, int page = 1)
            => Execute<CharCorpContractsBidsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/contracts/{contract_id}/bids/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() },
                    { "contract_id", contract_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);
    }
}