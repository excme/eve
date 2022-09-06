using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /corporations/{corporation_id}/members/titles/
    /// </summary>
    public class CorporationMembersTitlesResult:List<CorporationMembersTitlesResult.CorporationMembersTitlesItem>
    {
        public class CorporationMembersTitlesItem
        {
            public int character_id { get; set; }
            public List<int> titles { get; set; }
        }
    }
}
