using eveDirect.Databases.Contexts;
using eveDirect.Services.Jobs.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using eveDirect.Databases.Contexts.Public.Models;
using System.Linq;
using System.Collections.Generic;

namespace Services.Jobs.Processings
{
    /// <summary>
    /// Джоба, контролирующая элементы в поиске на сайте
    /// </summary>
    public class SearchItemsJob : JobBase
    {
        readonly DbContextOptions<PublicContext> _dbContextOptions;

        public SearchItemsJob(
            ILogger<SearchItemsJob> logger,
            DbContextOptions<PublicContext> dbContextOptions) 
            : base(logger)
        {
            _dbContextOptions = dbContextOptions;
        }

        public override void Execute()
        {
            using var _context = new PublicContext(_dbContextOptions);

            // Персонажи
            if(_context.EveOnline_Characters.Count() > _context.SearchItems.Count(x => x.type == ESearchItemType.character))
            {
                var chars = _context.EveOnline_Characters
                .Select(x => new { x.character_id, x.name })
                .ToList();

                var search_items = new List<SearchItem>();
                search_items.AddRange(chars.Select(x => new SearchItem() { type = ESearchItemType.character, item_id = x.character_id, title = x.name }));

                MergeItems(search_items);
            }

            // Корпорации
            if (_context.EveOnline_Corporations.Count() > _context.SearchItems.Count(x => x.type == ESearchItemType.corporation))
            {
                var corps = _context.EveOnline_Corporations
                .Select(x => new { x.corporation_id, x.name })
                .ToList();

                var search_items = new List<SearchItem>();
                search_items.AddRange(corps.Select(x => new SearchItem() { type = ESearchItemType.corporation, item_id = x.corporation_id, title = x.name }));

                MergeItems(search_items);
            }

            // Альянсы
            if (_context.EveOnline_Alliances.Count() > _context.SearchItems.Count(x => x.type == ESearchItemType.alliance))
            {
                var allies = _context.EveOnline_Alliances
                .Select(x => new { x.alliance_id, x.name })
                .ToList();

                var search_items = new List<SearchItem>();
                search_items.AddRange(allies.Select(x => new SearchItem() { type = ESearchItemType.alliance, item_id = x.alliance_id, title = x.name }));

                MergeItems(search_items);
            }

            // Имущество
            if (_context.Eveonline_UniverseTypes.Count() * 7 > _context.SearchItems.Count(x => x.type == ESearchItemType.alliance))
            {
                var types = _context.Eveonline_UniverseTypes
                    .Select(x => new { x.type_id, x.enname, x.dename, x.frname, x.janame, x.koname, x.runame, x.zhname })
                    .ToList();

                var search_items = new List<SearchItem>();
                search_items.AddRange(types.Select(x => new SearchItem() { type = ESearchItemType.type, item_id = x.type_id, title = x.enname }));
                search_items.AddRange(types.Select(x => new SearchItem() { type = ESearchItemType.type, item_id = x.type_id, title = x.dename }));
                search_items.AddRange(types.Select(x => new SearchItem() { type = ESearchItemType.type, item_id = x.type_id, title = x.frname }));
                search_items.AddRange(types.Select(x => new SearchItem() { type = ESearchItemType.type, item_id = x.type_id, title = x.janame }));
                search_items.AddRange(types.Select(x => new SearchItem() { type = ESearchItemType.type, item_id = x.type_id, title = x.koname }));
                search_items.AddRange(types.Select(x => new SearchItem() { type = ESearchItemType.type, item_id = x.type_id, title = x.runame }));
                search_items.AddRange(types.Select(x => new SearchItem() { type = ESearchItemType.type, item_id = x.type_id, title = x.zhname }));

                MergeItems(search_items);
            }

            // Локации
            if (_context.EveOnline_Alliances.Count() > _context.SearchItems.Count(x => x.type == ESearchItemType.location))
            {
                var locs = _context.EveOnlineUniverseLocations
                .Select(x => new { x.id, x.name })
                .ToList();

                var search_items = new List<SearchItem>();
                search_items.AddRange(locs.Select(x => new SearchItem() { type = ESearchItemType.location, item_id = x.id, title = x.name }));

                MergeItems(search_items);
            }
        }

        void MergeItems(List<SearchItem> items)
        {
            using var _context = new PublicContext(_dbContextOptions);
            _context.SearchItems.BulkMerge(items);
        }
    }
}
