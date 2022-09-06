using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class MailLogic : BaseLogic
    {
        public MailLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// GET /characters/{character_id}/mail/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterMailsResult> Headers(long[] labels = null, int last_mail_id = 0)
        {
            var parameters = new List<string>();

            if (labels != null)
                parameters.Add($"labels={string.Join(",", labels)}");

            if (last_mail_id > 0)
                parameters.Add($"last_mail_id={last_mail_id}");

            var response =  Execute<CharacterMailsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/mail/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                parameters: parameters.ToArray(),
                token: _data.AccessToken);

            return response;
        }

        /// <summary>
        /// POST /characters/{character_id}/mail/
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="approved_cost"></param>
        /// <returns></returns>
        public EsiResponse<int> New(object[] recipients, string subject, string body, int approved_cost = 0)
            =>  Execute<int>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/characters/{character_id}/mail/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                body: new
                {
                    recipients,
                    subject,
                    body,
                    approved_cost
                },
                token: _data.AccessToken);

        /// <summary>
        /// Get /characters/{character_id}/mail/labels/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterMailLabelsResult> Labels()
            =>  Execute<CharacterMailLabelsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/mail/labels/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/mail/labels/
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public EsiResponse<int> NewLabel(string name, string color)
            =>  Execute<int>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Post, "/characters/{character_id}/mail/labels/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                body: new
                {
                    name,
                    color
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/mail/labels/{label_id}/
        /// </summary>
        /// <param name="label_id"></param>
        /// <returns></returns>
        public EsiResponse<string> DeleteLabel(long label_id)
            =>  Execute<string>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Delete, "/characters/{character_id}/mail/labels/{label_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "label_id", label_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// /characters/{character_id}/mail/lists/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterMailListsResult> MailingLists()
            =>  Execute<CharacterMailListsResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/mail/lists/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// Get /characters/{character_id}/mail/{mail_id}/
        /// </summary>
        /// <param name="mail_id"></param>
        /// <returns></returns>
        public EsiResponse<CharacterMailInfoResult> Info(int mail_id)
            =>  Execute<CharacterMailInfoResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/mail/{mail_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "mail_id", mail_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// Put /characters/{character_id}/mail/{mail_id}/
        /// </summary>
        /// <param name="mail_id"></param>
        /// <param name="is_read"></param>
        /// <param name="labels"></param>
        /// <returns></returns>
        public EsiResponse<CharacterMailInfoResult> Update(int mail_id, bool? is_read = null, int[] labels = null)
            =>  Execute<CharacterMailInfoResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Put, "/characters/{character_id}/mail/{mail_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "mail_id", mail_id.ToString() }
                },
                body: BuildUpdateObject(is_read, labels),
                token: _data.AccessToken);

        /// <summary>
        /// Delete /characters/{character_id}/mail/{mail_id}/
        /// </summary>
        /// <param name="mail_id"></param>
        /// <returns></returns>
        public EsiResponse<CharacterMailInfoResult> Delete(int mail_id)
            =>  Execute<CharacterMailInfoResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Delete, "/characters/{character_id}/mail/{mail_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "mail_id", mail_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="is_read"></param>
        /// <param name="labels"></param>
        /// <returns></returns>
        private static dynamic BuildUpdateObject(bool? is_read, int[] labels = null)
        {
            dynamic body = null;

            if (is_read != null && labels == null)
                body = new { is_read };
            else if (is_read == null && labels != null)
                body = new { labels };
            else
                body = new { is_read, labels };
            return body;
        }
    }
}