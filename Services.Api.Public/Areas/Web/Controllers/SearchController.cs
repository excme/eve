using eveDirect.Api.Public.Areas.Web.Models;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eveDirect.Api.Public.Areas.Web.Controllers
{
    [Route(ApiRoutes.Search.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("web")]
    [ApiExplorerSettings(GroupName = "Web")]
    public class SearchController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public SearchController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic ?? throw new System.ArgumentNullException(nameof(repoPublic));
        }

        [HttpPost(ApiRoutes.Search.BySubString)]
        public async Task<IActionResult> ByString([FromBody]SearchReqModel model)
        {
            // Проверка на минимальную длину
            if(model?.str?.Length >= 3)
                return Ok(await _repoPublic.Search_BySubValue(model.str));
            return NotFound();
        }
    }
}
