using eveDirect.Api.Public.Services;
using eveDirect.Shared.EventBus.Abstractions;
using eveDirect.Translation.DbContext.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Threading.Tasks;

namespace eveDirect.Api.Public.IntegrationEvents
{
    public class LanguageAfterUpdatedVersionIntegrationEventHandler : IIntegrationEventHandler<LanguageAfterUpdatedVersionIntegrationEvent>
    {
        ILogger<LanguageAfterUpdatedVersionIntegrationEventHandler> _logger { get; }
        ILanguageService _languageService { get; }

        public LanguageAfterUpdatedVersionIntegrationEventHandler(ILogger<LanguageAfterUpdatedVersionIntegrationEventHandler> logger, ILanguageService languageService)
        {
            _logger = logger;
            _languageService = languageService;
        }
        public Task Handle(LanguageAfterUpdatedVersionIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation($"Обработка события {@event}: {@event.Lang}");

                _languageService.UpdateLangVersion(@event.Lang);
            }

            return Task.CompletedTask;
        }
    }
}
