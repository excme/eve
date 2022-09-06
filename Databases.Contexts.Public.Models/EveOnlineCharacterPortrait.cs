using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineCharacterPortrait
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int character_id { get; set; }
    }
}
