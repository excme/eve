using eveDirect.Caching;
using eveDirect.Databases.Contexts;
using Microsoft.EntityFrameworkCore;
using eveDirect.Repo.PublicReadOnly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly
    {
        public async Task<List<UniverseTypeDetail>> Universe_Types_Names(
            Expression<Func<UniverseTypeDetail, bool>> where = null,
            string lang = "en")
        {
            List<UniverseTypeDetail> all_names_by_lang = default;

            // Запрос всех имен по языку
            if (!_cache.Get(string.Format(CacheKeys.ApiUniverseTypeName, lang), out all_names_by_lang))
            {
                using var context = new PublicContext(_options);

                all_names_by_lang = await context.Eveonline_UniverseTypes
                    .AsNoTracking()
                    .Select(x => new UniverseTypeDetail() { 
                        id = x.type_id, 
                        name = EF.Property<string>(x, lang + "name"), 
                        img_tages = x.img_tags 
                    })
                    .Where(where)
                    .ToListAsync();

                await _cache.SetAsync(string.Format(CacheKeys.ApiUniverseTypeName, lang), all_names_by_lang);
            }

            // Фильтрация
            if (where != null)
                return all_names_by_lang.Where(where.Compile()).ToList();

            return all_names_by_lang;
        }

        public async Task<List<MarketGroupModel>> Universe_Type_Groups(string lang = "en")
        {
            var groups_structure = new List<MarketGroupModel>();
            string cache_key = string.Format(CacheKeys.ApiMarketContractsGroups, lang);
            if (!_cache.Get(cache_key, out groups_structure))
            {
                using var context = new PublicContext(_options);

                var _cats = await context.Eveonline_UniverseCategories
                    .Where(x => x.published)
                    .Select(x => new MarketGroupModel()
                    {
                        id = x.category_id + 1000000,
                        name = EF.Property<string>(x, lang + "name")
                    })
                    .ToListAsync();

                var _groups = await context.Eveonline_UniverseGroups
                    .Where(x => x.published && _cats.Select(xx => xx.id - 1000000).Contains(x.category_id))
                    .Select(x => new MarketGroupModel()
                    {
                        id = x.group_id + 2000000,
                        name = EF.Property<string>(x, lang + "name"),
                        parent = x.category_id + 1000000
                    })
                    .ToListAsync();

                var _types = await context.Eveonline_UniverseTypes
                    .Where(x => x.published && _groups.Select(xx => xx.id - 2000000).Contains(x.group_id))
                    .Select(x => new MarketGroupModel()
                    {
                        id = x.type_id,
                        parent = x.group_id + 2000000
                    })
                    .ToListAsync();

                groups_structure = _cats.Concat(_groups).Concat(_types).ToList();
                await _cache.SetAsync(cache_key, groups_structure);
            }

            return groups_structure;
        }

        public async Task<List<UniverseLocationName>> Universe_Location_Names(Expression<Func<UniverseLocationName, bool>> where = null)
        {
            List<UniverseLocationName> all_names = default;

            // Запрос всех имен по языку
            if (!_cache.Get(CacheKeys.ApiUniverseLocationNames, out all_names))
            {
                using var context = new PublicContext(_options);
                
                all_names = await context.EveOnlineUniverseLocations
                .AsNoTracking()
                .Select(x => new UniverseLocationName() { id = x.id, name = x.name, sec_status = x.security_status })
                .Where(where)
                .ToListAsync();

                await _cache.SetAsync(CacheKeys.ApiUniverseLocationNames, all_names);
            }

            // Фильтрация
            if (where != null)
                return all_names.Where(where.Compile()).ToList();


            return all_names;
        }
    }
}
