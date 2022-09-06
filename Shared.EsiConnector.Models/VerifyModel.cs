using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    public class VerifyModel
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public string CharacterOwnerHash { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string IntellectualProperty { get; set; }
        public List<string> Scopes { get; set; }
        public string TokenType { get; set; }
    }
}
