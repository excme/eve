using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class ZKillBoardStatItem
    {
        [Key]
        public string OnDate { get; set; }
        public int zKillBoard_Count { get; set; }
        //public int Local_Count { get; set; }
    }
}
