using Microsoft.Extensions.Logging;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared.EventBus.Abstractions;
using System.Threading.Tasks;

namespace eveDirect.Services.Jobs.EventSubscribers.IntegrationEvents
{
    public class _DefaultEventHandler<TLogCategory>
    {
        protected readonly IReadWrite _repoPublicCommon;
        protected readonly ILogger<TLogCategory> _logger;
        protected readonly ILoggerFactory _logFactory;
        protected readonly IEventBus _eventBus;

        public _DefaultEventHandler(
            IReadWrite repoPublicCommon,
            ILoggerFactory logFactory,
            IEventBus eventBus
            )
        {
            _repoPublicCommon = repoPublicCommon;
            //_options = options;
            _logger = logFactory.CreateLogger<TLogCategory>();
            _logFactory = logFactory;
            _eventBus = eventBus;
        }
    }
}
