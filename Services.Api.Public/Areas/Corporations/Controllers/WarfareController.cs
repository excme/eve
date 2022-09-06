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
    public class WarfareController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public WarfareController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }

        [HttpGet(ApiRoutes.Corporation.Wars)]
        public IActionResult Wars(int corporation_id)
        {
            return Ok();
        }

        [HttpGet(ApiRoutes.Corporation.Kills)]
        public IActionResult Kills(int corporation_id)
        {
            return Ok();
        }
    }
}