using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;

namespace eveDirect.Api.Public.Areas.Corporations.Controllers
{
    [Route(ApiRoutes.Corporation.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("corporations")]
    [ApiExplorerSettings(GroupName = "Corporations")]
    public class MarketController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public MarketController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }

        [HttpGet(ApiRoutes.Corporation.Contracts)]
        public IActionResult Contracts(int corporation_id)
        {
            return Ok();
        }

        [HttpGet(ApiRoutes.Corporation.ContractsHistory)]
        public IActionResult ContractsHistory(int corporation_id)
        {
            return Ok();
        }
    }
}