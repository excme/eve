

namespace eveDirect.Repo.ReadWrite.IntegrationEvents
{
    /// <summary>
    /// Добавление структуры по Ид
    /// </summary>
    public class UniverseStructureAfterAddIntergrationEvent : Shared.EventBus.Events.IntegrationEvent
    {
        public long Structure_id { get; set; }
        public UniverseStructureAfterAddIntergrationEvent()
        {

        }
        public UniverseStructureAfterAddIntergrationEvent(long structure_id)
        {
            Structure_id = structure_id;
        }
    }
}
