using System.Threading.Tasks;
using eveDirect.Caching;
using eveDirect.Shared.Api;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Repo.PublicReadOnly.Models;
using System.Collections.Generic;
using DevExtreme.AspNet.Data.ResponseModel;
using eveDirect.Api.Public.Areas.Market.Data;

namespace eveDirect.Api.Public.Areas.Market.Controllers
{
    [Route(ApiRoutes.Contracts.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("market")]
    [ApiExplorerSettings(GroupName = "Market")]
    public class ContractsController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        ICustomDistibutedCache _cache { get; set; }
        public ContractsController(IReadOnly repoPublic, ICustomDistibutedCache cache)
        {
            _repoPublic = repoPublic 
                ?? throw new System.ArgumentNullException(nameof(repoPublic));
            _cache = cache 
                ?? throw new System.ArgumentNullException(nameof(cache));
        }

        [HttpGet(ApiRoutes.Contracts.Groups)]
        public async Task<IActionResult> Groups(string l)
        {
            return Ok(await _repoPublic.Universe_Type_Groups(l));
        }
        [HttpPost(ApiRoutes.Contracts.List)]
        public async Task<LoadResult> List([FromBody] MarketContractParam param)
        {
            var contracts = await _repoPublic.Contracts_List(param.t, param.r, param.lo);
            return contracts;
        }
        [HttpGet(ApiRoutes.Contracts.Details)]
        public async Task<IActionResult> Details(int i)
        {
            ContractDetail prev = await _repoPublic.Contracts_Detail(i);
            return Ok(prev);
        }
        [HttpGet(ApiRoutes.Contracts.Items)]
        public async Task<IActionResult> Items(int i)
        {
            List<ContractItem> items = await _repoPublic.Contracts_Items(i);
            return Ok(items);
        }
        [HttpGet(ApiRoutes.Contracts.Bids)]
        public async Task<IActionResult> Bids(int i)
        {
            var bids = await _repoPublic.Contracts_Bids(i);
            return Ok(bids);
        }
        [HttpGet(ApiRoutes.Market.Market_AllRegions)]
        public async Task<IActionResult> Market_AllRegionsSystems()
        {
            List<NameModel<long>> list = default;
            if (!_cache.Get(CacheKeys.ApiMerketContractsRegions, out list))
            {
                list = await _repoPublic.Market_AllRegions();
                await _cache.SetAsync(CacheKeys.ApiMerketContractsRegions, list);
            }

            return Ok(list);
        }
    }
}