using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using System.Threading.Tasks;
using eveDirect.Api.Public.Areas.Market.Data;

namespace eveDirect.Api.Public.Areas.Characters.Controllers
{
    [Route(ApiRoutes.Character.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("characters")]
    [ApiExplorerSettings(GroupName = "Characters")]
    public class MarketController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public MarketController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
            // Только контракты, потому что ордера не имеют привязки к персонажу
        }

        [HttpPost(ApiRoutes.Character.Contracts_V1)]
        public async Task<LoadResult> Contracts([FromBody] CharacterMarketContractRequest request)
        {
            return await _repoPublic.Character_MarketContracts(request.id, request.lo);
        }
    }
}