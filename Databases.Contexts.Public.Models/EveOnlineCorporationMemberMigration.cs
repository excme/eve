using eveDirect.Shared.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class EveOnlineCorporationMemberMigrationItem
    {
        [JsonPropertyName("r")]
        public int record_id { get; set; }
        [JsonPropertyName("t")]
        public EMemberMigrationType migrationType { get; set; }
    }
    public enum EMemberMigrationType : byte
    {
        joined = 0,
        unjoin = 1
    }
}
