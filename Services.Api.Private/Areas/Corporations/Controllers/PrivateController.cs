using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Repo.PublicReadOnly.Models;

namespace eveDirect.Api.Private.Areas.Corporations.Controllers
{
    [Route("corps")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("corporations")]
    [ApiExplorerSettings(GroupName = "Corporations")]
    public class PrivateController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public PrivateController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }
        /// <summary>
        /// Диапозоны corporation_ids
        /// </summary>
        [HttpGet("ir")]
        public async Task<IActionResult> IdRangesAsync()
        {
            return Ok(await _repoPublic.Corporation_IdRanges());
        }
    }
}