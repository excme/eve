using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Repo.PublicReadOnly.Models;

namespace eveDirect.Api.Public.Areas.Corporations.Controllers
{
    [Route(ApiRoutes.Corporation.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("corporations")]
    [ApiExplorerSettings(GroupName = "Corporations")]
    public class PublicController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public PublicController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }

        /// <summary>
        /// Запрос публичной инфомации по корпорации
        /// </summary>
        [HttpPost(ApiRoutes.Corporation.Infos)]
        public async Task<IActionResult> PublicInfoAsync(int[] corporations_ids)
        {
            // Проверка на наличие персонажа
            List<CorporationPublicModel> corporation_publicInfo = await _repoPublic.Corporation_PublicInfos(corporations_ids);
            return Ok(corporation_publicInfo);
        }

        [HttpGet(ApiRoutes.Corporation.Info)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PublicInfoAsync(int corporation_id)
        {
            // Проверка на наличие корпорации
            CorporationPublicModel corporation_publicInfo = await _repoPublic.Corporation_PublicInfo(corporation_id);
            if (corporation_publicInfo == null)
                return NotFound();

            return Ok(corporation_publicInfo);
        }

        [HttpPost(ApiRoutes.Corporation.Names)]
        public async Task<IActionResult> NameAsync([FromBody] int[] corporations_ids)
        {
            var corporation_names = await _repoPublic.Corporation_Names(corporations_ids);
            return Ok(corporation_names);
        }

        [HttpGet(ApiRoutes.Corporation.Name)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> NameAsync(int i)
        {
            // Проверка на наличие корпорации
            var corporation_name = await _repoPublic.Corporation_Names(i);
            //if (corporation_name == null)
            //    return NotFound();

            return Ok(corporation_name);
        }

        [HttpGet(ApiRoutes.Corporation.MembersMigrations)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> MembersMigrations(int corporation_id, int page = 0)
        {
            var characters_migrations = await _repoPublic.Corporation_MembersMigrations(corporation_id, page);
            if (characters_migrations == null)
                return NotFound();

            return Ok(characters_migrations);
        }

        [HttpGet(ApiRoutes.Corporation.AllianceHistory)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AllianceHistory(int corporation_id)
        {
            List<CorporationAllianceHistory.AllianceHistoryItem> alliances_history = await _repoPublic.Corporation_AllianceHistory(corporation_id);
            if (alliances_history == null)
                return NotFound();

            return Ok(alliances_history);
        }

        [HttpGet(ApiRoutes.Corporation.Preview)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Preview(int corporation_id)
        {
            CorporationPreview corporation_preview = await _repoPublic.Corporation_Preview(corporation_id);
            if (corporation_preview == null)
                return NotFound();
            return Ok(corporation_preview);
        }
    }
}