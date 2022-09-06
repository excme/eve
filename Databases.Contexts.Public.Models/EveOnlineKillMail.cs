using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineKillMail : KillMailInfoResult
    {
        // Формула расчета hash - https://forums-archive.eveonline.com/message/4900335/#post4900335
        [Key]
        public new int killmail_id { get; set; }
        public string killmail_hash { get; set; }
        public new DateTime? killmail_time { get; set;}
        public new int? solar_system_id { get; set; }

        public double total_destroyed { get; set; }
        public double total_dropped { get; set; }
        public double fitting { get; set; }

        public new virtual List<EveOnlineKillMailAttacker> attackers { get; set; }

        #region victim

        /// <summary>
        /// Жертва убийства
        /// </summary>
        public virtual new EveOnlineKillMailVictim victim { get; set; }

        #endregion

        public virtual EveOnlineKillmailPreview preview { get; set; }
    }
    public class EveOnlineKillmailPreview
    {

    }
    public class EveOnlineKillmailActioner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int? character_id { get; set; }
        public int? corporation_id { get; set; }
        public int? alliance_id { get; set; }
        public int ship_type_id { get; set; }
        public int? faction_id { get; set; }

        public virtual EveOnlineKillMail killmail { get; set; } 
        public int killmailId { get; set; }
        public EEveOnlineKillmailActionerType d { get; set; }
    }
    public enum EEveOnlineKillmailActionerType : byte
    {
        Attacker = 0,
        Victim = 1,
        Base = 3
    }
    public class EveOnlineKillMailAttacker : EveOnlineKillmailActioner //: KillMailInfoResult.Attacker
    {
        public EveOnlineKillMailAttacker() { }
        public EveOnlineKillMailAttacker(KillMailInfoResult.Attacker attacker)
        {
            security_status = attacker.security_status;
            final_blow = attacker.final_blow;
            damage_done = attacker.damage_done;
            character_id = attacker.character_id;
            corporation_id = attacker.corporation_id;
            alliance_id = attacker.alliance_id;
            faction_id = attacker.faction_id;
            ship_type_id = attacker.ship_type_id ?? 0;
            weapon_type_id = attacker.weapon_type_id;
        }

        public float security_status { get; set; }
        public bool final_blow { get; set; }
        public int damage_done { get; set; }
        public int? weapon_type_id { get; set; }
    }
    public class EveOnlineKillMailVictim : EveOnlineKillmailActioner //: KillMailInfoResult.Victim
    {
        public EveOnlineKillMailVictim()
        {

        }
        public EveOnlineKillMailVictim(KillMailInfoResult.Victim data)
        {
            damage_taken = data.damage_taken;
            ship_type_id = data.ship_type_id;
            character_id = data.character_id;
            corporation_id = data.corporation_id;
            alliance_id = data.alliance_id;
            faction_id = data.faction_id;
            items = new List<EveOnlineKillMailVictimItemParent>();
            items = data.items.Select(x =>
            {
                return new EveOnlineKillMailVictimItemParent(x);
            }).ToList();
        }

        public int damage_taken { get; set; }

        /// <summary>
        /// Позиция убийства в системе
        /// </summary>
        public Position position
        {
            get
            {
                return new Position() { x = px, y = py, z = pz };
            }
            set
            {
                px = value.x;
                py = value.y;
                pz = value.z;
            }
        }

        public float px { get; set; }
        public float py { get; set; }
        public float pz { get; set; }

        /// <summary>
        /// Место убийства
        /// </summary>
        public long? location_id { get; set; }

        public virtual List<EveOnlineKillMailVictimItemParent> items { get; set; }

    }
    public class EveOnlineKillMailVictimItemParent : KillMailInfoResult.Item
    {
        public EveOnlineKillMailVictimItemParent()
        {

        }
        public EveOnlineKillMailVictimItemParent(KillMailInfoResult.Item item)
        {
            item_type_id = item.item_type_id;
            singleton = item.singleton;
            flag = item.flag;
            quantity_destroyed = item.quantity_destroyed;
            quantity_dropped = item.quantity_dropped;
            items = item.items.Select(x => {
                return new EveOnlineKillMailVictimItemChild(x);
            }).ToList();
        }
        [JsonPropertyName("i")]
        public new IList<EveOnlineKillMailVictimItemChild> items { get; set; }
        /// <summary>
        /// Рыночная цена за item
        /// </summary>
        [JsonPropertyName("m")]
        public double market_price { get; set; }
        [JsonPropertyName("t")]
        public new int item_type_id { get; set; }
        [JsonPropertyName("s")]
        public new int singleton { get; set; }
        [JsonPropertyName("f")]
        public new int flag { get; set; }
        [JsonPropertyName("q")]
        public new long quantity_destroyed { get; set; }
        [JsonPropertyName("d")]
        public new long quantity_dropped { get; set; }
        [JsonPropertyName("p")]
        public double item_price { get; set; }
        public double CalcSumDropped()
        {
            double temp = 0;
            if (items?.Any() ?? false)
                temp += items.Sum(x => x.quantity_dropped * x.market_price);
            temp += market_price * quantity_dropped;
            return temp;
        }
        public double CalcSumDestoryed()
        {
            double temp = 0;
            if (items?.Any() ?? false)
                temp += items.Sum(x => x.quantity_destroyed * x.market_price);
            temp += market_price * quantity_destroyed;
            return temp;
        }
        public double CalcSumFitting()
        {
            double temp = 0;
            if (items?.Any() ?? false)
            {
                var filter = items.Where(x => FittingRange.ForKillmail(x.flag)).ToList();
                if(filter.Any())
                    temp += filter.Sum(x => x.quantity_destroyed * x.market_price + x.quantity_dropped * x.market_price);
            }

            if(FittingRange.ForKillmail(flag))
                temp += market_price * quantity_destroyed + market_price * quantity_dropped;
            return temp;
        }
    }
    public class EveOnlineKillMailVictimItemChild : KillMailInfoResult.InnerItem
    {
        public EveOnlineKillMailVictimItemChild()
        {

        }
        public EveOnlineKillMailVictimItemChild(KillMailInfoResult.InnerItem item)
        {
            item_type_id = item.item_type_id;
            singleton = item.singleton;
            flag = item.flag;
            quantity_destroyed = item.quantity_destroyed;
            quantity_dropped = item.quantity_dropped;
        }
        /// <summary>
        /// Рыночная цена за item
        /// </summary>
        [JsonPropertyName("m")]
        public double market_price { get; set; }
        [JsonPropertyName("t")]
        public new int item_type_id { get; set; }
        [JsonPropertyName("s")]
        public new int singleton { get; set; }
        [JsonPropertyName("f")]
        public new int flag { get; set; }
        [JsonPropertyName("q")]
        public new long quantity_destroyed { get; set; }
        [JsonPropertyName("d")]
        public new long quantity_dropped { get; set; }
    }
}