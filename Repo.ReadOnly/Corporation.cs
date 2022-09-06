using eveDirect.Databases;
using Microsoft.EntityFrameworkCore;
using eveDirect.Repo.PublicReadOnly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.BaseRepo;
using eveDirect.Shared.Helper;
using eveDirect.Databases.Contexts;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly
    {
        public async Task<IdRanges> Corporation_IdRanges()
        {
            return await Task.Run(() => corporation_IdRanges);
        }

        /// <summary>
        /// Расчет диапозов ид корпораций
        /// </summary>
        public async Task Corporations_CalcIdRanges()
        {
            await using var context = new PublicContext(_options);
            var corporationsIds = await context.EveOnline_Corporations.Select(x => x.corporation_id).ToListAsync();
            corporation_IdRanges = Generic_CalcRange(corporationsIds.Select(x => x.ToInt64()).ToList());
        }

        public async Task<CorporationPublicModel> Corporation_PublicInfo(int corporations_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var corporation_info = await _eveOnlinePublicContext.EveOnline_Corporations.Select(x => new { x.corporation_id, x.name }).FirstOrDefaultAsync(x => x.corporation_id == corporations_id);

            if (corporation_info == null)
                return null;

            return new CorporationPublicModel()
            {
                id = corporation_info.corporation_id,
                name = corporation_info.name
            };
        }
        public async Task<List<CorporationPublicModel>> Corporation_PublicInfos(int[] corporations_ids)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var corporation_infos = await _eveOnlinePublicContext.EveOnline_Corporations
                .Where(x => corporations_ids.Contains(x.corporation_id))
                .Select(x => new CorporationPublicModel() { id = x.corporation_id, name = x.name })
                .ToListAsync();
            return corporation_infos;
        }
        public async Task<NameModel<int>> Corporation_Name(int corporation_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var corp_name = await _eveOnlinePublicContext.EveOnline_Corporations
                .Select(x => new NameModel<int> { id = x.corporation_id, name = x.name })
                .FirstOrDefaultAsync(x => x.id == corporation_id);

            return corp_name;
        }
        public async Task<List<NameModel<int>>> Corporation_Names(int corporation_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            List<NameModel<int>> corp_names = await _eveOnlinePublicContext.EveOnline_Corporations
                .Select(x => new NameModel<int> { id = x.corporation_id, name = x.name })
                .Where(x => x.id == corporation_id)
                .ToListAsync();

            return corp_names;
        }
        public async Task<List<NameModel<int>>> Corporation_Names(int[] corporations_ids)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var corporation_names = await _eveOnlinePublicContext.EveOnline_Corporations
                .Where(x => corporations_ids.Contains(x.corporation_id))
                .Select(x => new NameModel<int>() { id = x.corporation_id, name = x.name })
                .ToListAsync();
            return corporation_names;
        }
        public async Task<List<CorporationMembersMigrationItem>> Corporation_MembersMigrations(int corporation_id, int page = 0)
        {
            List<CorporationMembersMigrationItem> to_model = default;
            using var context = new PublicContext(_options);
            int pageSize = 50;
            var corporation = await context.EveOnline_Corporations.FirstOrDefaultAsync(x => x.corporation_id == corporation_id);
            if (corporation?.membersMigrations?.Any() ?? false)
            {
                var records = corporation.membersMigrations.Skip(pageSize * page).Take(pageSize);
                var history_records = await context.EveOnline_CharactersCorporationHistory
                    .Where(x => records.Contains(x.record_id)).ToListAsync();
                var characters_ids = history_records.Select(x => x.character_id);
                var corps = history_records.Select(x => x.prev_corp_id).ToList();
                corps.AddRange(history_records.Select(x => x.next_corp_id));
                corps.AddRange(history_records.Select(x => x.corporation_id));

                List<NameModel<int>> character_names = await Character_Names(characters_ids.Distinct().ToArray());
                var corps_names = await Corporation_Names(corps.Distinct().ToArray());

                to_model = history_records
                    .Join(character_names,
                        hr => hr.character_id,
                        ch => ch.id,
                        (hr, ch) => new { hr, ch }
                    )
                    .GroupJoin(corps_names,
                        hr => hr.hr.prev_corp_id,
                        co => co.id,
                        (hr, co) => new { hr, co }
                    )
                    .SelectMany(x => x.co.DefaultIfEmpty(), (hrs, co) => new {
                        hrs.hr,
                        co
                    })
                    .GroupJoin(corps_names,
                        hr => hr.hr.hr.corporation_id,
                        co => co.id,
                        (hr, co) => new { hr, co }
                    )
                    .SelectMany(x => x.co.DefaultIfEmpty(), (hrs, co) => new {
                        hrs.hr,
                        co
                    })
                    .GroupJoin(corps_names,
                        hr => hr.hr.hr.hr.next_corp_id,
                        co => co.id,
                        (hr, co) => new { hr, co }
                    )
                    .SelectMany(x => x.co.DefaultIfEmpty(), (hrs, co) => new {
                        hrs.hr,
                        co
                    })
                    .Select(x => new CorporationMembersMigrationItem()
                    {
                        character_id = x.hr.hr.hr.hr.character_id,
                        character_name = x.hr.hr.hr.ch.name,
                        type = corporation_id == x.hr.hr.hr.hr.corporation_id ? EMemberMigrationType.joined : EMemberMigrationType.unjoin,

                        cur_corp_id = x.hr.hr.hr.hr.corporation_id,
                        cur_corp_name = x.hr.co?.name,

                        next_corp_id = x.co?.id == 0 ? null : x.co?.id,
                        next_corp_name = x.co?.name,

                        prev_corp_id = x.hr.hr.co?.id == 0 ? null : x.hr.hr.co?.id,
                        prev_corp_name = x.hr.hr.co?.name,

                        start_date = x.hr.hr.hr.hr.start_date,
                        end_date = x.hr.hr.hr.hr.end_date,
                    })
                    .ToList();
            }

            return to_model;
        }
        public async Task<List<CorporationAllianceHistory.AllianceHistoryItem>> Corporation_AllianceHistory(int corporation_id)
        {
            using var context = new PublicContext(_options);
            var db_allyHistory = await context.EveOnline_CorporationAllianceHistories.Where(x => x.corporation_id == corporation_id && x.alliance_id > 0).ToListAsync();

            if (db_allyHistory == null)
                return null;

            if (db_allyHistory.Count > 0)
            {
                List<NameModel<int>> ally_names = default;
                int[] all_allies_ids = db_allyHistory.Select(x => x.alliance_id).Distinct().ToArray();
                if (all_allies_ids.Any())
                    ally_names = await Alliance_Names(all_allies_ids);

                var allyHistoryList = db_allyHistory.Join(ally_names,
                    h => h.alliance_id,
                    c => c.id,
                    (h, c) => new CorporationAllianceHistory.AllianceHistoryItem()
                    {
                        alliance_id = h.alliance_id,
                        alliance_name = c.name,
                        start_date = h.start_date,
                        end_date = h.end_date,
                        //record_id = h.record_id,
                        deleted = h.is_deleted
                    }).ToList();

                return allyHistoryList;
            }
            else
            {
                return new List<CorporationAllianceHistory.AllianceHistoryItem>();
            }
        }

        public async Task<List<CorporationAllianceHistory>> Corporation_AllianceHistory(int[] corporation_ids)
        {
            using var context = new PublicContext(_options);
            var db_allyHistories = await context.EveOnline_CorporationAllianceHistories.Where(x => corporation_ids.Contains(x.corporation_id)).ToListAsync();

            // Уникальные alliance_id
            List<NameModel<int>> ally_names = default;
            int[] all_allies_ids = db_allyHistories.Select(x => x.alliance_id).Distinct().ToArray();
            if (all_allies_ids.Any())
                ally_names = await Alliance_Names(all_allies_ids);

            var corpHistoryList = db_allyHistories
                .Join(ally_names,
                    ch => ch.alliance_id,
                    cn => cn.id,
                    (co, an) => new { co, an }
                )
                .GroupBy(x => x.co.corporation_id, (key, group) => new CorporationAllianceHistory()
                {
                    corporation_id = key,
                    ally_history = group.Select(xx => new CorporationAllianceHistory.AllianceHistoryItem()
                    {
                        alliance_id = xx.co.corporation_id,
                        alliance_name = xx.an.name,
                        deleted = xx.co.is_deleted,
                        start_date = xx.co.start_date
                    })
                }).ToList();

            return corpHistoryList;
        }
        public async Task<CorporationPreview> Corporation_Preview(int corporation_id)
        {
            using var context = new PublicContext(_options);
            var corporation_prev = await context.EveOnline_Corporations.Select(x => new { x.corporation_id, x.alliance_id, x.ceo_id, x.home_station_id, x.member_count, x.ncp, x.name, x.preview }).FirstOrDefaultAsync(x => x.corporation_id == corporation_id);
            if (corporation_prev != null)
            {
                CorporationPreview model = default;
                if (corporation_prev.preview != null)
                    model = new CorporationPreview(corporation_prev.preview);
                else
                    model = new CorporationPreview();

                model.alliance_id = corporation_prev.alliance_id;
                model.corporation_name = corporation_prev.name;
                model.npc = corporation_prev.ncp ? true : default;
                model.member_count = corporation_prev.member_count;

                if (model?.alliance_id > 0)
                    model.alliance_name = (await Alliance_Name((int)model.alliance_id))?.name;

                if (model?.ceo_id > 0)
                    model.ceo_name = (await Character_Name((int)model.ceo_id))?[0].name;

                return model;
            }

            return null;
        }
    }
}
