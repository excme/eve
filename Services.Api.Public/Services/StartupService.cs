using eveDirect.Repo.PublicReadOnly;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace eveDirect.Api.Public.Services
{
    /// <summary>
    /// Загрузка данных при старте сервиса
    /// </summary>
    public class StartupService : IHostedService
    {
        IReadOnly _publicReadOnly { get; }
        ILanguageService _languageService { get; }
        IWebHostEnvironment _environment { get; }

        public StartupService(IReadOnly publicReadOnly, ILanguageService languageService, IWebHostEnvironment environment)
        {
            _publicReadOnly = publicReadOnly;
            _languageService = languageService;
            _environment = environment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Загрзука мультиязычности
            _languageService.UpdateLangVersion("ru");

            if (_environment.IsProduction())
            {
                // Загрзука мультиязычности
                _languageService.UpdateLangVersion("en");

                // Загрузка newborns characters
                await _publicReadOnly.CharacterNewbornItems_Calc();

                // Расчет количества записей миграции персонжей по корпорациям
                //await _publicReadOnly.Characters_MigrationsRoot_UpdateTotalCount();

                _languageService.UpdateLangVersion("de");
                _languageService.UpdateLangVersion("fr");
                _languageService.UpdateLangVersion("ja");
                _languageService.UpdateLangVersion("zh");
                _languageService.UpdateLangVersion("ko");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
