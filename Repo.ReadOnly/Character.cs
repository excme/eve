using eveDirect.Databases.Contexts;
using Microsoft.EntityFrameworkCore;
using eveDirect.Repo.PublicReadOnly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eveDirect.BaseRepo;
using eveDirect.Shared.Helper;

namespace eveDirect.Repo.PublicReadOnly
{
    public partial class ReadOnly : IReadOnly
    {
        public async Task<IdRanges> Character_IdRanges()
        {
            return await Task.Run(() => character_IdRanges);
        }

        /// <summary>
        /// Расчет диапозов ид персонажей
        /// </summary>
        public async Task Characters_CalcIdRanges()
        {
            await using var context = new PublicContext(_options);
            var characterIds = await context.EveOnline_Characters.Select(x => x.character_id).ToListAsync();
            character_IdRanges = Generic_CalcRange(characterIds.Select(x => x.ToInt64()).ToList());
        }
        //public async Task<bool> Character_Exist(int character_id)
        //{
        //    using var _eveOnlinePublicContext = new PublicContext(_options);
        //    return await _eveOnlinePublicContext.EveOnline_Characters.AnyAsync(x => x.character_id == character_id);
        //}
        public async Task<CharacterPublicModel> Character_PublicInfo(int character_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var character_info = await _eveOnlinePublicContext.EveOnline_Characters.Select(x => new { x.character_id, x.name }).FirstOrDefaultAsync(x => x.character_id == character_id);

            if (character_info == null)
                return null;

            return new CharacterPublicModel()
            {
                id = character_info.character_id,
                name = character_info.name
            };
        }
        public async Task<List<CharacterPublicModel>> Character_PublicInfos(int[] character_ids)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var character_infos = await _eveOnlinePublicContext.EveOnline_Characters
                .Where(x => character_ids.Contains(x.character_id))
                .Select(x => new CharacterPublicModel() { id = x.character_id, name = x.name })
                .ToListAsync();
            return character_infos;
        }
        public async Task<List<NameModel<int>>> Character_Name(int character_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            List<NameModel<int>> character_names = await _eveOnlinePublicContext.EveOnline_Characters
                .Select(x => new NameModel<int> { id=x.character_id, name = x.name })
                .Where(x => x.id == character_id)
                .ToListAsync();

            return character_names;
        }
        public async Task<List<NameModel<int>>> Character_Names(int[] character_ids)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var character_names = await _eveOnlinePublicContext.EveOnline_Characters
                .Where(x => character_ids.Contains(x.character_id))
                .Select(x => new NameModel<int>(x.character_id, x.name))
                .ToListAsync();
            return character_names;
        }
        /// <summary>
        /// История корпораций у персонажа
        /// </summary>
        public async Task<List<CharacterCorporationHistoryModel.CorporationHistoryItem>> Character_CorporationHistory(int character_id)
        {
            using var context = new PublicContext(_options);
            var db_corpHistory = await context.EveOnline_CharactersCorporationHistory
                .Where(x => x.character_id == character_id)
                .Select(h => new CharacterCorporationHistoryModel.CorporationHistoryItem()
                {
                    corporation_id = h.corporation_id,
                    start_date = h.start_date,
                    deleted = h.is_deleted
                })
                .ToListAsync();

            return db_corpHistory;
        }
        /// <summary>
        /// История альянсов у персонажа
        /// </summary>
        public async Task<List<CharacterAllianceHistoryModel.AllianceHistoryItem>> Character_AllianceHistory(int character_id)
        {
            using var context = new PublicContext(_options);
            var db_allyHistory = await context.EveDirect_CharacterAllianceHistory
                .Where(x => x.character_id == character_id)
                .Select(x => new CharacterAllianceHistoryModel.AllianceHistoryItem()
                {
                    alliance_id = x.alliance_id,
                    corporation_id = x.corporation_id,
                    start = x.start,
                    end = x.end
                })
                .ToListAsync();
            return db_allyHistory;
        }
        public async Task<List<CharacterCorporationHistoryModel>> Characters_CorporationHistory(int[] character_ids)
        {
            using var context = new PublicContext(_options);
            var db_corpHistories = await context.EveOnline_CharactersCorporationHistory
                .Where(x => character_ids.Contains(x.character_id))
                .ToListAsync();

            return db_corpHistories
                    .GroupBy(x => x.character_id, (key, group) => new CharacterCorporationHistoryModel()
                    {
                        character_id = key,
                        corp_history = group.Select(xx => new CharacterCorporationHistoryModel.CorporationHistoryItem()
                        {
                            corporation_id = xx.corporation_id,
                            deleted = xx.is_deleted,
                            start_date = xx.start_date
                        })
                    }).ToList();

            // Уникальные corp_id
            //List<NameModel<int>> corp_names = default;
            //int[] all_corps_ids = db_corpHistories.Select(x => x.corporation_id).Distinct().ToArray();
            //if (all_corps_ids.Any())
            //    corp_names = await Corporation_Names(all_corps_ids);

            //var corpHistoryList = db_corpHistories
            //    .Join(corp_names,
            //        ch => ch.corporation_id,
            //        cn => cn.id,
            //        (ch, cn) => new { ch, cn }
            //    )
            //    .GroupBy(x => x.ch.character_id, (key, group) => new CharacterCorporationHistoryModel()
            //    {
            //        character_id = key,
            //        corp_history = group.Select(xx => new CharacterCorporationHistoryModel.CorporationHistoryItem()
            //        {
            //            corporation_id = xx.ch.corporation_id,
            //            //corporation_name = xx.cn.name,
            //            deleted = xx.ch.is_deleted,
            //            start_date = xx.ch.start_date
            //        })
            //    }).ToList();

            //return corpHistoryList;
        }
        public async Task<CharacterPreview> Character_Preview(int character_id)
        {
            using var context = new PublicContext(_options);
            var character_prev = await context.EveOnline_Characters.Select(x => new { x.character_id, x.name, x.alliance_id, x.security_status, x.corporation_id, x.preview }).FirstOrDefaultAsync(x => x.character_id == character_id);
            if (character_prev != null)
            {
                CharacterPreview model = default;
                if (character_prev.preview != null)
                    model = new CharacterPreview(character_prev.preview);
                else
                    model = new CharacterPreview();

                model.sec_status = character_prev.security_status;
                model.character_name = character_prev.name;
                model.alliance_id = character_prev.alliance_id;
                model.corporation_id = character_prev.corporation_id;

                if (model?.alliance_id > 0)
                    model.alliance_name = (await Alliance_Name((int)model.alliance_id))?.name;

                if (model?.corporation_id > 0) {
                    NameModel<int> n = await Corporation_Name((int)model.corporation_id);
                    model.corporation_name = n?.name ?? "";
                }

                return model;
            }

            return null;
        }

    }
}
