using System;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.Caching;
using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;
using System.Collections.Generic;
using eveDirect.Repo.PublicReadOnly.Models;

namespace eveDirect.Api.Public.Areas.Universe.Controllers
{
    [Route(ApiRoutes.Universe.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("universe")]
    [ApiExplorerSettings(GroupName = "Universe")]
    public class PublicController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        ICustomDistibutedCache _cache { get; set; }
        public PublicController(IReadOnly repoPublic, ICustomDistibutedCache cache)
        {
            _repoPublic = repoPublic;
            _cache = cache;
        }

        
        [HttpPost(ApiRoutes.Universe.TypeNames)]
        public async Task<IActionResult> Type_Names([FromBody]int[] type_ids, string l = "en")
        {
            if(type_ids?.Any() ?? false)
            {
                var r = await _repoPublic.Universe_Types_Names(where: x => type_ids.Contains(x.id), l);
                return Ok(r);
            }

            return NoContent();
        }
        /// <summary>
        /// Получение информации недостающих отностельно входящего массива
        /// </summary>
        [HttpPost(ApiRoutes.Universe.TypeNamesMissing)]
        public async Task<IActionResult> Type_Names_Missing([FromBody]int[] t, string l = "en")
        {
            t = t == null ? new int[] { } : t;

            var r = await _repoPublic.Universe_Types_Names(where: x => !t.Contains(x.id), l);
            return Ok(r);
        }
        [HttpGet(ApiRoutes.Universe.TypeName)]
        public async Task<IActionResult> Type_Name(int i, string l = "en")
        {
            return Ok(await _repoPublic.Universe_Types_Names(where: x => x.id == i, lang: l));
        }

        [HttpGet(ApiRoutes.Universe.TypeAllNames)]
        public async Task<IActionResult> Types_AllNames(string l = "en")
        {
            return Ok(await _repoPublic.Universe_Types_Names(lang: l));
        }

        [HttpGet(ApiRoutes.Universe.LocationName)]
        public async Task<IActionResult> Location_Name(long i)
        {
            return Ok(await _repoPublic.Universe_Location_Names(where: x => x.id == i));
        }

        [HttpPost(ApiRoutes.Universe.LocationNames)]
        public async Task<IActionResult> Location_Names([FromBody]long[] loc_ids)
        {
            if (loc_ids?.Any() ?? false)
            {
                var r = await _repoPublic.Universe_Location_Names(where: x => loc_ids.Contains(x.id));
                return Ok(r);
            }

            return NoContent();
        }

        
    }
}