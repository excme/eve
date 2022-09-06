using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class LoyaltyLogic : BaseLogic
    {
        public LoyaltyLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /loyalty/stores/{corporation_id}/offers/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<LoyaltyStoresOffersResult> Offers(int corporation_id)
            => Execute<LoyaltyStoresOffersResult>(_client, _config, RequestSecurity.Public, RequestMethod.Get, "/loyalty/stores/{corporation_id}/offers/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                });

        /// <summary>
        /// /characters/{character_id}/loyalty/points/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterLoyaltyPointsResult> Points()
            => Execute<CharacterLoyaltyPointsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/loyalty/points/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);
    }
}