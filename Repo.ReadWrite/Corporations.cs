using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared;
using eveDirect.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using eveDirect.Shared.EventBus.Events;

namespace eveDirect.Repo.ReadWrite
{
    public partial class ReadWriteRepo : IReadWrite
    {
        public void Corporation_UpdatePreview(int corporation_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var corporation =  _eveOnlinePublicContext.EveOnline_Corporations.FirstOrDefault(x => x.corporation_id == corporation_id);
            if (corporation != null)
            {
                if (corporation.preview == null)
                    corporation.preview = new EveOnlineCorporationPreview();

                //corporation.preview.killmails_count = corporation.KillmailsActivity.Count;
                corporation.preview.ally_movings =  _eveOnlinePublicContext.EveOnline_CorporationAllianceHistories.Count(x => x.corporation_id == corporation_id);

                //_eveOnlinePublicContext.EveOnline_Corporations.Update(corporation);
                _eveOnlinePublicContext.Attach(corporation);
                _eveOnlinePublicContext.Entry(corporation).Property(x => x.preview).IsModified = true;

                 _eveOnlinePublicContext.SaveChanges();
            }
        }
        public List<int> Corporation_Ids(
            Expression<Func<EveOnlineCorporation, DateTime>> order_by_datetime = null,
            Expression<Func<EveOnlineCorporation, bool>> where = null,
            int skip = 0,
            int max_count = 0,
            int part_of = -1
            )
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.EveOnline_Corporations.AsQueryable();

            if (where != null)
                source = source.Where(where);

            if (order_by_datetime != null)
                source = source.OrderBy(order_by_datetime);

            if (part_of > 0)
            {
                int count = source.Count();
                int part_size = (count / part_of).ToInt32();
                source = source.Skip(_random.Next(0, part_of) * part_size).Take(part_size);
            }
            else
            {
                if (skip > 0)
                    source = source.Skip(skip);

                if (max_count > 0)
                    source = source.Take(max_count);
            }

            return source.Select(x => x.corporation_id).ToList();
        }

        public void Corporation_Delete(int corporation_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var corporation = _eveOnlinePublicContext.EveOnline_Corporations
                .FirstOrDefault(x => x.corporation_id == corporation_id);

            if (corporation != null)
            {
                _eveOnlinePublicContext.EveOnline_Corporations.Remove(corporation);
                _eveOnlinePublicContext.SaveChanges();
            }
        }

        public bool Corporation_Update_PublicInfo(int corporation_id, CorporationInfoResult data, bool newCreated=false)
        {
            var success = false;
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var corporation = _eveOnlinePublicContext.EveOnline_Corporations.FirstOrDefault(x => x.corporation_id == corporation_id);

            if (corporation != null)
            {
                var compareResult = corporation.UpdateProperties(data);

                if (!compareResult.AreEqual)
                {
                    // Сохранение
                    //corporation.last_update_publicInfo = last_modified ?? DateTime.UtcNow;
                    _eveOnlinePublicContext.EveOnline_Corporations.Update(corporation);
                    success = _eveOnlinePublicContext.SaveChanges() > 0;

                    // Интеграционное событие после обновления
                    foreach (var diff in compareResult.Differences)
                    {
                        switch (diff.PropertyName)
                        {
                            // Изменение имени
                            case nameof(CorporationInfoResult.name):
                                _eventBus.Publish(new CorporationAfterUpdatedNameIntegrationEvent
                                    (corporation_id, newCreated));
                                break;

                            // Изменение альянса
                            case nameof(CorporationInfoResult.alliance_id):
                                // Запрос обновления истории альянса
                                _eventBus.Publish(new CorporationNeedUpdateAllianceHistoryIntegrationEvent(corporation_id));
                                break;

                            // Изменение количества персонажей
                            case nameof(CorporationInfoResult.member_count):
                                _eventBus.Publish(new CorporationMemberCountUpdatedIntegrationEvent(corporation_id, corporation.member_count));
                                break;

                            // Изменение ceo_id
                            case nameof(CorporationInfoResult.ceo_id):
                                Character_AddNew(data.ceo_id);
                                _eventBus.Publish(new CorporationCeoUpdatedIntegrationEvent(corporation_id, corporation.ceo_id));
                                break;

                            // Установка creator_id
                            case nameof(CorporationInfoResult.creator_id):
                                Character_AddNew(data.creator_id);
                                break;
                        }
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Установка связи killmail с группой corporations
        /// </summary>
        public void Corporations_SetLinkToKillmail(int killmail_id, List<int> corporations_id)
        {
            Parallel.ForEach(corporations_id, corporation_id => 
            {
                Corporation_SetLinkToKillmail(killmail_id, corporation_id);
            });
        }
        public void Corporation_SetLinkToKillmail(int killmail_id, int corporation_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            //var corporation = await _eveOnlinePublicContext.EveOnline_Corporations.Include(x => x.killmails).FirstOrDefaultAsync(x => x.corporation_id == corporation_id);
            //if (corporation != null)
            //{
            //    if (!corporation.KillmailsActivity.Any(x => x == killmail_id))
            //    {
            //        corporation.KillmailsActivity.Add(killmail_id);
            //        //_eveOnlinePublicContext.EveOnline_Corporations.Update(corporation);

            //        _eveOnlinePublicContext.Attach(corporation);
            //        _eveOnlinePublicContext.Entry(corporation).Property(x => x.KillmailsActivity).IsModified = true;
            //        await _eveOnlinePublicContext.SaveChangesAsync();
            //    }
            //}
        }

        public void Corporation_AddNew(int corporation_id, bool newCreated = false)
        {
            if (CorporationIdInRange(corporation_id))
            {
                using var _context = new PublicContext(_options);
                if (!_context.EveOnline_Corporations.Any(x => x.corporation_id == corporation_id))
                {
                    // Добвление в БД
                    var corporation = new EveOnlineCorporation() { corporation_id = corporation_id };
                    _context.EveOnline_Corporations
                        .BulkInsert(new[] { corporation }, options =>
                        {
                            options.InsertKeepIdentity = true;
                            options.AutoMapOutputDirection = false;
                        });

                    // Уведомления
                    _eventBus.Publish(new CorporationAfterAddIntegrationEvent(corporation_id, newCreated));
                    _eventBus.Publish(new CorporationNeedUpdateAllianceHistoryIntegrationEvent(corporation_id));
                }
            }
        }
        
        public void Corporation_AddNew(bool isNcp = false, params int[] corporation_ids)
        {
            using var dbContext = new PublicContext(_options);

            // Определить, какие копорации отсутствуют в БД
            var db_ids = dbContext.EveOnline_Corporations
                .Where(x => x.corporation_id >= corporation_ids.Min() && x.corporation_id <= corporation_ids.Max())
                .Select(x => x.corporation_id)
                .ToList();

            List<EveOnlineCorporation> corpIds_toAdd = corporation_ids
                .Where(x => !db_ids.Any(y => y == x))
                .Where(corporation_id => CorporationIdInRange(corporation_id))
                .Select(x => new EveOnlineCorporation() { corporation_id = x, ncp = isNcp })
                .ToList();

            dbContext.EveOnline_Corporations
                .BulkInsert(corpIds_toAdd, options => { 
                    options.InsertKeepIdentity = true; 
                    options.AutoMapOutputDirection = false; 
                });

            foreach (var corp in corpIds_toAdd)
            {
                // Уведомления
                _eventBus.Publish(new CorporationAfterAddIntegrationEvent(corp.corporation_id));
                _eventBus.Publish(new CorporationNeedUpdateAllianceHistoryIntegrationEvent(corp.corporation_id));
            }
        }

        bool CorporationIdInRange(int corporation_id)
        {
            return (corporation_id >= 1000000 && corporation_id < 2000000)
                || (corporation_id >= 98000000 && corporation_id < 99000000)
                || (corporation_id >= 100000000 && corporation_id < 2100000000);
        }

        /// <summary>
        /// Обновление истории альянсов у корпорации
        /// </summary>
        public bool Corporation_UpdateAllianceHistory(int corporation_id, List<CorporationAllianceHistoryResult.CorporationAllianceHistoryItem> data)
        {
            bool success = false;
            if(data?.Count > 0) { 

                using var dbContext = new PublicContext(_options);
                var allyHistory = dbContext.EveOnline_CorporationAllianceHistories.Where(x => x.corporation_id == corporation_id).ToList();

                // Уведомления
                List<IntegrationEvent> events = new List<IntegrationEvent>();

                var to_add = data.Where(x => !allyHistory.Any(xx => x.record_id == xx.record_id)).ToList();
                if (to_add.Any())
                {
                    bool first = true;
                    for (int i = to_add.Count - 1; i >= 0; i--)
                    {
                        // Определение миграций до и после
                        CorporationAllianceHistoryResult.CorporationAllianceHistoryItem cur_migration = to_add[i];
                        CorporationAllianceHistoryResult.CorporationAllianceHistoryItem next_migration = i > 0 ? to_add[i - 1] : default;
                        CorporationAllianceHistoryResult.CorporationAllianceHistoryItem prev_migration = i != to_add.Count - 1 && i < to_add.Count ? to_add[i + 1] : default;

                        if (first)
                        {
                            // Обновлние последней записи из бд
                            if (allyHistory.Any())
                            {
                                var prev_history_item = allyHistory.OrderByDescending(x => x.record_id).First();

                                prev_history_item.next_ally_id = cur_migration.alliance_id;
                                prev_history_item.end_date = cur_migration.start_date.AddMinutes(-1);
                                dbContext.EveOnline_CorporationAllianceHistories.Update(prev_history_item);

                                events.Add(new CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent(prev_history_item.record_id, corporation_id, null));
                            }

                            first = false;
                        }

                        // Запись на добавление
                        var item_add = new EveOnlineCorporationAllianceHistory()
                        {
                            corporation_id = corporation_id,
                            alliance_id = cur_migration.alliance_id,
                            next_ally_id = next_migration?.alliance_id ?? 0,
                            prev_ally_id = prev_migration?.alliance_id ?? 0,
                            end_date = next_migration?.start_date.AddMinutes(-1),
                            is_deleted = cur_migration.is_deleted,
                            record_id = cur_migration.record_id,
                            start_date = cur_migration.start_date
                        };

                        dbContext.EveOnline_CorporationAllianceHistories.Add(item_add);
                        if (item_add.end_date != null)
                            events.Add(new CorporationAfterAllianceHistoryItemUpdateEndDateIntegrationEvent(item_add.record_id, corporation_id,
                                /* Last Actions */
                                item_add.start_date,
                                allyHistory.Count > 0 && next_migration == null
                                ));
                    }
                }

                // Сохранение времени последней проверки истории альянсов у корпорации
                var corpInfo = new EveOnlineCorporation() { corporation_id = corporation_id, lastUpdate_allianceHistory = DateTime.UtcNow };
                dbContext.EveOnline_Corporations.Attach(corpInfo);
                dbContext.Entry(corpInfo).Property(p => p.lastUpdate_allianceHistory).IsModified = true;

                success = dbContext.SaveChanges() > 0;

                // Если нужно уведомлять об обновление истории альянса у корпорации
                if (events.Any())
                    events.ForEach(@event => _eventBus.Publish(@event));
            }

            return success;
        }

        public bool Corporation_Update_Ncp(CorporationNpccorpsResult data)
        {
            bool success = false;
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var db_ncp_corps = _eveOnlinePublicContext.EveOnline_Corporations.Where(x => x.ncp).Select(x => x.corporation_id).ToList();

            var comparation = db_ncp_corps.UpdateProperties_NonBased(data, makeUpdates: false);
            if (!comparation.AreEqual)
            {
                foreach (var diff in comparation.Differences)
                {
                    if (diff.ChildPropertyName == "Count")
                        continue;

                    if (diff.ChildPropertyName == "Item-toAdd")
                    {
                        int new_corp_id = diff.Object2.ToInt32();
                        Corporation_AddNew(new_corp_id);

                        var corp = _eveOnlinePublicContext.EveOnline_Corporations.FirstOrDefault(x => x.corporation_id == new_corp_id);

                        if (corp != null)
                        {
                            corp.ncp = true;
                            _eveOnlinePublicContext.EveOnline_Corporations.Update(corp);
                            var _success = _eveOnlinePublicContext.SaveChanges() > 0;
                            if (!success)
                                success = _success;
                        }
                    }

                    if (diff.ChildPropertyName == "Item-toRemove")
                    {
                        int corp_id = diff.Object1.ToInt32();
                        var corp = _eveOnlinePublicContext.EveOnline_Corporations.FirstOrDefault(x => x.corporation_id == corp_id);
                        corp.ncp = false;

                        _eveOnlinePublicContext.EveOnline_Corporations.Update(corp);
                        var _success = _eveOnlinePublicContext.SaveChanges() > 0;
                        if (!success)
                            success = _success;
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Определение альянса корпорации по дате-время
        /// </summary>
        public int Corporation_AllianceOnDate(int corporation_id, DateTime date)
        {
            int alliance_id = 0;

            if (corporation_id > 0)
            {
                using var dbContext = new PublicContext(_options);

                // Запрос истории по корпорации
                var localHistory = dbContext.EveOnline_CorporationAllianceHistories
                    .AsNoTracking()
                    .Where(x => x.corporation_id == corporation_id && x.start_date <= date && (x.end_date == null || x.end_date > date))
                    .ToList();

                if (localHistory.Any())
                {
                    // Выбор записи
                    var hRecord = localHistory.OrderBy(x => x.record_id).Last();
                    alliance_id = hRecord.alliance_id;
                }
            }

            return alliance_id;
        }

        public List<int> Corporation_MembersOnDate(int corporation_id, DateTime date)
        {
            using var dbContext = new PublicContext(_options);

            // Запрос по истории
            var migrationRecords = dbContext.EveOnline_CharactersCorporationHistory
                .AsNoTracking()
                .Select(x => new { 
                    x.corporation_id,
                    x.start_date,
                    x.end_date,
                    x.character_id
                })
                .Where(x => x.corporation_id == corporation_id && x.start_date <= date && (x.end_date == null && x.end_date > date))
                .Select(x => x.character_id)
                .ToList();

            if (migrationRecords.Any())
                return migrationRecords;

            return new List<int>();
        }

        public int Corporation_MembersCountOnDate(int corporation_id, DateTime date)
        {
            return Corporation_MembersOnDate(corporation_id, date).Count;
        }

        public EveOnlineCorporationAllianceHistory Corporation_AllianceHistoryItem(int record_id)
        {
            using var dbContext = new PublicContext(_options);
            return dbContext.EveOnline_CorporationAllianceHistories.FirstOrDefault(x => x.record_id == record_id);
        }
    }
}
