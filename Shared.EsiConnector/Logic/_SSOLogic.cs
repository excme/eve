using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eveDirect.Shared.EsiConnector
{
    public class SsoLogic
    {
        private readonly HttpClient _client;
        private readonly EsiConfig _config;
        private readonly ILogger _logger;
        private readonly string _clientKey;
        private readonly string _ssoUrl;

        public SsoLogic(HttpClient client, EsiConfig config, ILogger logger)
        {
            _client = client;
            _config = config;
            _logger = logger;

            switch (_config.DataSource)
            {
                case DataSource.tranquility:
                    _ssoUrl = "https://login.eveonline.com";
                    break;
                case DataSource.singularity:
                    _ssoUrl = "https://sisilogin.testeveonline.com";
                    break;
                case DataSource.serenity:
                    _ssoUrl = "https://login.evepc.163.com";
                    break;
            }
            _clientKey = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{config.ClientId}:{config.SecretKey}"));
        }

        public string CreateAuthenticationUrl(List<string> scopes = null)
            => $"{_ssoUrl}/oauth/authorize/?response_type=code&redirect_uri={Uri.EscapeDataString(_config.CallbackUrl)}&client_id={_config.ClientId}{((scopes != null) ? $"&scope={string.Join(" ", scopes)}" : "")}";

        /// <summary>
        /// SSO Token helper
        /// </summary>
        /// <param name="grantType"></param>
        /// <param name="code">The authorization_code or the refresh_token</param>
        /// <returns></returns>
        public SsoToken GetToken(GrantType grantType, string code)
        {
            var body = $"grant_type={grantType.ToEsiValue()}";
            if (grantType == GrantType.AuthorizationCode)
                body += $"&code={code}";
            else if (grantType == GrantType.RefreshToken)
                body += $"&refresh_token={code}";

            HttpContent postBody = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _clientKey);

            var response = _client.PostAsync($"{_ssoUrl}/oauth/token", postBody).Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (response.Length == 0 || response.Contains("error"))
            {
                // Ошибка
            }
            else if (response.Length > 0)
            {
                return JsonSerializer.Deserialize<SsoToken>(response);
            }

            return default;
        }

        /// <summary>
        /// Проверка access_token
        /// Verifies the Character information for the provided Token information.
        /// While this method represents the oauth/verify request, in addition to the verified data that ESI returns, this object also stores the Token and Refresh token
        /// and this method also uses ESI retrieves other information pertinent to making calls in the ESI.NET API. (alliance_id, corporation_id, faction_id)
        /// You will need a record in your database that stores at least this information. Serialize and store this object for quick retrieval and token refreshing.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public AuthorizedCharacterData Verify(SsoToken token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var response = _client.GetAsync($"{_ssoUrl}/oauth/verify").Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (response.Length > 0)
            {
                var authorizedCharacter = JsonSerializer.Deserialize<AuthorizedCharacterData>(response);
                authorizedCharacter.AccessToken = token.AccessToken;
                authorizedCharacter.RefreshToken = token.RefreshToken;
                authorizedCharacter.ExpiresIn = DateTime.UtcNow.AddSeconds(token.ExpiresIn);

                var url = $"{_config.EsiUrl}v1/characters/affiliation/?datasource={_config.DataSource.ToEsiValue()}";
                var body = new StringContent(JsonSerializer.Serialize(new int[] { authorizedCharacter.CharacterID }), Encoding.UTF8, "application/json");

                // Get more specifc details about authorized character to be used in API calls that require this data about the character
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                var characterResponse = client.PostAsync(url, body)//.ConfigureAwait(false);
                    .GetAwaiter().GetResult();

                //var characterResponse = new CharacterLogic(_client, _config, authorizedCharacter).Affiliation(new int[] { authorizedCharacter.CharacterID }).ConfigureAwait(false).GetAwaiter().GetResult();
                if (characterResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    EsiResponse<CharacterAffiliationResult> affiliations = new EsiResponse<CharacterAffiliationResult>(_logger, characterResponse, "Post|/character/affiliations/", "v1", url);
                    var characterData = affiliations.Data.First();

                    authorizedCharacter.AllianceID = characterData.alliance_id;
                    authorizedCharacter.CorporationID = characterData.corporation_id;
                    //authorizedCharacter.FactionID = characterData.faction_id;
                }

                return authorizedCharacter;
            }

            return default;
        }
    }
}
