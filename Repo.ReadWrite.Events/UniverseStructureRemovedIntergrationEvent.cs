
namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Удаление структуры по Ид
    /// </summary>
    public class UniverseStructureRemovedIntergrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public long Structure_id { get; set; }
        public UniverseStructureRemovedIntergrationEvent()
        {

        }
        public UniverseStructureRemovedIntergrationEvent(long structure_id)
        {
            Structure_id = structure_id;
        }
    }
}
