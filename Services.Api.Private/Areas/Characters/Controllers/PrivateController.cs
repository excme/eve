using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;

namespace eveDirect.Api.Private.Areas.Characters.Controllers
{
    [Route("chars")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("characters")]
    [ApiExplorerSettings(GroupName = "Characters")]
    public class PrivateController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public PrivateController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }
        /// <summary>
        /// Диапозоны character_ids
        /// </summary>
        [HttpGet("ir")]
        public async Task<IActionResult> IdRangesAsync()
        {
            return Ok(await _repoPublic.Character_IdRanges());
        }
    }
}