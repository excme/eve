using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class SearchLogic : BaseLogic
    {
        public SearchLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /search/ and /characters/{character_id}/search/
        /// </summary>
        /// <param name="search">The string to search on</param>
        /// <param name="categories">Type of entities to search for</param>
        /// <param name="isStrict">Whether the search should be a strict match</param>
        /// <param name="language">Language to use in the response</param>
        /// <returns></returns>
        public EsiResponse<SearchInfoResult> Query(string search, SearchCategory categories, bool isStrict = false, string language = "en-us")
        {
            var categoryList = categories.ToEsiValue();
            
            var endpoint = "/search/";
            Dictionary<string, string> replacements = null;
            RequestSecurity security = RequestSecurity.Public;

            var response = Execute<SearchInfoResult>(_client, _config, security, RequestMethod.Get, endpoint, replacements, parameters: new string[] {
                $"search={search}",
                $"categories={categoryList}",
                $"strict={isStrict}",
                $"language={language}"
            },
            token: _data?.AccessToken);

            return response;
        }

        public EsiResponse<CharacterSearchResult> ForCharacter(string search, SearchCategory categories, bool isStrict = false, string language = "en-us")
        {
            var categoryList = categories.ToEsiValue();

            var endpoint = "/characters/{character_id}/search/";
            Dictionary<string, string> replacements = new Dictionary<string, string>()
            {
                { "character_id", character_id.ToString() }
            };
            RequestSecurity security = RequestSecurity.Authenticated;

            var response = Execute<CharacterSearchResult>(_client, _config, security, RequestMethod.Get, endpoint, replacements, parameters: new string[] {
                $"search={search}",
                $"categories={categoryList}",
                $"strict={isStrict}",
                $"language={language}"
            },
            token: _data?.AccessToken);

            return response;
        }
    }
}
