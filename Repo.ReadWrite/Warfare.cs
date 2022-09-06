using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared;
using eveDirect.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static eveDirect.Shared.EsiConnector.Models.KillMailInfoResult;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Databases.Contexts;
using eveDirect.Repo.ReadWrite.IntegrationEvents;

namespace eveDirect.Repo.ReadWrite
{
    public partial class ReadWriteRepo : IReadWrite
    {
        public void War_AddNew(int war_id)
        {
            if (war_id > 0)
            {
                using var _eveOnlinePublicContext = new PublicContext(_options);
                EveOnlineWar war = _eveOnlinePublicContext.Eveonline_Wars.FirstOrDefault(x => x.war_id == war_id);
                if (war == null)
                {
                    war = new EveOnlineWar() { war_id = war_id };
                    _eveOnlinePublicContext.Eveonline_Wars.Add(war);
                    _eveOnlinePublicContext.SaveChanges();

                    // Событие
                    var @event = new WarAddNewIntegrationEvent(war_id);
                    _eventBus.Publish(@event);
                }
            }
        }
        public void War_Update(int war_id, WarInfoResult to_update)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            EveOnlineWar war = _eveOnlinePublicContext.Eveonline_Wars.FirstOrDefault(x => x.war_id == war_id);
            if (war != null)
            {
                var compareResult = war.UpdateProperties(to_update);
                if (!compareResult.AreEqual)
                {
                    // Сохранение
                    _eveOnlinePublicContext.Eveonline_Wars.Update(war);
                    _eveOnlinePublicContext.SaveChanges();

                    // Интеграционное событие после обновления
                    //foreach (var diff in compareResult.Differences)
                    //{
                    //    switch (diff.PropertyName)
                    //    {
                    //        case nameof(WarInfoResult.finished):
                    //            var @event = new WarFinishedIntegrationEvent(war_id);
                    //            _eventBus.Publish(@event);
                    //            break;
                    //    }
                    //}
                    var @event = new WarUpdateInfoIntegrationEvent(war_id);
                    _eventBus.Publish(@event);
                }
            }
        }

        public int War_GetLast()
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var last_war_id = _eveOnlinePublicContext.Eveonline_Wars.OrderByDescending(x => x.war_id).FirstOrDefault();
            return last_war_id?.war_id ?? 0;
        }

        public List<int> War_Ids(Expression<Func<EveOnlineWar, bool>> where = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.Eveonline_Wars.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return source.Select(x => x.war_id).ToList();
        }

        public void War_Remove(int war_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var war_to_remove = _eveOnlinePublicContext.Eveonline_Wars.FirstOrDefault(x => x.war_id == war_id);
            if (war_to_remove != null)
            {
                _eveOnlinePublicContext.Eveonline_Wars.Remove(war_to_remove);
                _eveOnlinePublicContext.SaveChanges();
            }
        }

        public List<EveOnlineWar> War_Get(Expression<Func<EveOnlineWar, bool>> where)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.Eveonline_Wars.AsQueryable();

            if (where != null)
                source = source.Where(where);

            return source.ToList();
        }

        //public List<int>> Killmails_Ids(int from_id, int to_id)
        //{
        //    if (from_id < to_id)
        //    {
        //        using var _eveOnlinePublicContext = new PublicContext(_options);
        //        var source = _eveOnlinePublicContext.Eveonline_KillMails.Where(killmail => killmail.killmail_id >= from_id && killmail.killmail_id < to_id).Select(x => x.killmail_id).ToList();

        //        return source;
        //    }

        //    throw new ArgumentException($"{nameof(from_id)} не может быть больше или равно {nameof(to_id)}");
        //}

        //public int> Killmails_LastId()
        //{
        //    using var _eveOnlinePublicContext = new PublicContext(_options);
        //    var killmail = _eveOnlinePublicContext.Eveonline_KillMails
        //            .OrderByDescending(p => p.killmail_id)
        //            .FirstOrDefault();
        //    return killmail?.killmail_id ?? 0;
        //}
        //public void Killmails_Add(int killmail_id)
        //{
        //    using var _eveOnlinePublicContext = new PublicContext(_options);
        //    if (!_eveOnlinePublicContext.Eveonline_KillMails.Any(x => x.killmail_id == killmail_id))
        //    {
        //        // Если есть пробелы между новым killmail_id и последним в базе
        //        int top_killmail_id = Killmails_LastId();
        //        while (killmail_id >= top_killmail_id + 1)
        //        {
        //            top_killmail_id++;
        //            _eveOnlinePublicContext.Eveonline_KillMails.Add(new EveOnlineKillMail() { killmail_id = top_killmail_id });

        //            var @event = new KillmailAddedNewIntegrationEvent(killmail_id);
        //            _eventBus.Publish(@event);
        //        }

        //        _eveOnlinePublicContext.SaveChanges();
        //    }
        //}
        public void Killmails_Add(int killmail_id, string killmail_hash)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            if (killmail_id > 0 && !_eveOnlinePublicContext.Eveonline_KillMails.Any(x => x.killmail_id == killmail_id))
            {
                Killmails_AddWithoutCheck(killmail_id, killmail_hash);
            }
        }
        void Killmails_AddWithoutCheck(int killmail_id, string killmail_hash)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var killmail = new EveOnlineKillMail() { killmail_id = killmail_id, killmail_hash = killmail_hash };
            _eveOnlinePublicContext.Eveonline_KillMails.Add(killmail);
            _eveOnlinePublicContext.SaveChanges();

            _eventBus.Publish(new KillmailAddNewIntegrationEvent(killmail_id));
        }
        public int Killmails_Add(Dictionary<int, string> killmails)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            //Запрос отрезка killmails из базы.Определение недостающих
            var killmailIds_period = _eveOnlinePublicContext.Eveonline_KillMails.Where(x => x.killmail_id >= killmails.Keys.Min() && x.killmail_id <= killmails.Keys.Max()).Select(x => x.killmail_id).ToList();
            var not_exists = killmails.Keys.Except(killmailIds_period).ToList();
            if (not_exists?.Any() ?? false)
            {
                _eveOnlinePublicContext.Eveonline_KillMails.AddRange(not_exists.Select(x => new EveOnlineKillMail() { killmail_id = x, killmail_hash = killmails[x] }));
                var added = _eveOnlinePublicContext.SaveChanges();

                not_exists.ForEach(x => {
                    _eventBus.Publish(new KillmailAddNewIntegrationEvent(x));
                });

                return added;
            }

            return 0;
        }
        //public void Killmails_UpdateHash(int killmail_id, string killmail_hash)
        //{
        //    using var _eveOnlinePublicContext = new PublicContext(_options);
        //    if (!_eveOnlinePublicContext.Eveonline_KillMails.Any(x => x.killmail_id == killmail_id))
        //        Killmails_Add(killmail_id);

        //    var killmail = _eveOnlinePublicContext.Eveonline_KillMails.FirstOrDefault(x => x.killmail_id == killmail_id);

        //    if(killmail != null)
        //    {
        //        if(killmail.killmail_hash == null /*|| killmail.killmail_hash.Length < killmail_hash.Length*/)
        //        {
        //            killmail.killmail_hash = killmail_hash;
        //            _eveOnlinePublicContext.Eveonline_KillMails.Update(killmail);
        //            _eveOnlinePublicContext.SaveChanges();

        //            var @event = new KillmailUpdateHashIntegrationEvent(killmail_id, killmail_hash);
        //            _eventBus.Publish(@event);
        //        }
        //    }
        //}
        //public void Killmails_UpdateHash(Dictionary<int, string> killmails)
        //{
        //    if (killmails?.Any() ?? false)
        //    {
        //        using var _eveOnlinePublicContext = new PublicContext(_options);

        //        // Добавление недостающих
        //        //if (!_eveOnlinePublicContext.Eveonline_KillMails.Any(x => x.killmail_id == killmail_id))
        //        //Killmails_Add(killmail_id);

        //        // Запрос отрезка killmails из базы. Определение недостающих
        //        var killmailIds_period = _eveOnlinePublicContext.Eveonline_KillMails.Where(x => x.killmail_id >= killmails.Keys.Min() && x.killmail_id <= killmails.Keys.Max()).Select(x => x.killmail_id).ToList();
        //        var not_exists = killmails.Keys.Except(killmailIds_period).ToList();
        //        if (not_exists?.Any() ?? false)
        //            not_exists.ForEach(async killmail_id => Killmails_Add(killmail_id));

        //        // Обновление hash
        //        var db_killmails = _eveOnlinePublicContext.Eveonline_KillMails.Where(x => killmails.Keys.Contains(x.killmail_id)).ToList();
        //        if (db_killmails?.Any() ?? false)
        //        {
        //            foreach (var db_killmail in db_killmails)
        //            {
        //                if (db_killmail.killmail_hash == null /*|| db_killmail.killmail_hash.Length < db_killmail.Length*/)
        //                {
        //                    db_killmail.killmail_hash = killmails[db_killmail.killmail_id];
        //                    _eveOnlinePublicContext.Eveonline_KillMails.Update(db_killmail);
        //                }
        //            }

        //            _eveOnlinePublicContext.SaveChanges();

        //            foreach (var db_killmail in db_killmails)
        //            {
        //                var @event = new KillmailUpdateHashIntegrationEvent(db_killmail.killmail_id, db_killmail.killmail_hash);
        //                _eventBus.Publish(@event);
        //            }
        //        }
        //    }
        //}
        public Dictionary<string, int> Killmails_zKillBoardStats()
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            return _eveOnlinePublicContext.zKillBoardsItems.ToDictionary(x => x.OnDate, t => t.zKillBoard_Count);
        }
        public void Killmails_zKillBoardStatItemUpdate(string key, int value)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var item = _eveOnlinePublicContext.zKillBoardsItems.FirstOrDefault(x => x.OnDate == key);
            if (item != null)
            {
                item.zKillBoard_Count = value;
                //item.Local_Count = _eveOnlinePublicContext.Eveonline_KillMails.Where(x => x.killmail_time.)
                _eveOnlinePublicContext.zKillBoardsItems.Update(item);

            }
            else
            {
                item = new ZKillBoardStatItem()
                {
                    OnDate = key,
                    zKillBoard_Count = value
                };
                _eveOnlinePublicContext.zKillBoardsItems.Add(item);
            }
            _eveOnlinePublicContext.SaveChanges();
        }

        //public bool> Killmails_AnyToUpdate(Expression<Func<EveOnlineKillMail, bool>> where)
        //{
        //    using var _eveOnlinePublicContext = new PublicContext(_options);
        //    return _eveOnlinePublicContext.Eveonline_KillMails.Any(where);
        //}
        //public Dictionary<int, string>> Killmails_GetDic(Expression<Func<EveOnlineKillMail, bool>> where, int limit)
        //{
        //    using (var _eveOnlinePublicContext = new PublicWarsKillmailsContext(EveZoneDbOptions.Load(_connectionStrWarsKillmailsContext)))
        //    {
        //        return _eveOnlinePublicContext.Eveonline_KillMails.Where(where).Take(limit).ToDictionary(x => x.killmail_id, t => t.killmail_hash);
        //    }
        //}
        public List<EveOnlineKillMail> Killmails_Get(
            Expression<Func<EveOnlineKillMail, bool>> where,
            int take = 0,
            int skip = 0,
            Expression<Func<EveOnlineKillMail, EveOnlineKillMail>> select = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var query = _eveOnlinePublicContext.Eveonline_KillMails.AsQueryable();

            if (where != null)
                query = query.Where(where);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            if (select != null)
                query = query.Select(select);

            return query.ToList();
        }

        public List<EveOnlineKillMail> Killmails_Get(
            Expression<Func<EveOnlineKillMail, bool>> where,
            int take = 0,
            int skip = 0,
            Expression<Func<EveOnlineKillMail, EveOnlineKillMail>> select = null,
            Expression<Func<EveOnlineKillMail, EveOnlineKillMailVictim>> include = null)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var query = _eveOnlinePublicContext.Eveonline_KillMails.AsQueryable();

            if (where != null)
                query = query.Where(where);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            if (select != null)
                query = query.Select(select);

            if (include != null)
                query = query.Include(include);

            return query.ToList();
        }

        //public void Killmails_UpdateResult(int key, KillMailInfoResult data)
        //{
        //    using var _eveOnlinePublicContext = new PublicContext(_options);
        //    EveOnlineKillMail killmail = _eveOnlinePublicContext.Eveonline_KillMails.FirstOrDefault(x => x.killmail_id == key);
        //    Killmails_UpdateResult(killmail, data);
        //}
        public void Killmails_UpdateResults(ConcurrentDictionary<EveOnlineKillMail, KillMailInfoResult> updated)
        {
            if (updated?.Any() ?? false)
            {
                using var _eveOnlinePublicContext = new PublicContext(_options);
                foreach (var to_update in updated)
                {
                    var killmail = to_update.Key;
                    var data = to_update.Value;

                    killmail = Killmails_UpdateResultInner(killmail, data);

                    _eveOnlinePublicContext.Eveonline_KillMails.Update(killmail);
                }

                _eveOnlinePublicContext.SaveChanges();
            }
        }
        private EveOnlineKillMail Killmails_UpdateResultInner(EveOnlineKillMail killmail, KillMailInfoResult data)
        {
            if (killmail != null)
            {
                if (killmail.killmail_time == null)
                    killmail.killmail_time = data.killmail_time;

                if (killmail.solar_system_id == null)
                    killmail.solar_system_id = data.solar_system_id;

                if (killmail.victim == null)
                    killmail.victim = new EveOnlineKillMailVictim(data.victim);

                if (killmail.attackers == null)
                    killmail.attackers = data.attackers.Select(x => {
                        return new EveOnlineKillMailAttacker(x);
                    }).ToList();

                if (killmail.war_id == null)
                    killmail.war_id = data.war_id;

                if (killmail.moon_id == null)
                    killmail.moon_id = data.moon_id;

                killmail.victim.location_id = Killmails_GetLocationId(
                    data.victim.position?.x ?? 0,
                    data.victim.position?.y ?? 0,
                    data.victim.position?.z ?? 0,
                    data.solar_system_id
                );

                killmail.victim.items = Killmail_GetPrices(data.killmail_time, data.victim.items);
                if(killmail.victim.items?.Any() ?? false)
                {
                    killmail.total_destroyed = killmail.victim.items.Sum(x => x.CalcSumDestoryed());
                    killmail.total_dropped = killmail.victim.items.Sum(x => x.CalcSumDropped());
                    killmail.fitting = killmail.victim.items.Sum(x => x.CalcSumFitting());
                }

                //  Поиск во внутренних данных ids
                Killmails_SearcInnerIds(killmail);
            }

            return killmail;
        }
        List<EveOnlineKillMailVictimItemParent> Killmail_GetPrices(DateTime onDate, List<Item> items)
        {
            List<EveOnlineKillMailVictimItemParent> result = new List<EveOnlineKillMailVictimItemParent>();
            if(items != null)
            {
                foreach(var item in items)
                {
                    var temp = new EveOnlineKillMailVictimItemParent() {
                        singleton = item.singleton,
                        flag = item.flag,
                        item_type_id = item.item_type_id,
                        quantity_destroyed = item.quantity_destroyed,
                        quantity_dropped = item.quantity_dropped
                    };
                    temp.market_price = Market_HistoryPrice(item.item_type_id, onDate);
                    if(item.items?.Any() ?? false)
                    {
                        temp.items = new List<EveOnlineKillMailVictimItemChild>();
                        foreach (var child in item.items)
                        {
                            var tempChild = new EveOnlineKillMailVictimItemChild()
                            {
                                singleton = child.singleton,
                                flag = child.flag,
                                item_type_id = child.item_type_id,
                                quantity_destroyed = child.quantity_destroyed,
                                quantity_dropped = child.quantity_dropped
                            };
                            tempChild.market_price = Market_HistoryPrice(child.item_type_id, onDate);
                            temp.items.Add(tempChild);
                        }
                    }
                    result.Add(temp);
                }
            }

            return result;
        }

        //public void Killmails_UpdateResult(EveOnlineKillMail killmail, KillMailInfoResult data)
        //{
        //    killmail = Killmails_UpdateResultInner(killmail, data);

        //    using var _eveOnlinePublicContext = new PublicContext(_options);
        //    _eveOnlinePublicContext.Eveonline_KillMails.Update(killmail);
        //    _eveOnlinePublicContext.SaveChanges();
        //}
        public void Killmails_UpdateGetLocationId(EveOnlineKillMail killmail)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            killmail.victim.location_id = Killmails_GetLocationId(killmail.victim.position?.x ?? 0,
                    killmail.victim.position?.y ?? 0,
                    killmail.victim.position?.z ?? 0,
                    killmail.solar_system_id ?? 0);

            _eveOnlinePublicContext.Eveonline_KillMails.Update(killmail);
            _eveOnlinePublicContext.SaveChanges();
        }
        long Killmails_GetLocationId(double pos_x, double pos_y, double pos_z, int solar_system_id)
        {
            if (solar_system_id > 0)
            {
                //if (pos_x != 0 && pos_y != 0 && pos_z != 0)
                //{
                List<EveOnlineUniverseLocation> locations = Universe_InnerLocations(solar_system_id);

                double minDistance = 0;
                long nearestLocationId = 0;
                locations.ForEach(location =>
                {
                    if (location.position.x == 0 && location.position.y == 0 && location.position.z == 0 && location.type != EUniverseLocationType.Star)
                    {
                        //bool here = true;
                    }
                    else
                    {
                        var distance = Math.Sqrt(Math.Pow(location.position.x - pos_x, 2) + Math.Pow(location.position.y - pos_y, 2) + Math.Pow(location.position.z - pos_z, 2));

                        if ((minDistance == 0 && nearestLocationId == 0) || (distance <= minDistance))
                        {
                            nearestLocationId = location.id;
                            minDistance = distance;
                        }
                    }
                });

                return nearestLocationId;
                //}
            }

            return 0;
        }
        public EveOnlineKillMail Killmail_Get(int killmail_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            return _eveOnlinePublicContext.Eveonline_KillMails.FirstOrDefault(x => x.killmail_id == killmail_id);
        }
        /// <summary>
        /// Поиск внутренних char_id, corp_id, ally_id
        /// </summary>
        public void Killmails_SearcInnerIds(EveOnlineKillMail killmail)
        {
            if (killmail != null)
            {

                List<int> allies = new List<int>() { killmail.victim?.alliance_id ?? 0 };
                List<int> corps = new List<int>() { killmail.victim?.corporation_id ?? 0 };
                List<int> chars = new List<int>() { killmail.victim?.character_id ?? 0 };

                if (killmail.attackers?.Any() ?? false)
                {
                    foreach (var attacker in killmail.attackers)
                    {
                        allies.Add(attacker.alliance_id ?? 0);
                        corps.Add(attacker.corporation_id ?? 0);
                        chars.Add(attacker.character_id ?? 0);
                    }
                }

                // Сохранение
                allies = GetUniqueInts(allies);
                if (allies.Any())
                    foreach (int alliance_id in allies)
                        Alliance_AddNew(alliance_id);

                corps = GetUniqueInts(corps);
                if (corps.Any())
                    foreach (int corp_id in corps)
                        Corporation_AddNew(corp_id);


                chars = GetUniqueInts(chars);
                if (chars.Any())
                    foreach (int char_id in chars)
                        Character_AddNew(char_id);

                // Добавление связей с killmail
                Characters_SetLinkToKillmail(killmail.killmail_id, chars);
                Corporations_SetLinkToKillmail(killmail.killmail_id, corps);
                Alliances_SetLinkToKillmail(killmail.killmail_id, allies);

                //  На удаление
                using var _eveOnlinePublicContext = new PublicContext(_options);
                //killmail.updatedSearchIds = true;
                _eveOnlinePublicContext.Eveonline_KillMails.Update(killmail);
                _eveOnlinePublicContext.SaveChanges();
            }
        }
        List<int> GetUniqueInts(List<int> list)
        {
            list = list.Distinct().ToList();
            list.Remove(0);
            return list;
        }
    }
}
