using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Services.Jobs.Core;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EsiConnector;
using eveDirect.Shared.EsiConnector.Enumerations;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;

namespace eveDirect.Services.EsiConnector
{
    public class ConnectorJob : JobBase
    {
        private readonly string _secret;
        private readonly string _client_id;

        private readonly string _proxtAddr;
        private readonly string _proxyPort;
        private readonly string _proxyUser;
        private readonly string _proxyPass;

        public override void Dispose()
        {
            _repoPublicCommon = null;
        }

        public ConnectorJob() : base(null) { }
        /// <summary>
        /// Для запросов без авторизации с прокси
        /// </summary>
        public ConnectorJob(ILogger logger, string addr, string port, string user, string pass) : base(logger)
        {
            _proxtAddr = addr ?? "";
            _proxyPort = port ?? "";
            _proxyUser = user?.Length > 0 ? user+random.Next(1, 11) : "";
            _proxyPass = pass ?? "";

            esiClient = newEsiClient();
        }
        /// <summary>
        /// Для запросов без авторизации
        /// </summary>
        public ConnectorJob(ILogger logger) : base(logger)
        {
            esiClient = newEsiClient();
        }
        /// <summary>
        /// Для запросов с авторизацией
        /// </summary>
        public ConnectorJob(ILogger logger, string clientId, string secret) : base(logger)
        {
            _client_id = clientId ?? "";
            _secret = secret ?? "";
            esiClient = newEsiClient();
        }

        protected IReadWrite _repoPublicCommon { get; set; }
        //protected DbContextOptions<JobsContext> _jobsContextOptions { get; set; }

        /// <summary>
        /// Необходимые роли в корпорации у персонажа для выполнения запросов к esi
        /// </summary>
        protected string[] Needed_roles_in_corporation { get; set; }
        /// <summary>
        /// Необходимый sso scope для auth-запросов
        /// </summary>
        protected string Requires_scope { get; set; }
        /// <summary>
        /// Необходим Sso
        /// </summary>
        protected bool With_sso()
        {
            return Requires_scope.Length > 0;
        }

        public EsiClient newEsiClient(AuthorizedCharacterData data = null)
        {
            var client =  new EsiClient(esiClientConfig(), _logger);
            if (data != null)
            {
                if (data.Expired())
                {
                    var token = client.SSO.GetToken(GrantType.RefreshToken, data.RefreshToken);
                    data = client.SSO.Verify(token);

                    // Обновление access_token в базе
                    _repoPublicCommon.Sso_UpdateAccessToken(data);
                }
                client.SetCharacterData(data);
            }
            return client;
        }

        IOptions<EsiConfig> esiClientConfig()
        {
            return Options.Create(new EsiConfig()
            {
                EsiUrl = "https://esi.evetech.net/",
                DataSource = DataSource.tranquility,
                ClientId = _client_id,
                SecretKey = _secret,
                CallbackUrl = "",

                ProxyAddr = _proxtAddr,
                ProxyPort = _proxyPort,
                ProxyUser = _proxyUser,
                ProxyPass = _proxyPass,
            });
        }

        /// <summary>
        /// Элемент на запрос
        /// </summary>
        protected int Item_To_Request { get; set; }
        /// <summary>
        /// Выполненные запросы Esi. Собираются для того, чтобы создать отчет в БД
        /// </summary>
        protected List<IdentitySsoRequest> Maked_Esi_Requests { get; set; } = new List<IdentitySsoRequest>();

        /// <summary>
        /// Настройки проекта
        /// </summary>
        //IConfiguration _config { get; set; }

        protected EsiClient esiClient { get; set; }

        #region Коннектор
        protected RequestResult<T> EsiConnector<T>(Func<EsiResponse<T>> esi_request)
            where T : ISsoResult
        {
            var result_request = esi_request();
            return EsiConnector_ProcessingResult(result_request);
        }
        protected RequestResult<T> EsiConnector<T>(Func<ELanguages, EsiResponse<T>> esi_request,
            ELanguages lang = ELanguages.en_us)
            where T : ISsoResult
        {
            var result_request = esi_request(lang);
            return EsiConnector_ProcessingResult(result_request);
        }
        protected RequestResult<T> EsiConnector<T>(Func<int, EsiResponse<T>> esi_request, 
            int arg1)
            where T : ISsoResult
        {
            EsiResponse<T> result_request = esi_request(arg1);
            return EsiConnector_ProcessingResult(result_request);
        }
        protected RequestResult<T> EsiConnector<T>(Func<long, EsiResponse<T>> esi_request,
            long arg1)
            where T : ISsoResult
        {
            EsiResponse<T> result_request = esi_request(arg1);
            return EsiConnector_ProcessingResult(result_request);
        }
        protected RequestResult<T> EsiConnector<T>(Func<int,int, EsiResponse<T>> esi_request,
            int arg1,
            int arg2)
            where T : ISsoResult
        {
            EsiResponse<T> result_request = esi_request(arg1,arg2);
            return EsiConnector_ProcessingResult(result_request);
        }
        protected RequestResult<T> EsiConnector<T>(Func<int, ELanguages, EsiResponse<T>> esi_request,
            int arg1,
            ELanguages lang = ELanguages.en_us)
            where T : ISsoResult
        {
            EsiResponse<T> result_request = esi_request(arg1, lang);
            return EsiConnector_ProcessingResult(result_request);
        }
        protected RequestResult<T> EsiConnector<T>(
            Func<List<int>, EsiResponse<T>> esi_request,
            List<int> arg1)
            where T : ISsoResult
        {
            EsiResponse<T> result_request = esi_request(arg1);
            var to_return = EsiConnector_ProcessingResult(result_request);
            return to_return;
        }
        protected RequestResult<T> EsiConnector<T>(Func<int, string, EsiResponse<T>> esi_request,
            int arg1,
            string arg2)
            where T : ISsoResult
        {
            EsiResponse<T> result_request = esi_request(arg1, arg2);
            return EsiConnector_ProcessingResult(result_request);
        }
        /// <summary>
        /// Автоматическое листание без дополнительных аргументов
        /// </summary>
        protected List<RequestResult<T>> EsiConnector_AutoPaging<T>(Func<int, EsiResponse<T>> esi_request)
            where T : ISsoResult
        {
            var result_requests = EsiConnectorGeneric.Auto_Paging(esi_request);
            return EsiConnector_ProcessingResult_AutoPaging(result_requests);
        }
        /// <summary>
        /// Автоматическое листание с 1 дополнительным аргументом
        /// </summary>
        protected List<RequestResult<T>> EsiConnector_AutoPaging<T, T0>(
            Func<T0, int, EsiResponse<T>> esi_request, 
            T0 arg1)
            where T : ISsoResult
        {
            var result_requests = EsiConnectorGeneric.Auto_Paging(esi_request, arg1); 
            return EsiConnector_ProcessingResult_AutoPaging(result_requests);
        }
        protected List<RequestResult<T>> EsiConnector_AutoPaging<T, T0, T1, T2>(
            Func<T0, T1, T2, int, EsiResponse<T>> esi_request,
            T0 arg1,
            T1 arg2,
            T2 arg3)
            where T : ISsoResult
        {
            var result_requests = EsiConnectorGeneric.Auto_Paging(esi_request, arg1, arg2, arg3);
            return EsiConnector_ProcessingResult_AutoPaging(result_requests);
        }
        internal List<RequestResult<T>> EsiConnector_ProcessingResult_AutoPaging<T>(List<EsiResponse<T>> esi_results, int arg1 = -255)
            where T : ISsoResult
        {
            List<RequestResult<T>> results = new List<RequestResult<T>>();
            foreach (var esi_result in esi_results)
            {
                // TODO: Обработка результата запросов. Обработка исключений и ошибок. Отправление статистики
                //Sso_RequestStatistic(arg1, SsoRequestType, esi_result.isSuccess ? Get_CountItemsOfObject(esi_result.Data) : 0);

                results.Add(
                    new RequestResult<T>(esi_result.isSuccess,
                    esi_result.StatusCode,
                    esi_result.Data, 
                    esi_result.LastModified,
                    esi_result.Date)
                );
            }
            return results;
        }
        internal RequestResult<T> EsiConnector_ProcessingResult<T>(
            EsiResponse<T> esi_result, int arg1 = -255)
            where T : ISsoResult
        {

            // TODO: Обработка результата запросов. Обработка исключений и ошибок. Отправление статистики
            //Sso_RequestStatistic(arg1, SsoRequestType, esi_result.isSuccess ? Get_CountItemsOfObject(esi_result.Data) : 0);

            return new RequestResult<T>(esi_result.isSuccess,
                    esi_result.StatusCode,
                    esi_result.Data,
                    esi_result.LastModified,
                    esi_result.Date);
        }
        private int Get_CountItemsOfObject<T>(T value)
        {
            if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                var property = typeof(List<>).GetProperty("Count");
                int count = (int)property.GetValue(value, null);
                return count;
            }
            return 1;
        }
        #endregion
    }
    public class RequestResult<T>
        where T : ISsoResult
    {
        public RequestResult(bool success, HttpStatusCode statusCode, T data, DateTime? last_modified, DateTime? date)
        {
            isSuccess = success;
            Data = data;
            StatusCode = statusCode;
            Last_Modified = last_modified;
            Date = date;
        }

        public bool isSuccess { get; set; }
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public DateTime? Last_Modified { get; set; }
        public DateTime? Date { get; set; }

        public dynamic Request { get; set; }
    }
}
