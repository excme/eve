using System.Threading.Tasks;

namespace eveDirect.Shared.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
