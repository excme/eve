using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveDirectCheckPoint
    {
        //public int id { get; set; }
        
        [Key]
        public string checkpointName { get; set; }
        public int value { get; set; }
    }
}
