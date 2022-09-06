using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;

namespace eveDirect.Api.Private.Areas.Contracts.Controllers
{
    [Route("contracts")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("contracts")]
    [ApiExplorerSettings(GroupName = "Contracts")]
    public class PrivateController : ControllerBase
    {
        IReadOnly RepoPublic { get; }
        public PrivateController(IReadOnly repoPublic)
        {
            RepoPublic = repoPublic;
        }

        /// <summary>
        /// Диапозоны contract_ids
        /// </summary>
        [HttpGet("ir")]
        public async Task<IActionResult> IdRangesAsync()
        {
            return Ok(await RepoPublic.Contracts_IdRanges());
        }
    }
}