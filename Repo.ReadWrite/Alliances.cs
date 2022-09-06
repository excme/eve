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
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using eveDirect.Databases.Contexts;

namespace eveDirect.Repo.ReadWrite
{
    public partial class ReadWriteRepo : IReadWrite
    {
        public List<int> Alliance_Ids(
            Expression<Func<EveOnlineAlliance, DateTime?>> order_by_datetime = null,
            Expression<Func<EveOnlineAlliance, bool>> where = null,
            int max_count = 0,
            int part_of = -1
            )
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.EveOnline_Alliances.AsQueryable();
            if (order_by_datetime != null)
                source = source.OrderBy(order_by_datetime);
            if (where != null)
                source = source.Where(where);

            if (part_of > 0)
            {
                int count = source.Count();
                int part_size = (count / part_of).ToInt32();
                source = source.Skip(_random.Next(0, part_of) * part_size).Take(part_size);
            }
            else
            {
                if (max_count > 0)
                    source = source.Take(max_count);
            }

            return source.Select(x => x.alliance_id).ToList();
        }

        public void Alliance_AddNew(int alliance_id, bool is_active = false, bool newCreated = false)
        {
            // Проверка в диапозоне
            if (AllianceIdInRange(alliance_id))
            {
                using var _context = new PublicContext(_options);
                if (!_context.EveOnline_Alliances.Any(x => x.alliance_id == alliance_id))
                {
                    var alliance = new EveOnlineAlliance() { alliance_id = alliance_id, active = is_active };
                    //_context.EveOnline_Alliance.Add(alliance);
                    //_context.SaveChanges();
                    _context.EveOnline_Alliances.BulkInsert(new[] { alliance }, options =>
                    {
                        options.InsertKeepIdentity = true;
                        options.AutoMapOutputDirection = false;
                    });

                    // Событие о новом альянсе
                    _eventBus.Publish(new AllianceAfterAddIntegrationEvent(alliance_id, newCreated));
                }
            }
        }

        public int Alliance_AddNew(List<int> alliance_ids)
        {
            List<int> alliances_to_add = new List<int>();

            using (var _eveOnlinePublicContext = new PublicContext(_options)){
                var db_ids = _eveOnlinePublicContext.EveOnline_Alliances.Select(x => x.alliance_id).ToList();
                alliances_to_add = alliance_ids.Where(x => !db_ids.Any(y => y == x)).ToList();
            }

            foreach (int alliance_id in alliances_to_add)
                Alliance_AddNew(alliance_id, true);

            return alliances_to_add.Count;
        }

        bool AllianceIdInRange(int alliance_id)
        {
            return (alliance_id >= 99000000 && alliance_id < 100000000)
                || (alliance_id >= 100000000 && alliance_id < 2100000000);
        }

        public void Alliance_Delete(int alliance_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var alliance = _eveOnlinePublicContext.EveOnline_Alliances
                .FirstOrDefault(x => x.alliance_id == alliance_id);

            if (alliance != null)
            {
                _eveOnlinePublicContext.EveOnline_Alliances.Remove(alliance);
                _eveOnlinePublicContext.SaveChanges();
            }
        }

        public bool Alliance_Update(int alliance_id, AllianceInfoResult data, bool new_Created = false)
        {
            bool result = false;
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var alliance = _eveOnlinePublicContext.EveOnline_Alliances.FirstOrDefault(x => x.alliance_id == alliance_id);

            if (alliance != null)
            {
                var compareResult = alliance.UpdateProperties(data);

                if (!compareResult.AreEqual)
                {
                    // Сохранение
                    //alliance.last_info_updated = DateTime.UtcNow;
                    _eveOnlinePublicContext.EveOnline_Alliances.Update(alliance);
                    result = _eveOnlinePublicContext.SaveChanges() > 0;

                    // Интеграционное событие после обновления
                    foreach (var diff in compareResult.Differences)
                    {
                        switch (diff.PropertyName)
                        {
                            // Изменение название
                            case nameof(AllianceInfoResult.name):
                                _eventBus.Publish(new AllianceAfterUpdatedNameIntegrationEvent(alliance_id, new_Created));
                                break;

                            // Проверка наличия в базе creator_corporation
                            case nameof(AllianceInfoResult.creator_corporation_id):
                                _eventBus.Publish(new CorporationAfterAddIntegrationEvent(alliance.creator_corporation_id));
                                break;

                            // Проверка наличия в базе executor_corporation
                            case nameof(AllianceInfoResult.executor_corporation_id):
                                _eventBus.Publish(new CorporationAfterAddIntegrationEvent(alliance.executor_corporation_id));
                                break;

                            // Проверка наличия в базе creator
                            case nameof(AllianceInfoResult.creator_id):
                                _eventBus.Publish(new CharacterAfterAddIntegrationEvent(alliance.creator_id));
                                break;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Установка связи killmail с группой alliances
        /// </summary>
        public void Alliances_SetLinkToKillmail(int killmail_id, List<int> alliances_id)
        {
            //await alliances_id.ParallelForEachAsync(async alliance_id =>
            Parallel.ForEach(alliances_id, alliance_id =>
            {
                Alliance_SetLinkToKillmail(killmail_id, alliance_id);
            });
        }

        public void Alliance_SetLinkToKillmail(int killmail_id, int alliance_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            //var alliance = await _eveOnlinePublicContext.EveOnline_Alliance.Include(x => x.killmails).FirstOrDefaultAsync(x => x.alliance_id == alliance_id);
            //if (alliance != null)
            //{
            //if (!alliance.killmails.Any(x => x == killmail_id))
            //{
            //    alliance.killmails.Add(killmail_id);
            //    //_eveOnlinePublicContext.EveOnline_Alliance.Update(alliance);

            //    _eveOnlinePublicContext.Attach(alliance);
            //    _eveOnlinePublicContext.Entry(alliance).Property(x => x.killmails).IsModified = true;
            //    await _eveOnlinePublicContext.SaveChangesAsync();
            //}
            //}
        }

        //public bool Alliance_CorporationsUpdated(int alliance_id)
        //{
        //    bool success = false;
        //    using var _eveOnlinePublicContext = new PublicContext(_options);
        //    var alliance = _eveOnlinePublicContext.EveOnline_Alliance.FirstOrDefault(x => x.alliance_id == alliance_id);

        //    if (alliance != null)
        //    {
        //        alliance.last_corps_list_update = DateTime.UtcNow;
        //        _eveOnlinePublicContext.EveOnline_Alliance.Update(alliance);
        //        success = _eveOnlinePublicContext.SaveChanges() > 0;
        //    }
        //    return success;
        //}

        public int Alliance_UpdateCorpations(int alliance_id, List<int> corporation_ids)
        {
            using var _context = new PublicContext(_options);
            // Запрос корпораций альянса в БД
            var db_corps = _context.EveOnline_Corporations
                .Where(x => x.alliance_id == alliance_id)
                .Select(x => new
                {
                    x.corporation_id,
                    x.alliance_id
                })
                .ToList();

            // Выбывшие
            var out_corps = db_corps.Select(xx => xx.corporation_id).Where(x => !corporation_ids.Contains(x)).ToList();
            foreach (var corp_id in out_corps)
            {
                // У выбывших необходимо обновить публ. инфо, чтобы определить новый альянс
                var @event = new CorporationNeedUpdatePublicInfoIntegrationEvent(corp_id);
                _eventBus.Publish(@event);
            }
            
            // Вступившие
            var in_corps = corporation_ids.Where(x => !db_corps.Select(xx => xx.corporation_id).Contains(x)).ToList();
            foreach (var corp_id in in_corps)
            {
                var corp = new EveOnlineCorporation() { corporation_id = corp_id, alliance_id = alliance_id };
                _context.EveOnline_Corporations.Attach(corp);
                _context.Entry(corp).Property(p => p.alliance_id).IsModified = true;
            }
            _context.SaveChanges();

            foreach(var corp_id in in_corps)
            {
                // У вступивших необходимо обновить историю альянсов
                var @event = new CorporationNeedUpdateAllianceHistoryIntegrationEvent(corp_id);
                _eventBus.Publish(@event);
            }

            return in_corps.Count + out_corps.Count;
        }
        //void Alliance_AddMemberMigrationRecord(int alliance_id, int record_id)
        //{
        //    if (alliance_id > 0)
        //    {
        //        using var _eveOnlinePublicContext = new PublicContext(_options);
        //        var alliance = _eveOnlinePublicContext.EveOnline_Alliance.FirstOrDefault(x => x.alliance_id == alliance_id);

        //        // Если такого альянса нет
        //        if (alliance == null)
        //        {
        //            Alliance_AddNew(alliance_id);
        //            alliance = _eveOnlinePublicContext.EveOnline_Alliance.FirstOrDefault(x => x.alliance_id == alliance_id);
        //        }

        //        if (alliance?.membersMigrations == null)
        //            alliance.membersMigrations = new List<int>();

        //        if (!alliance?.membersMigrations?.Any(x => x == record_id) ?? false)
        //        {
        //            alliance.membersMigrations.Add(record_id);
        //            alliance.membersMigrations = alliance.membersMigrations.OrderByDescending(x => x).ToList();
        //            _eveOnlinePublicContext.EveOnline_Alliance.Update(alliance);
        //            _eveOnlinePublicContext.SaveChanges();
        //        }
        //    }
        //}

        public void Alliance_UpdatePreview(int alliance_id)
        {
            using var dbContext = new PublicContext(_options);
            var alliance = dbContext.EveOnline_Alliances.FirstOrDefault(x => x.alliance_id == alliance_id);
            if (alliance != null)
            {
                if (alliance.preview == null)
                    alliance.preview = new EveOnlineAlliancePreview();

                //alliance.preview.killmails_count = alliance.KillmailsActivity.Count;

                //_eveOnlinePublicContext.EveOnline_Alliance.Update(alliance);
                dbContext.Attach(alliance);
                dbContext.Entry(alliance).Property(x => x.preview).IsModified = true;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Альянс. Количество персонажей по дате
        /// </summary>
        public int Alliance_CountCharactersOnDate(int alliance_id, DateTime date)
        {
            return Alliance_CharactersOnDate(alliance_id, date).Count;
        }

        /// <summary>
        /// Альянс. Ид персонажей по дате
        /// </summary>
        public List<int> Alliance_CharactersOnDate(int alliance_id, DateTime date)
        {
            var characters = new List<int>();
            using var dbContext = new PublicContext(_options);

            // Получение корпораций
            var corps = Alliance_CorporationsOnDate(alliance_id, date);

            // Получение
            foreach(var corp_id in corps)
                characters.AddRange(Corporation_MembersOnDate(corp_id, date));

            return characters.Distinct().ToList();
        }

        /// <summary>
        /// Альянс. Члены-корпорации по дате
        /// </summary>
        public List<int> Alliance_CorporationsOnDate(int alliance_id, DateTime date) {
            using var dbContext = new PublicContext(_options);

            // Запрос по истории
            var migrationRecords = dbContext.EveOnline_CorporationAllianceHistories
                .AsNoTracking()
                .Select(x => new {
                    x.alliance_id,
                    x.start_date,
                    x.end_date,
                    x.corporation_id
                })
                .Where(x => x.alliance_id == alliance_id && x.start_date <= date && (x.end_date == null && x.end_date > date))
                .Select(x => x.corporation_id)
                .ToList();

            if (migrationRecords.Any())
                return migrationRecords;

            return new List<int>();
        }

        /// <summary>
        /// Альянс. Количество корпораций по дате
        /// </summary>
        public int Alliance_CountCorporationsOnDate(int alliance_id, DateTime date)
        {
            return Alliance_CorporationsOnDate(alliance_id, date).Count;
        }
    }
}
