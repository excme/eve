using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class BookmarksLogic : BaseLogic
    {
        public BookmarksLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /characters/{character_id}/bookmarks/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<BookmarksResult> ForCharacter(int page = 1)
            =>  Execute<BookmarksResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/bookmarks/",
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
        /// /characters/{character_id}/bookmarks/folders/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterBookmarksFoldersResult> FoldersForCharacter(int page = 1)
            =>  Execute<CharacterBookmarksFoldersResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/bookmarks/folders/",
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
        /// /corporations/{corporation_id}/bookmarks/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<BookmarksResult> ForCorporation(int page = 1)
            =>  Execute<BookmarksResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/bookmarks/",
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
        /// /corporations/{corporation_id}/bookmarks/folders/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CorporationBookmarksFoldersResult> FoldersForCorporation(int page = 1)
            =>  Execute<CorporationBookmarksFoldersResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/bookmarks/folders/",
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