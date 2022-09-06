using Microsoft.EntityFrameworkCore;
using eveDirect.Repo.PublicReadOnly.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.BaseRepo;
using eveDirect.Shared.Helper;
using System.Security.Cryptography.X509Certificates;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using eveDirect.Databases.Contexts;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly
    {
        public async Task<IdRanges> Alliances_IdRanges()
        {
            return await Task.Run(() => alliance_IdRanges);
        }

        /// <summary>
        /// Расчет диапозов ид альянсов
        /// </summary>
        public async Task Alliances_CalcIdRanges()
        {
            await using var context = new PublicContext(_options);
            var allianceIds = await context.EveOnline_Alliances.Select(x => x.alliance_id).ToListAsync();
            alliance_IdRanges = Generic_CalcRange(allianceIds.Select(x => x.ToInt64()).ToList());
        }

        public async Task<NameModel<int>> Alliance_Name(int alliance_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var allaince_info = await _eveOnlinePublicContext.EveOnline_Alliances.Select(x => new { x.alliance_id, x.name }).FirstOrDefaultAsync(x => x.alliance_id == alliance_id);

            if (allaince_info == null)
                return null;

            return new NameModel<int>(allaince_info.alliance_id, allaince_info.name);
        }
        public async Task<List<NameModel<int>>> Alliance_Names(int[] alliance_ids)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var alliance_names = await _eveOnlinePublicContext.EveOnline_Alliances
                .Where(x => alliance_ids.Contains(x.alliance_id))
                .Select(x => new NameModel<int>(x.alliance_id, x.name))
                .ToListAsync();
            return alliance_names;
        }
        public async Task<List<NameModel<int>>> Alliance_Names(int alliance_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            List<NameModel<int>> ally_names = await _eveOnlinePublicContext.EveOnline_Alliances
                .Select(x => new NameModel<int> { id = x.alliance_id, name = x.name })
                .Where(x => x.id == alliance_id)
                .ToListAsync();

            return ally_names;
        }
        public async Task<AlliancePreview> Alliance_Preview(int i)
        {
            using var _context = new PublicContext(_options);
            var preview = await _context.EveOnline_Alliances
                .Select(x => new
                {
                    corps_count = x.corps_count,
                    creator_corporation_id = x.creator_corporation_id,
                    creator_id = x.creator_id,
                    date_founded = x.date_founded,
                    executor_corporation_id = x.executor_corporation_id,
                    faction_id = x.faction_id,
                    ticker = x.ticker,
                    id = x.alliance_id,
                })
                .FirstOrDefaultAsync(x => x.id == i);
            if(preview != null)
            {
                AlliancePreview model = new AlliancePreview()
                {
                    corps_count = preview.corps_count,
                    creator_corporation_id = preview.creator_corporation_id,
                    creator_id = preview.creator_id,
                    date_founded = preview.date_founded,
                    executor_corporation_id = preview.executor_corporation_id,
                    faction_id = preview.faction_id,
                    ticker = preview.ticker
                };
                return model;
            }

            return null;
        }

        public async Task<LoadResult> Alliance_CurrentCharacters(int alliance_id, DataSourceLoadOptionsBase lo)
        {
            await using var context = new PublicContext(_options);

            var query = context.EveDirect_CharacterAllianceHistory
                .AsQueryable()
                .Where(x => x.alliance_id == alliance_id && x.end == null)
                .Select(x => new AllianceCharacterMemberModel()
                {
                    c = x.corporation_id,
                    i = x.character_id,
                    s = x.start,
                });

            return await DataSourceLoader.LoadAsync(query, lo);
        }
    }
}
