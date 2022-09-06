using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Repo.PublicReadOnly.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace eveDirect.Api.Public.Areas.Characters.Controllers
{
    [Route(ApiRoutes.Character.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("characters")]
    [ApiExplorerSettings(GroupName = "Characters")]
    public class PublicController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public PublicController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }

        /// <summary>
        /// Запрос публичной инфомации по персонажу
        /// </summary>
        [HttpPost(ApiRoutes.Character.Infos)]
        public async Task<IActionResult> PublicInfoAsync([FromBody]int[] characters_ids)
        {
            // Проверка на наличие персонажа
            List<CharacterPublicModel> character_publicInfo = await _repoPublic.Character_PublicInfos(characters_ids);
            return Ok(character_publicInfo);
        }

        [HttpGet(ApiRoutes.Character.Info)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PublicInfoAsync(int character_id)
        {
            // Проверка на наличие персонажа
            CharacterPublicModel character_publicInfo = await _repoPublic.Character_PublicInfo(character_id);
            if (character_publicInfo == null)
                return NotFound();

            return Ok(character_publicInfo);
        }

        [HttpPost(ApiRoutes.Character.Names)]
        public async Task<IActionResult> NameAsync([FromBody] int[] characters_ids)
        {
            List<NameModel<int>> character_names = await _repoPublic.Character_Names(characters_ids);
            return Ok(character_names);
        }

        [HttpGet(ApiRoutes.Character.Name)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> NameAsync(int i)
        {
            // Проверка на наличие персонажа
            return Ok(await _repoPublic.Character_Name(i));
        }

        [HttpGet(ApiRoutes.Character.CorpHistory)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CorporationHistory(int i)
        {
            var CharacterCorporationHistory = await _repoPublic.Character_CorporationHistory(i);
            if (CharacterCorporationHistory == null)
                return NotFound();

            return Ok(CharacterCorporationHistory);
        }

        [HttpPost(ApiRoutes.Character.CorpHistories)]
        public async Task<IActionResult> CorporationHistory([FromBody]int[] characters_ids)
        {
            var CharactersCorporationHistories = await _repoPublic.Characters_CorporationHistory(characters_ids);
            if (CharactersCorporationHistories == null)
                return NotFound();

            return Ok(CharactersCorporationHistories);
        }

        [HttpGet(ApiRoutes.Character.AllyHistory)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> AllianceHistory(int i)
        {
            List<CharacterAllianceHistoryModel.AllianceHistoryItem> CharacterAllianceHistory = await _repoPublic.Character_AllianceHistory(i);
            if (CharacterAllianceHistory == null)
                return NotFound();

            return Ok(CharacterAllianceHistory);
        }

        [HttpGet(ApiRoutes.Character.Preview)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Preview(int i)
        {
            CharacterPreview character_preview = await _repoPublic.Character_Preview(i);
            if (character_preview == null)
                return NotFound();
            return Ok(character_preview);
        }

        [HttpPost(ApiRoutes.Character.Previews)]
        public Task<IActionResult> Previews([FromBody]int[] characters_ids)
        {
            //var CharactersCorporationHistories = await _repoPublic.Characters_CorporationHistory(characters_ids);
            //if (CharactersCorporationHistories == null)
            //    return NotFound();

            //return Ok(CharactersCorporationHistories);
            throw new NotImplementedException();
        }
    }
}