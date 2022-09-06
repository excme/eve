using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace eveDirect.Shared.EsiConnector
{
    public class EsiResponse<T>
    {
        public EsiResponse(
            ILogger logger, 
            HttpResponseMessage response, 
            string path,
            string version, 
            string endpoint, 
            string body = null)
        {
            Message = path; 
            
            StatusCode = response.StatusCode;
            Endpoint = path.Split('|')[1];
            Version = version;

            parseHeaders(response);

            // Парсинг ответа
            var result = response.Content?.ReadAsStringAsync().Result;
            if (response.StatusCode == HttpStatusCode.OK 
                || response.StatusCode == HttpStatusCode.Created)
            {
                if ((result.StartsWith("{") && result.EndsWith("}")) 
                    || (result.StartsWith("[") && result.EndsWith("]")))
                {
                    Data = JsonSerializer.Deserialize<T>(result);
                    isSuccess = true;
                }
            }else if(response.StatusCode == HttpStatusCode.NoContent)
            {
                // for PUT, DELETE
                isSuccess = true;
            }
            else if(response.StatusCode == HttpStatusCode.ResetContent)
            {
                // Если endpoint был на паузе
                Message = "Endpoint paused";
            }
            else
            {
                Message = result;
            }

            // Логирование
            if (!isSuccess)
            {
                using (LogContext.PushProperty("Endpoint", endpoint))
                using (LogContext.PushProperty("Pages", Pages))
                using (LogContext.PushProperty("Expires", Expires))
                using (LogContext.PushProperty("LastModified", LastModified))
                using (LogContext.PushProperty("ETag", ETag))
                using (LogContext.PushProperty("StatusCode", StatusCode))
                using (LogContext.PushProperty("body", body))
                using (LogContext.PushProperty("ErrorLimitRemain", ErrorLimitRemain))
                using (LogContext.PushProperty("ErrorLimitReset", ErrorLimitReset))
                {
                    logger?.LogError($"EsiResp|{(int)response.StatusCode}|{path}|{Message}");
                }
            }
        }

        void parseHeaders(HttpResponseMessage response)
        {
            if (response.Headers.Contains("X-ESI-Request-ID"))
                RequestId = Guid.Parse(response.Headers.GetValues("X-ESI-Request-ID").First());

            if (response.Headers.Contains("X-Pages"))
                Pages = int.Parse(response.Headers.GetValues("X-Pages").First());

            if (response.Headers.Contains("ETag"))
                ETag = response.Headers.GetValues("ETag").First().Replace("\"", string.Empty);

            if (response.Content?.Headers.Contains("Expires") ?? false)
                Expires = DateTime.Parse(response.Content.Headers.GetValues("Expires").First());

            if (response.Content?.Headers.Contains("Last-Modified") ?? false)
                LastModified = DateTime.Parse(response.Content.Headers.GetValues("Last-Modified").First());

            if (response.Headers.Contains("Date"))
                Date = DateTime.Parse(response.Headers.GetValues("Date").First());

            if (response.Headers.Contains("X-Esi-Error-Limit-Remain"))
                ErrorLimitRemain = int.Parse(response.Headers.GetValues("X-Esi-Error-Limit-Remain").First());

            if (response.Headers.Contains("X-Esi-Error-Limit-Reset"))
                ErrorLimitReset = int.Parse(response.Headers.GetValues("X-Esi-Error-Limit-Reset").First());
        }

        /// <summary>
        /// Ид запроса
        /// </summary>
        public Guid RequestId { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Endpoint { get; set; }
        public string Version { get; set; }
        /// <summary>
        /// Кэширование до
        /// </summary>
        public DateTime? Expires { get; set; }
        /// <summary>
        /// Последнее обновление данных
        /// </summary>
        public DateTime? LastModified { get; set; }
        /// <summary>
        /// Время ответа с сервера
        /// </summary>
        public DateTime? Date { get; set; }
        public string ETag { get; set; }
        /// <summary>
        /// Ограничение ошибок
        /// </summary>
        public int? ErrorLimitRemain { get; set; }
        /// <summary>
        /// Текущее количество ошибок
        /// </summary>
        public int? ErrorLimitReset { get; set; }
        public int? Pages { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        //public Exception Exception { get; set; }
        /// <summary>
        /// Успешно ли выполнение запроса
        /// </summary>
        public bool isSuccess { get;set; }

        //private readonly ImmutableDictionary<string, string> _noContentMessage = new Dictionary<string, string>()
        //{
        //    //Calendar
        //    {"Put|/characters/{character_id}/calendar/{event_id}/", "Event updated"},

        //    //Contacts
        //    {"Put|/characters/{character_id}/contacts/", "Contacts updated"},
        //    {"Delete|/characters/{character_id}/contacts/", "Contacts deleted"},

        //    //Corporations
        //    {"Put|/corporations/{corporation_id}/structures/{structure_id}/", "Structure vulnerability window updated"},

        //    //Fittings
        //    {"Delete|/characters/{character_id}/fittings/{fitting_id}/", ""},

        //    //Fleets
        //    {"Put|/fleets/{fleet_id}/", "Fleet updated"},
        //    {"Post|/fleets/{fleet_id}/members/", "Fleet invitation sent"},
        //    {"Delete|/fleets/{fleet_id}/members/{member_id}/", "Fleet member kicked"},
        //    {"Put|/fleets/{fleet_id}/members/{member_id}/", "Fleet invitation sent"},
        //    {"Delete|/fleets/{fleet_id}/wings/{wing_id}/", "Wing deleted"},
        //    {"Put|/fleets/{fleet_id}/wings/{wing_id}/", "Wing renamed"},
        //    {"Delete|/fleets/{fleet_id}/squads/{squad_id}/", "Squad deleted"},
        //    {"Put|/fleets/{fleet_id}/squads/{squad_id}/", "Squad renamed"},

        //    //Mail
        //    {"Post|/characters/{character_id}/mail/", "Mail created"},
        //    {"Post|/characters/{character_id}/mail/labels/", "Label created"},
        //    {"Delete|/characters/{character_id}/mail/labels/{label_id}/", "Label deleted"},
        //    {"Put|/characters/{character_id}/mail/{mail_id}/", "Mail updated"},
        //    {"Delete|/characters/{character_id}/mail/{mail_id}/", "Mail deleted"},

        //    //User Interface
        //    {"Post|/ui/openwindow/marketdetails/", "Open window request received"},
        //    {"Post|/ui/openwindow/contract/", "Open window request received"},
        //    {"Post|/ui/openwindow/information/", "Open window request received"},
        //    {"Post|/ui/autopilot/waypoint/", "Open window request received"},
        //    {"Post|/ui/openwindow/newmail/", "Open window request received"}
        //}.ToImmutableDictionary();
    }

    public static class EsiResponseStatic
    {
        public static string parseHeaderValue(this HttpResponseMessage response, string key)
        {
            if (response.Headers.Contains(key))
               return response.Headers.GetValues(key).First();
            return null;
        }
    }
}