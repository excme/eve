using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using Serilog.Context;
using System.Threading.Tasks;
using eveDirect.Shared.EventBus.Abstractions;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    /// <summary>
    /// Новая структуа. Обработка события
    /// </summary>
    public class UniverseStructureAfterAddIntegrationEventHandler : _DefaultEventHandler<UniverseStructureAfterAddIntegrationEventHandler>, IIntegrationEventHandler<UniverseStructureAfterAddIntergrationEvent>
    {
        public UniverseStructureAfterAddIntegrationEventHandler(
            IReadWrite repoPublicCommon,
            //DbContextOptions<JobsContext> options,
            IEventBus eventBus,
            ILoggerFactory logFactory
            ) : base(repoPublicCommon/*, options*/, logFactory, eventBus) { }

        public Task Handle(UniverseStructureAfterAddIntergrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation($"Обработка события: {@event}-{@event.Structure_id}");

                if (@event.Structure_id > 0)
                {
                    // Обновление публичной информации альянса
                    //var structurePublicInformation = new AlliancesPublicInformation(_repoPublicCommon, _options, _logFactory.CreateLogger<AlliancesPublicInformation>());
                    //await structurePublicInformation.SimpleAlliance(@event.Structure_id);
                }

                return Task.CompletedTask;
            }
        }
    }
}
