using eveDirect.Shared.EsiConnector.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineWarAlly : WarInfoResult.Ally/*, IUpdateProperties<WarInfoResult.Ally>*/
    {
        [Key]
        public int id { get; set; }

        public DateTime? excluded { get; set; }

        public int warId { get; set; }
        public virtual EveOnlineWar war { get; set; }
    }
}