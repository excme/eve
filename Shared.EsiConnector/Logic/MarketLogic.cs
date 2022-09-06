using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class MarketLogic : BaseLogic
    {
        public MarketLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /markets/prices/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<MarketsPricesResult> Prices()
            =>  Execute<MarketsPricesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/markets/prices/");

        /// <summary>
        /// Get /markets/{region_id}/orders/
        /// </summary>
        /// <param name="region_id"></param>
        /// <param name="order_type"></param>
        /// <param name="page"></param>
        /// <param name="type_id"></param>
        /// <returns></returns>
        public EsiResponse<MarketsOrdersResult> RegionOrders(
            int region_id, 
            MarketOrderType order_type = MarketOrderType.All, 
            int? type_id = null,
            int page = 1)
        {
            var parameters = new List<string>() { $"order_type={order_type.ToEsiValue()}" };
            parameters.Add($"page={page}");

            if (type_id != null)
                parameters.Add($"type_id={type_id}");

            var response =  Execute<MarketsOrdersResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/markets/{region_id}/orders/",
                replacements: new Dictionary<string, string>()
                {
                    { "region_id", region_id.ToString() }
                },
                parameters: parameters.ToArray());

            return response;
        }

        /// <summary>
        /// /markets/{region_id}/history/
        /// </summary>
        /// <param name="region_id"></param>
        /// <param name="type_id"></param>
        /// <returns></returns>
        public EsiResponse<MarketsHistoryResult> TypeHistoryInRegion(int region_id, int type_id)
            =>  Execute<MarketsHistoryResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/markets/{region_id}/history/",
                replacements: new Dictionary<string, string>()
                {
                    { "region_id", region_id.ToString() }
                },
                parameters: new string[]
                {
                    $"type_id={type_id}"
                });

        /// <summary>
        /// /markets/structures/{structure_id}/
        /// </summary>
        /// <param name="structure_id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<MarketsStructureOrdersResult> StructureOrders(long structure_id, int page = 1)
            =>  Execute<MarketsStructureOrdersResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/markets/structures/{structure_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "structure_id", structure_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /markets/groups/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<MarketsGroupsResult> Groups()
            =>  Execute<MarketsGroupsResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/markets/groups/");

        /// <summary>
        /// /markets/groups/{market_group_id}/
        /// </summary>
        /// <param name="market_group_id"></param>
        /// <returns></returns>
        public EsiResponse<MarketsGroupInfoResult> Group(int market_group_id, ELanguages lang = ELanguages.en_us)
            =>  Execute<MarketsGroupInfoResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/markets/groups/{market_group_id}/",
                replacements: new Dictionary<string, string>() { { "market_group_id", market_group_id.ToString() } }, 
                parameters: new string[] { $"language={lang.ToArg()}" });

        /// <summary>
        /// /characters/{character_id}/orders/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterOrdersResult> CharacterOrders()
            =>  Execute<CharacterOrdersResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/orders/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/orders/history/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CharacterOrdersHistoryResult> CharacterOrderHistory(int page = 1)
            =>  Execute<CharacterOrdersHistoryResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/orders/history/",
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
        /// /markets/{region_id}/types/
        /// </summary>
        /// <param name="region_id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<MarketsTypesResult> Types(int region_id, int page = 1)
            =>  Execute<MarketsTypesResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/markets/{region_id}/types/",
                replacements: new Dictionary<string, string>()
                {
                    { "region_id", region_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                });

        /// <summary>
        /// /corporations/{corporation_id}/orders/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationOrdersResult> CorporationOrders(int page = 1)
            =>  Execute<CorporationOrdersResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/orders/",
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
        /// /corporations/{corporation_id}/orders/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationOrdersHistoryResult> CorporationOrderHistory(int page = 1)
            =>  Execute<CorporationOrdersHistoryResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/orders/history/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);
    }
}