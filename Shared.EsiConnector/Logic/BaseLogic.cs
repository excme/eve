using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using Microsoft.Extensions.Logging;
using eveDirect.Shared.EsiConnector.Models.SSO;
using static eveDirect.Shared.EsiConnector.EsiRequest;
using static eveDirect.Shared.EsiConnector.EsiErrorsCollect;
using Serilog.Context;
using eveDirect.Shared.Helper;

namespace eveDirect.Shared.EsiConnector.Logic
{
    public class BaseLogic
    {
        protected HttpClient _client { get; }
        protected EsiConfig _config { get; }
        protected ILogger _logger { get; }

        protected AuthorizedCharacterData _data { get; }
        protected int character_id { get; }
        protected int corporation_id { get; }
        protected int alliance_id { get; }

        public BaseLogic(HttpClient client, EsiConfig config, ILogger logger = null, AuthorizedCharacterData data = null) { 
            _client = client; 
            _config = config;
            _logger = logger;

            _data = data;

            if (data != null)
            {
                character_id = data.CharacterID;
                corporation_id = data.CorporationID;
                alliance_id = _data.AllianceID;
            }
        }

        public string ETag { get; set; }

        /// <summary>
        /// Выполнение запроса к esi
        /// </summary>
        protected EsiResponse<T> Execute<T>(
            HttpClient client, 
            EsiConfig config, 
            RequestSecurity security, 
            RequestMethod method, 
            string endpoint, 
            Dictionary<string, string> replacements = null, 
            string[] parameters = null, 
            object body = null, 
            string token = null)
        {
            string url = "";
            client.DefaultRequestHeaders.Clear();

            var path = $"{method}|{endpoint}";
            var version = RequestsInfo.EndpointVersions[path];
            HttpResponseMessage response = null;

            // Настройка прокси соединения
            string proxy_key = config.ProxyUser?.Length > 1 
                ? config?.ProxyUser 
                : "local";
            if (!current_ErrorLimitRemain.ContainsKey(proxy_key))
                current_ErrorLimitRemain.TryAdd(proxy_key, 100);
            if (!current_ErrorLimitReset.ContainsKey(proxy_key))
                current_ErrorLimitReset.TryAdd(proxy_key, 60);

            DateTime itemDt = DateTime.MinValue;
            bool in_paused = paused.TryGetValue(path, out itemDt);
            // Проверка на временную недоступность сервиса
            if (!in_paused || itemDt < DateTime.UtcNow)
            {
                if (in_paused)
                    paused.TryRemove(path, out _);

                if (replacements != null)
                    foreach (var property in replacements)
                        endpoint = endpoint.Replace($"{{{property.Key}}}", property.Value);

                url = $"{config.EsiUrl}{version}{endpoint}?datasource={config.DataSource.ToEsiValue()}";

                //Attach token to request header if this endpoint requires an authorized character
                if (security == RequestSecurity.Authenticated)
                {
                    if (token == null)
                        throw new ArgumentException($"{path}. Запрос требует обязательного наличия SSO аутентификации и токена.");

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                if (ETag != null)
                {
                    client.DefaultRequestHeaders.Add("If-None-Match", $"\"{ETag}\"");
                    ETag = null;
                }

                //Attach query string parameters
                if (parameters != null)
                    url += $"&{string.Join("&", parameters)}";

                //Serialize post body data
                HttpContent postBody = null; string b = "";
                if (body != null)
                {
                    b = JsonSerializer.Serialize(body);
                    postBody = new StringContent(b, Encoding.UTF8, "application/json");
                }

                //Get response from client based on request type
                //This is also where body variables will be created and attached as necessary

                int trying = 0, maxTries = 3;
                do
                {
                    trying++;

                    // Проверка на переполнение ошибок пред. запросами. При необходимости замедляемся
                    if (current_ErrorLimitRemain[proxy_key] <= 10)
                        Thread.Sleep(current_ErrorLimitReset[proxy_key] * 1000);

                    try
                    {
                        switch (method)
                        {
                            case RequestMethod.Delete:
                                response = client.DeleteAsync(url).GetAwaiter().GetResult();
                                break;

                            case RequestMethod.Get:
                                response = client.GetAsync(url).GetAwaiter().GetResult();
                                break;

                            case RequestMethod.Post:
                                response = client.PostAsync(url, postBody).GetAwaiter().GetResult();
                                break;

                            case RequestMethod.Put:
                                response = client.PutAsync(url, postBody).GetAwaiter().GetResult();
                                break;
                        }
                    }
                    catch
                    {
                        trying--;
                        continue;
                    }

                    // Контрольные зн-я по ошибкам
                    current_ErrorLimitRemain[proxy_key] = response.parseHeaderValue("X-Esi-Error-Limit-Remain")?.ToInt32() ?? 0;
                    current_ErrorLimitReset[proxy_key] = response.parseHeaderValue("X-Esi-Error-Limit-Reset")?.ToInt32() ?? 0;

                    // Проверка ответа на возможность исправления ошибки применением паузы и перезапроса
                    // Это 420 - переполнение лимита ошибок на клиент
                    // И >= 500 - ошибки сервера
                    if ((int)response.StatusCode >= 420)
                    {
                        using (LogContext.PushProperty("Endpoint", endpoint))
                        using (LogContext.PushProperty("ErrorLimitRemain", current_ErrorLimitRemain[proxy_key]))
                        using (LogContext.PushProperty("ErrorLimitReset", current_ErrorLimitReset[proxy_key]))
                        {
                            if ((int)response.StatusCode == 420)
                            {
                                _logger?.LogCritical("Last in Out of limit errors");
                            }
                            else
                            {
                                _logger?.LogWarning($"Esi server error {(int)response.StatusCode}");
                            }
                        }

                        // Добавление endpoint в список приостановленных
                        if (paused.ContainsKey(path))
                            paused.TryRemove(path, out _);
                        paused.TryAdd(path, DateTime.UtcNow.AddSeconds(65));

                        // Проверка на переполнение ошибок. 
                        // Если нас ограничили, то ждем время, пока не сбросят ограничение
                        EsiErrors(response, current_ErrorLimitReset[proxy_key]);
                    }
                    else
                    {
                        // Если запрос был выполнен успешно или с ошибкой, то выходим из while
                        break;
                    }
                } while (trying <= maxTries);
            }
            else
            {
                response = new HttpResponseMessage() { StatusCode = HttpStatusCode.ResetContent };
            }

            //Output final object
            EsiResponse<T> esi_responce = new EsiResponse<T>(_logger, response, path, version, endpoint, body: body != null ? JsonSerializer.Serialize(body) : null);
            return esi_responce;
        }

        void EsiErrors(HttpResponseMessage httpResponse, int errorLimitReset)
        {
            // Если переполнение ошибками, то ставим ожидание и перевыполнение
            if ((int)httpResponse.StatusCode == 420)
                Thread.Sleep((errorLimitReset + 1) * 1000);
        }
    }
}
