using System;
using System.Collections.Generic;
using System.Linq;

namespace eveDirect.Shared.EsiConnector.Models.SSO
{
    public class AuthorizedCharacterData
    {
        public DateTime ExpiresIn { get; set; }
        public List<string> ScopesList { get; set; }
        public string Scopes { 
            set {
                var t = value.Split(' ');
                ScopesList = t.ToList();
            } 
        }
        public string TokenType { get; set; }
        public string CharacterOwnerHash { get; set; }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int AllianceID { get; set; }
        public int CorporationID { get; set; }
        public int CharacterID { get; set; }
        //public int FactionID { get; set; }
        //public string CharacterName { get; set; }

        public bool Expired()
        {
            return DateTime.UtcNow.AddSeconds(10) > ExpiresIn;
        }
    }
}
