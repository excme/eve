using System.Net;
using System.Threading.Tasks;
using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Repo.PublicReadOnly.Models;
using DevExtreme.AspNet.Data.ResponseModel;
using eveDirect.Api.Public.Areas.Alliance.Data;

namespace eveDirect.Api.Public.Areas.Alliance.Controllers
{
    [Route(ApiRoutes.Alliance.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("alliances")]
    [ApiExplorerSettings(GroupName = "Alliances")]
    public class PublicController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public PublicController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }

        ///// <summary>
        ///// Запрос публичной инфомации по корпорации
        ///// </summary>
        //[HttpPost(ApiRoutes.Corporation.Infos)]
        //public async Task<IActionResult> PublicInfoAsync(int[] corporations_ids)
        //{
        //    // Проверка на наличие персонажа
        //    List<CorporationPublicModel> corporation_publicInfo = await _repoPublic.Corporation_PublicInfos(corporations_ids);
        //    return Ok(corporation_publicInfo);
        //}

        //[HttpGet(ApiRoutes.Corporation.Info)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> PublicInfoAsync(int corporation_id)
        //{
        //    // Проверка на наличие корпорации
        //    CorporationPublicModel corporation_publicInfo = await _repoPublic.Corporation_PublicInfo(corporation_id);
        //    if (corporation_publicInfo == null)
        //        return NotFound();

        //    return Ok(corporation_publicInfo);
        //}

        [HttpPost(ApiRoutes.Alliance.Names)]
        public async Task<IActionResult> NameAsync([FromBody] int[] alliance_ids)
        {
            var alliance_names = await _repoPublic.Alliance_Names(alliance_ids);
            return Ok(alliance_names);
        }

        [HttpGet(ApiRoutes.Alliance.Name)]
        public async Task<IActionResult> NameAsync(int i)
        {
            // Проверка на наличие корпорации
            var alliance_name = await _repoPublic.Alliance_Names(i);

            return Ok(alliance_name);
        }

        //[HttpGet(ApiRoutes.Corporation.MembersMigrations)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> MembersMigrations(int corporation_id, int page = 0)
        //{
        //    var characters_migrations = await _repoPublic.Corporation_MembersMigrations(corporation_id, page);
        //    if (characters_migrations == null)
        //        return NotFound();

        //    return Ok(characters_migrations);
        //}

        //[HttpGet(ApiRoutes.Corporation.AllianceHistory)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> AllianceHistory(int corporation_id)
        //{
        //    List<CorporationAllianceHistory.AllianceHistoryItem> alliances_history = await _repoPublic.Corporation_AllianceHistory(corporation_id);
        //    if (alliances_history == null)
        //        return NotFound();

        //    return Ok(alliances_history);
        //}

        [HttpGet(ApiRoutes.Alliance.Preview)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Preview(int i)
        {
            AlliancePreview alliance_preview = await _repoPublic.Alliance_Preview(i);
            if (alliance_preview == null)
                return NotFound();
            return Ok(alliance_preview);
        }

        [HttpPost(ApiRoutes.Alliance.CurrentCharacters)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<LoadResult> CurrentCharacters([FromBody] AllianceCurrentCharactersRequest request)
        {
            return await _repoPublic.Alliance_CurrentCharacters(request.id, request.lo);
        }
    }
}