

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Изменение имени структуры по Ид
    /// </summary>
    public class UniverseStructureChangeNameIntergrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public long Structure_id { get; set; }
        public UniverseStructureChangeNameIntergrationEvent()
        {

        }
        public UniverseStructureChangeNameIntergrationEvent(long structure_id)
        {
            Structure_id = structure_id;
        }
    }
}
