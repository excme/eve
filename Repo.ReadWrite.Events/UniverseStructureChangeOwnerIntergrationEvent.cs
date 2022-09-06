

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Изменение владельца структуры по Ид
    /// </summary>
    public class UniverseStructureChangeOwnerIntergrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public long Structure_id { get; set; }
        public UniverseStructureChangeOwnerIntergrationEvent()
        {

        }
        public UniverseStructureChangeOwnerIntergrationEvent(long structure_id)
        {
            Structure_id = structure_id;
        }
    }
}
