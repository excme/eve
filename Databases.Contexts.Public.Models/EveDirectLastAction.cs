using System.ComponentModel.DataAnnotations;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveDirectLastAction
    {
        [Key]
        public long id { get; set; }

        public bool auth { get; set; }

        public int owner_id { get; set; }
        public int parent_id { get; set; }

        public ELastActionType type { get; set; }

        public long ref_id { get; set; }
    }
    public enum ELastActionType : byte
    {
        contract_published = 2,
        contract_disable = 5,

        character_join = 3,
        character_leave = 4,

        character_killmail_victim = 6,
        character_killmail_attacker = 7,
    }
}
