using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.Repo.PublicReadOnly;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Api.Private.Areas.Orders.Controllers
{
    [Route("orders")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("orders")]
    [ApiExplorerSettings(GroupName = "Orders")]
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
            return Ok(await RepoPublic.Orders_IdRanges());
        }
    }
}