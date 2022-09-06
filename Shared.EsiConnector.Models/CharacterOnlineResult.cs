using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/online/
    /// </summary>
    public class CharacterOnlineResult
    {
        public bool online { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime last_login { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime last_logout { get; set; }
        public int logins { get; set; }
    }
}
