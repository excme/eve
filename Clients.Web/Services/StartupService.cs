using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using eveDirect.Repo.PublicReadOnly;

namespace eveDirect.Clients.Web.Services
{
    public class StartupService : IHostedService
    {
        ICheckExistService CheckExistService { get; }
        IWebHostEnvironment _environment { get; }

        public StartupService(
            IConfiguration configuration, 
            ICheckExistService checkExistService, 
            IWebHostEnvironment env) {
            CheckExistService = checkExistService;
            _environment = env;
        }

        /// <summary>
        /// Выполнение метода при запуске.
        /// </summary>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //// Загрузка кэшированных диапозонов ид
            if (_environment.IsProduction())
            {
                //// Персонажи
                //await CheckExistService.CharactersIdsRanges_Update();

                //// Корпорации
                //await CheckExistService.CorporationsIdsRanges_Update();

                //// Альянсы
                //await CheckExistService.AlliancesIdsRanges_Update();

                //// Маркет. Контракты
                //await CheckExistService.ContractsIdsRanges_Update();

                // Маркет. Ордера
                // Причина отмены - Ордеров добавляется 300к в день. В месяц 9М. Нет смысла хранить в операвке, они не актуальны после деактивации.
                //await CheckExistService.OrdersIdsRanges_Update();
            }
        }

        //private void UploadFile(string lang, string text)
        //{
        //    using (StreamWriter sw = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\l", $"{lang}.json"), false))
        //    {
        //        sw.Write(text);
        //        sw.Flush();
        //        sw.Close();
        //    }
        //}

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
