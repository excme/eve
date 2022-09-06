using Serilog.Context;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using eveDirect.Repo.PublicReadOnly.Models.Events;
using eveDirect.Clients.Web.Services;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Clients.Web.IntegrationEvents
{
    /// <summary>
    /// Обновление локалльного кэша character_ids
    /// </summary>
    public class CharacterIdsRangesUpdatedIntegrationEventHandler : IIntegrationEventHandler<Character_RangeUpdated_IntegrationEvent>
    {
        ICheckExistService CheckExistService { get; }
        private ILogger<CharacterIdsRangesUpdatedIntegrationEventHandler> Logger { get; }

        public CharacterIdsRangesUpdatedIntegrationEventHandler(
            ILogger<CharacterIdsRangesUpdatedIntegrationEventHandler> logger,
            ICheckExistService checkExistService)
        {
            CheckExistService = checkExistService;
            Logger = logger;
        }

        public async Task Handle(Character_RangeUpdated_IntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                Logger.LogInformation($"Обработка события - {@event}");

                await CheckExistService.CharactersIdsRanges_Update();
            }
        }
    }
}
