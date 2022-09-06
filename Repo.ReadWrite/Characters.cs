using eveDirect.Shared.CompareObjects;
using eveDirect.Databases.Contexts;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using eveDirect.Shared;
using eveDirect.Shared.EventBus.Events;
using eveDirect.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eveDirect.Repo.ReadWrite
{
    public partial class ReadWriteRepo : IReadWrite
    {
        public void Character_UpdatePreview(int character_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var character = _eveOnlinePublicContext.EveOnline_Characters.FirstOrDefault(x => x.character_id == character_id);
            if (character != null)
            {
                if (character.preview == null)
                    character.preview = new EveOnlineCharacterPreview();

                character.preview.killmails_count = character.killmails.Count;
                character.preview.corp_movings = _eveOnlinePublicContext.EveOnline_CharactersCorporationHistory.Count(x => x.character_id == character_id);

                //_eveOnlinePublicContext.EveOnline_Characters.Update(character);
                _eveOnlinePublicContext.Attach(character);
                _eveOnlinePublicContext.Entry(character).Property(x => x.preview).IsModified = true;

                _eveOnlinePublicContext.SaveChanges();
            }
        }

        public bool Character_UpdateCorporationHistory(int character_id, List<CharacterCorporationHistoryResult.CharacterCorporationHistoryItem> data)
        {
            var success = false;
            List<IntegrationEvent> events = new List<IntegrationEvent>();

            if (data?.Count > 0)
            {
                using var dbContext = new PublicContext(_options);
                var corpHistory = dbContext.EveOnline_CharactersCorporationHistory
                    .Where(x => x.character_id == character_id)
                    .ToList();

                // Перебираем историю корпораций персонажа
                // Перебираем новые записи в обратном порядке, чтобы воспроизвести, как они были по очередности
                for (int index = data.Count - 1; index >= 0; index--)
                {
                    var cur = data[index];
                    var next = index > 0 ? data[index - 1] : default;
                    var prev = index != data.Count - 1 ? data[index + 1] : default;

                    // Смотрим эту запись в бд
                    var cur_db_item = corpHistory.FirstOrDefault(x => x.record_id == cur.record_id);

                    if (cur_db_item != null)
                    {
                        // Если появилась след. корпорация
                        if (next != null && cur_db_item.next_corp_id == 0 && cur_db_item.next_corp_id != next.corporation_id)
                        {
                            cur_db_item.next_corp_id = next.corporation_id;
                            cur_db_item.end_date = next.start_date.AddMinutes(-1);
                            cur_db_item.just_newborn = data.Count == 1;

                            dbContext.EveOnline_CharactersCorporationHistory.Update(cur_db_item);
                            events.Add(new CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent(character_id, null));

                        }
                    }
                    else
                    {
                        // Если записи нет в БД, то добавляем
                        var new_history_item = new EveOnlineCharacterCorpHistory()
                        {
                            character_id = character_id,

                            // Текущая корпорация
                            corporation_id = cur.corporation_id,
                            // Пред. корпорация
                            prev_corp_id = prev?.corporation_id ?? 0,
                            // След. корпорация
                            next_corp_id = next?.corporation_id ?? 0,
                            end_date = next?.start_date.AddMinutes(-1),
                            is_deleted = cur.is_deleted,
                            record_id = cur.record_id,
                            start_date = cur.start_date,
                            just_newborn = data.Count == 1
                        };

                        dbContext.EveOnline_CharactersCorporationHistory.Add(new_history_item);
                        events.Add(new CharacterAfterUpdatedCorporationHistoryItemIntegrationEvent(character_id,
                            /* Last Actions */
                            new_history_item.start_date,
                            corpHistory.Count > 0 && next == null));
                    }
                }

                // Обновление количества записей миграций у персонажа
                var ch = new EveOnlineCharacter() { 
                    character_id = character_id, 
                    corpHistoryCount = data.Count, 
                    lastupdate_corphistory = DateTime.UtcNow 
                };
                dbContext.EveOnline_Characters.Attach(ch);
                dbContext.Entry(ch).Property(p => p.corpHistoryCount).IsModified = true;
                dbContext.Entry(ch).Property(p => p.lastupdate_corphistory).IsModified = true;

                // Завершение
                success = dbContext.SaveChanges() > 0;

                // Уведомление подписчиков
                if (success)
                    events.ForEach(@event => _eventBus.Publish(@event));

                // Если был добавлен хотя бы один, то мы должны известить слушаетелей, что изменился totalCount
                //if (corpHistory.Count < data.Count)
                //    _eventBus.Publish(new CharacterCorpHistoryItemsCountIntegrationEvent(dbContext.EveOnline_CharactersCorporationHistory.Count()));
            }

            return success;
        }

        public void Character_AddNew(List<int> character_ids)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);

            var db_ids = _eveOnlinePublicContext.EveOnline_Characters
                .Where(x => x.character_id >= character_ids.Min() && x.character_id <= character_ids.Max())
                .Select(x => x.character_id)
                .ToList();

            List<int> characters_to_add = character_ids.Where(x => !db_ids.Any(y => y == x)).ToList();

            var characters = characters_to_add.Select(x => new EveOnlineCharacter() { character_id = x }).ToList();
            _eveOnlinePublicContext.EveOnline_Characters.AddRange(characters);

            // Сохраняем
            _eveOnlinePublicContext.SaveChanges();

            // Уведомляем подписчиков
            foreach (int character_id in characters_to_add)
                _eventBus.Publish(new CharacterAfterAddIntegrationEvent(character_id));

        }
        bool Character_InRange(int character_id)
        {
            return (character_id >= 3000000 && character_id < 4000000)
                || (character_id >= 90000000 && character_id < 98000000)
                || (character_id >= 100000000 && character_id < 2100000000)
                || (character_id >= 210000000 && character_id < 2147483647);
        }
        /// <summary>
        /// Удаление персонажа
        /// </summary>
        public void Character_Delete(int character_id)
        {
            using var _context = new PublicContext(_options);
            //var character = _context.EveOnline_Characters.FirstOrDefault(x => x.character_id == character_id);
            //if (character != null)
            //{
            //    _context.EveOnline_Characters.Remove(character);
            //    _context.SaveChanges();
            //}

            _context.EveOnline_Characters.Where(x => x.character_id == character_id).DeleteFromQuery();
        }
        public bool Character_PublicInformation_Update(int character_id, CharacterInfoResult characterInfo)
        {
            bool success = false;
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var db_character = _eveOnlinePublicContext.EveOnline_Characters.FirstOrDefault(x => x.character_id == character_id);

            if (db_character != null)
            {
                var compareResult = db_character.UpdateProperties(characterInfo);
                if (!compareResult.AreEqual)
                {
                    // Сохранение
                    //db_character.last_update_publicInfo = DateTime.UtcNow;
                    _eveOnlinePublicContext.EveOnline_Characters.Update(db_character);
                    success = _eveOnlinePublicContext.SaveChanges() > 0;

                    // Интеграционное событие после обновления
                    foreach (var diff in compareResult.Differences)
                    {
                        switch (diff.PropertyName)
                        {
                            // Изменение названия
                            case nameof(CharacterInfoResult.name):
                                _eventBus.Publish(new CharacterAfterUpdatedNameIntegrationEvent
                                    (character_id));
                                break;

                            // Изменение alliance
                            case nameof(CharacterInfoResult.alliance_id):
                                _eventBus.Publish(new CharacterAfterUpdatedAllianceIntegrationEvent(character_id, characterInfo.alliance_id));
                                break;

                            // Изменение корпорации
                            case nameof(CharacterInfoResult.corporation_id):
                                _eventBus.Publish(new CharacterAfterUpdatedCorporationIntegrationEvent(character_id, characterInfo.corporation_id));
                                break;

                            // Изменение security_status
                            case nameof(CharacterInfoResult.security_status):
                                _eventBus.Publish(new CharacterIpdateSecurityStatusIntergrationEvent(character_id));
                                break;
                        }
                    }
                }
            }

            return success;
        }
        public int Character_UpdateAffiliation(CharacterAffiliationResult data, DateTime on_date)
        {
            using var context = new PublicContext(_options);
            var db_character_affiliation = context.EveOnline_Characters
                .Select(x => new EveOnlineCharacter()
                {
                    character_id = x.character_id,
                    corporation_id = x.corporation_id,
                    alliance_id = x.alliance_id,
                    faction_id = x.faction_id,
                })
                .Where(x => data.Select(y => y.character_id).Contains(x.character_id))
                .ToList();

            // Сбор изменений
            var genericDifferences = new List<(int character_id, Difference difference)>();

            // Перебор и обновление
            foreach (var affiliation in data)
            {
                var db_value = db_character_affiliation.FirstOrDefault(x => x.character_id == affiliation.character_id);
                if (db_value == null)
                    continue;

                // Сравнение
                var compareResult = db_value.UpdateProperties_NonBased(affiliation);
                if (!compareResult.AreEqual)
                {
                    foreach (var diff in compareResult.Differences)
                    {
                        switch (diff.PropertyName)
                        {
                            // Изменение корпорации
                            case nameof(CharacterAffiliationResult.CharacterAffiliationItem.corporation_id):
                                context.Entry(db_value).Property(p => p.corporation_id).IsModified = true;
                                break;

                            // Изменение альянса
                            case nameof(CharacterAffiliationResult.CharacterAffiliationItem.alliance_id):
                                context.Entry(db_value).Property(p => p.alliance_id).IsModified = true;
                                break;

                            // Изменение фракции
                            case nameof(CharacterAffiliationResult.CharacterAffiliationItem.faction_id):
                                context.Entry(db_value).Property(p => p.faction_id).IsModified = true;
                                break;
                        }

                        genericDifferences.Add((db_value.character_id, diff));
                    }
                }
            }

            var countUpdates = context.SaveChanges();

            // Уведомление подписчиов
            foreach (var diff in genericDifferences)
            {
                switch (diff.difference.PropertyName)
                {
                    // Изменение корпорации
                    case nameof(CharacterAffiliationResult.CharacterAffiliationItem.corporation_id):
                        _eventBus.Publish(new CharacterAfterUpdatedCorporationIntegrationEvent(diff.character_id, diff.difference.Object2Value.ToInt32()));
                        break;

                    // Изменение альянса
                    case nameof(CharacterAffiliationResult.CharacterAffiliationItem.alliance_id):
                        _eventBus.Publish(new CharacterAfterUpdatedAllianceIntegrationEvent(diff.character_id, diff.difference.Object2Value.ToInt32()));
                        break;

                    // Изменение фракции
                    case nameof(CharacterAffiliationResult.CharacterAffiliationItem.faction_id):
                        _eventBus.Publish(new CharacterAfterUpdatedFactionIntegrationEvent(diff.character_id, diff.difference.Object2Value.ToInt32()));
                        break;
                }
            }

            return countUpdates;
        }

        public List<int> Character_Ids(
            Expression<Func<EveOnlineCharacter, DateTime>> order_by_datetime = null,
            Expression<Func<EveOnlineCharacter, bool>> where = null,
            int max_count = 0,
            int skip = 0,
            int part_of = -1)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var source = _eveOnlinePublicContext.EveOnline_Characters.AsNoTracking().AsQueryable();

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
                if (skip > 0)
                    source = source.Skip(skip);

                if (max_count > 0)
                    source = source.Take(max_count);
            }

            return source.Select(x => x.character_id).ToList();
        }
        public void Character_AddNew(int character_id, bool newBorn = false)
        {
            // Проверка в диапозоне
            if (Character_InRange(character_id))
            {
                using var _context = new PublicContext(_options);
                if (!_context.EveOnline_Characters.Any(x => x.character_id == character_id))
                {
                    EveOnlineCharacter character = new EveOnlineCharacter() { character_id = character_id };
                    _context.EveOnline_Characters.BulkInsert(new[] { character }, options =>
                    {
                        options.InsertKeepIdentity = true;
                        options.AutoMapOutputDirection = false;
                    });

                    // Event
                    _eventBus.Publish(new CharacterAfterAddIntegrationEvent(character_id, newBorn));
                }
            }
        }
        /// <summary>
        /// Установка связи killmail с группой characters
        /// </summary>
        public void Characters_SetLinkToKillmail(int killmail_id, List<int> characters_ids)
        {
            //await characters_ids.ParallelForEachAsync(async character_id =>
            Parallel.ForEach(characters_ids, character_id =>
            {
                Character_SetLinkToKillmail(killmail_id, character_id);
            });
        }
        public void Character_SetLinkToKillmail(int killmail_id, int character_id)
        {
            using var _eveOnlinePublicContext = new PublicContext(_options);
            var character = _eveOnlinePublicContext.EveOnline_Characters.FirstOrDefault(x => x.character_id == character_id);
            if (character != null)
            {
                if (character.killmails == null)
                    character.killmails = new List<int>();
                //if (!character.killmails.Any(x => x.killmail_id == killmail_id))
                //{
                //    character.killmails.Add(killmail_id);
                //    //_eveOnlinePublicContext.EveOnline_Characters.Update(character);

                //    _eveOnlinePublicContext.Attach(character);
                //    _eveOnlinePublicContext.Entry(character).Property(x => x.killmails).IsModified = true;
                //    await _eveOnlinePublicContext.SaveChangesAsync();
                //}
            }
        }

        int Character_CorporationOnDate(int character_id, DateTime date)
        {
            int corporation_id = 0;
            using var dbContext = new PublicContext(_options);

            // Запрос истории по персонажу
            var localHistory = dbContext.EveOnline_CharactersCorporationHistory
                .AsNoTracking()
                .Where(x => x.character_id == corporation_id && x.start_date <= date && (x.end_date == null || x.end_date > date))
                .ToList();

            if (localHistory.Any())
            {
                // Выбор записи
                var hRecord = localHistory.OrderBy(x => x.record_id).Last();
                corporation_id = hRecord.corporation_id;
            }

            return corporation_id;
        }


    }
}
