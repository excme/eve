using System.Threading.Tasks;
using eveDirect.Repo.PublicReadOnly;
using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Api.Private.Areas.Alliances.Controllers
{
    [Route("allies")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("alliances")]
    [ApiExplorerSettings(GroupName = "Alliances")]
    public class PrivateController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public PrivateController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }
        /// <summary>
        /// Диапозоны alliances_ids
        /// </summary>
        [HttpGet("ir")]
        public async Task<IActionResult> IdRangesAsync()
        {
            return Ok(await _repoPublic.Alliances_IdRanges());
        }
    }
}