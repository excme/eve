using System.Collections.Generic;

namespace eveDirect.Shared.EsiConnector.Models
{
    /// <summary>
    /// GET /characters/{character_id}/implants/
    /// </summary>
    public class CharacterImplantsResult:List<int>, ISsoResult
    {
    }
}
