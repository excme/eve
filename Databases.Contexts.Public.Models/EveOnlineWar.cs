using eveDirect.Shared.EsiConnector.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineWar : WarInfoResult
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new int war_id { get; set; }

        [NotMapped]
        public new Participant aggressor
        {
            get
            {
                return new Participant()
                {
                    ships_killed = aggressor_ships_killed,
                    isk_destroyed = aggressor_isk_destroyed,
                    alliance_id = aggressor_alliance_id,
                    corporation_id = aggressor_corporation_id
                };
            }
            set
            {
                aggressor_ships_killed = value.ships_killed;
                aggressor_isk_destroyed = value.isk_destroyed;
                aggressor_alliance_id = value.alliance_id;
                aggressor_corporation_id = value.corporation_id;
            }
        }

        public int aggressor_ships_killed { get; set; }
        public float aggressor_isk_destroyed { get; set; }
        public int? aggressor_alliance_id { get; set; }
        public int? aggressor_corporation_id { get; set; }

        [NotMapped]
        public new Participant defender
        {
            get
            {
                return new Participant()
                {
                    ships_killed = defender_ships_killed,
                    isk_destroyed = defender_isk_destroyed,
                    alliance_id = defender_alliance_id,
                    corporation_id = defender_corporation_id
                };
            }
            set
            {
                defender_ships_killed = value.ships_killed;
                defender_isk_destroyed = value.isk_destroyed;
                defender_alliance_id = value.alliance_id;
                defender_corporation_id = value.corporation_id;
            }
        }

        public int defender_ships_killed { get; set; }
        public float defender_isk_destroyed { get; set; }
        public int? defender_alliance_id { get; set; }
        public int? defender_corporation_id { get; set; }

        public new virtual List<EveOnlineWarAlly> allies { get; set; }

        public bool killmail_loaded { get; set; }

        public EveOnlineWar()
        {
            aggressor = new Participant();
            defender = new Participant();
            allies = new List<EveOnlineWarAlly>();
        }
    }
}