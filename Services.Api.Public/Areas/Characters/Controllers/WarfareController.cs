using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;

namespace eveDirect.Api.Public.Areas.Characters.Controllers
{
    [Route(ApiRoutes.Character.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("characters")]
    [ApiExplorerSettings(GroupName = "Characters")]
    public class WarfareController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public WarfareController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }

        [HttpGet(ApiRoutes.Character.Wars)]
        public IActionResult Wars(int corporation_id)
        {
            return Ok();
        }

        [HttpGet(ApiRoutes.Character.Kills)]
        public IActionResult Kills(int corporation_id)
        {
            return Ok();
        }
    }
}