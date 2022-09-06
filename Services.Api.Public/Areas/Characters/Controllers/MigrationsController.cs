using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using eveDirect.Repo.PublicReadOnly.Models;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;

namespace eveDirect.Api.Public.Areas.Characters.Controllers
{
    [Route(ApiRoutes.Character.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("characters")]
    [ApiExplorerSettings(GroupName = "Characters")]
    public class MigrationsController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        public MigrationsController(IReadOnly repoPublic)
        {
            _repoPublic = repoPublic;
        }

        [HttpGet(ApiRoutes.Character.MigrationsRoot)]
        public async Task<LoadResult> NewbornsCorpsAsync(DataSourceLoadOptions lo)
        {
            var model = await _repoPublic.Characters_MigrationsRoot_Items(lo);
            return model;
        }
    }
}
