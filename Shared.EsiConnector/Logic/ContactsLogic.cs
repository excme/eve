using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class ContactsLogic : BaseLogic
    {
        public ContactsLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /characters/{character_id}/contacts/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CharacterContactsResult> ListForCharacter(int page = 1)
            => Execute<CharacterContactsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/contacts/",
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
        /// /corporations/{corporation_id}/contacts/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<CorporationContactsResult> ListForCorporation(int page = 1)
            => Execute<CorporationContactsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/contacts/",
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
        /// /alliances/{alliance_id}/contacts/
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public EsiResponse<AllianceContactsResult> ListForAlliance(int page = 1)
            => Execute<AllianceContactsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/alliances/{alliance_id}/contacts/",
                replacements: new Dictionary<string, string>()
                {
                    { "alliance_id", alliance_id.ToString() }
                },
                parameters: new string[]
                {
                    $"page={page}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// Post /characters/{character_id}/contacts/
        /// </summary>
        /// <param name="contact_id"></param>
        /// <param name="standing"></param>
        /// <param name="label_id"></param>
        /// <param name="watched"></param>
        /// <returns></returns>
        public EsiResponse<List<int>> Add(int contact_id, decimal standing, int? label_id = null, bool? watched = null)
        {
            var body = new int[] { contact_id };

            var parameters = new List<string>() { $"standing={standing}" };

            if (label_id != null)
                parameters.Add($"label_id={label_id}");

            if (watched != null)
                parameters.Add($"watched={watched}");

            return Execute<List<int>>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/characters/{character_id}/contacts/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                parameters: parameters.ToArray(),
                body: body,
                token: _data.AccessToken);
        }

        /// <summary>
        /// /characters/{character_id}/contacts/
        /// </summary>
        /// <param name="contact_id"></param>
        /// <param name="standing"></param>
        /// <param name="label_id"></param>
        /// <param name="watched"></param>
        /// <returns></returns>
        public EsiResponse<string> Update(int contact_id, decimal standing, int? label_id = null, bool? watched = null)
        {
            var body = new int[] { contact_id };

            var parameters = new List<string>() { $"standing={standing}" };

            if (label_id != null)
                parameters.Add($"label_id={label_id}");

            if (watched != null)
                parameters.Add($"watched={watched}");

            return Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Put, "/characters/{character_id}/contacts/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                parameters: parameters.ToArray(),
                body: body,
                token: _data.AccessToken);
        }

        /// <summary>
        /// /characters/{character_id}/contacts/
        /// </summary>
        /// <param name="contact_ids"></param>
        /// <returns></returns>
        public EsiResponse<string> Delete(int[] contact_ids)
            => Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Delete, "/characters/{character_id}/contacts/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                parameters: new string[]
                {
                    $"contact_ids={string.Join(",", contact_ids)}"
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/contacts/labels/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<ContactsLabelsResult> LabelsForCharacter()
            => Execute<ContactsLabelsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/contacts/labels/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /corporations/{corporation_id}/contacts/labels/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<ContactsLabelsResult> LabelsForCorporation()
            => Execute<ContactsLabelsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/corporations/{corporation_id}/contacts/labels/",
                replacements: new Dictionary<string, string>()
                {
                    { "corporation_id", corporation_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /alliances/{alliance_id}/contacts/labels/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<ContactsLabelsResult> LabelsForAlliance()
            => Execute<ContactsLabelsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/alliances/{alliance_id}/contacts/labels/",
                replacements: new Dictionary<string, string>()
                {
                    { "alliance_id", alliance_id.ToString() }
                },
                token: _data.AccessToken);
    }
}