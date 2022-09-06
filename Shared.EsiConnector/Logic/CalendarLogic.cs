using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System.Collections.Generic;
using System.Net.Http;
using static eveDirect.Shared.EsiConnector.EsiRequest;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class CalendarLogic : BaseLogic
    {
        public CalendarLogic(HttpClient client, EsiConfig config, Microsoft.Extensions.Logging.ILogger logger, AuthorizedCharacterData data = null) : base(client, config, logger, data) { }

        /// <summary>
        /// /characters/{character_id}/calendar/
        /// </summary>
        /// <returns></returns>
        public EsiResponse<CharacterCalendarResult> Events()
            =>  Execute<CharacterCalendarResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/calendar/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// GET /characters/{character_id}/calendar/{event_id}/
        /// </summary>
        /// <param name="contract_id"></param>
        /// <returns></returns>
        public EsiResponse<CharacterCalendarEventResult> Event(int event_id)
            =>  Execute<CharacterCalendarEventResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/calendar/{event_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "event_id", event_id.ToString() }
                },
                token: _data.AccessToken);

        /// <summary>
        /// PUT /characters/{character_id}/calendar/{event_id}/
        /// </summary>
        /// <param name="event_id"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public EsiResponse<CharacterCalendarEventResult> Respond(int event_id, EventResponse eventResponse)
            =>  Execute<CharacterCalendarEventResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Put, "/characters/{character_id}/calendar/{event_id}/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "event_id", event_id.ToString() }
                },
                body: new
                {
                    response = eventResponse.ToEsiValue()
                },
                token: _data.AccessToken);

        /// <summary>
        /// GET /characters/{character_id}/calendar/{event_id}/attendees/
        /// </summary>
        /// <param name="contract_id"></param>
        /// <returns></returns>
        public EsiResponse<CharacterCalendarAttendeesResult> Responses(int event_id)
            =>  Execute<CharacterCalendarAttendeesResult>(_client, _config, RequestSecurity.Authenticated, RequestMethod.Get, "/characters/{character_id}/calendar/{event_id}/attendees/",
                replacements: new Dictionary<string, string>()
                {
                    { "character_id", character_id.ToString() },
                    { "event_id", event_id.ToString() }
                },
                token: _data.AccessToken);
    }
}