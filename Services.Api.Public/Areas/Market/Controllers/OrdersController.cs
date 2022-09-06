using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.Api.Public.Areas.Market.Data;
using eveDirect.Caching;
using eveDirect.Shared.Api;
using eveDirect.Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using eveDirect.Repo.PublicReadOnly;
using eveDirect.Repo.PublicReadOnly.Models;
using DevExtreme.AspNet.Data.ResponseModel;

namespace eveDirect.Api.Public.Areas.Market.Controllers
{
    [Route(ApiRoutes.Market.Route)]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Area("market")]
    [ApiExplorerSettings(GroupName = "Market")]
    public class OrdersController : ControllerBase
    {
        IReadOnly _repoPublic { get; set; }
        ICustomDistibutedCache _cache { get; set; }
        public OrdersController(IReadOnly repoPublic, ICustomDistibutedCache cache)
        {
            _repoPublic = repoPublic ?? throw new System.ArgumentNullException(nameof(repoPublic));
            _cache = cache ?? throw new System.ArgumentNullException(nameof(cache));
        }
        /// <summary>
        /// Регионы и системы активных рыночных ордеров
        /// </summary>
        //[HttpGet(ApiRoutes.Market.Market_ActiveRegionsSystems)]
        //public async Task<IActionResult> Market_ActiveRegionsSystems()
        //{
        //    List<int> list = default;
        //    if (!_cache.Get(CacheKeys.ApiMarketActiveRegionsSystems, out list))
        //    {
        //        list = await _repoPublic.Market_ActiveOrdersRegionsAndSystems();
        //        await _cache.SetAsync(CacheKeys.ApiMarketActiveRegionsSystems, list);
        //    }

        //    return Ok(list);
        //}

        /// <summary>
        /// Список регионов и их систем
        /// </summary>
        [HttpGet(ApiRoutes.Market.Market_AllRegionsSystems)]
        public async Task<IActionResult> Market_AllRegionsSystems()
        {
            List<MarketRegionModel> list = default;
            if (!_cache.Get(CacheKeys.ApiMarketAllRegionsSystems, out list))
            {
                list = await _repoPublic.Market_AllRegionsAndSystems();
                await _cache.SetAsync(CacheKeys.ApiMarketAllRegionsSystems, list);
            }

            return Ok(list);
        }
        [HttpGet(ApiRoutes.Market.Market_Groups)]
        public async Task<IActionResult> Market_Groups(string l = "en")
        {
            // Проверка на доступность языка
            //lang = AvailableLanguages.Exists(lang) ? lang : "en";

            string cache_key = string.Format(CacheKeys.ApiMarketGroups, l);
            List<MarketGroupModel> groups = default;
            if (!_cache.Get(cache_key, out groups))
            {
                groups = await _repoPublic.Market_Groups(l);
                await _cache.SetAsync(cache_key, groups);
            }

            return Ok(groups);
        }
        //[HttpPost(ApiRoutes.Market.Market_Orders)]
        //public async Task<IActionResult> Market_Orders(int t, bool b, int p, [FromBody] int[] s)
        //{
        //    if (s?.Length > 0)
        //    {
        //        var orders = await _repoPublic.Market_Orders(t, b, p, s);
        //        return Ok(orders);
        //    }

        //    return NotFound();
        //}
        [HttpPost(ApiRoutes.Market.Market_Orders)]
        public async Task<LoadResult> Market_Orders2([FromBody]MarketOrderParam _params)
        {
            var orders = await _repoPublic.Market_Orders(_params.t, _params.b, _params.s, _params.lo);
            return orders;
        }
    }
}