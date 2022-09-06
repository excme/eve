using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Repo.PublicReadOnly.Models;
using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eveDirect.Api.Public.Areas.Characters.Controllers
{
    [Route(ApiRoutes.Character.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("characters")]
    [ApiExplorerSettings(GroupName = "Characters")]
    public class NewbornsController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public NewbornsController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }

        /// <summary>
        /// Запрос списка новорожденных
        /// </summary>
        [HttpGet(ApiRoutes.Character.Newborns)]
        public async Task<IActionResult> NewbornsAsync()
        {
            return Ok(await _repoPublic.CharacterNewbornItems());
        }

        //[HttpPost(ApiRoutes.Character.NewbornsCorps)]
        //public async Task<LoadResult> NewbornsCorpsAsync([FromBody] DataSourceLoadOptionsBase lo)
        //{
        //    // Проверка на наличие персонажа
        //    List<CharacterNewbornCorporationItem> newBornsCorps = await _repoPublic.CharacterNewbornsCorpsItems();
        //    return DataSourceLoader.Load(newBornsCorps, lo);
        //}

        ///// <summary>
        ///// Запрос данных графика новорожденных
        ///// </summary>
        //[HttpGet(ApiRoutes.Character.NewbornsChart)]
        //public async Task<IActionResult> NewbornsChartAsync()
        //{
        //    return Ok(await _repoPublic.CharacterNewbornChartItems());
        //}

        //[HttpGet(ApiRoutes.Character.NewbornsCCP)]
        //public async Task<IActionResult> NewbornsNCPAsync()
        //{
        //    // Проверка на наличие персонажа CCP
        //    var newBornsNPC = await _repoPublic.CharacterCCPNewbornItems();
        //    return Ok(newBornsNPC);
        //}
    }
}
