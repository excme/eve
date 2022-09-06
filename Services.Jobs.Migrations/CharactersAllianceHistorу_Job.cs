using eveDirect.Databases.Contexts;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Repo.ReadWrite.IntegrationEvents;
using eveDirect.Services.Jobs.Core;
using eveDirect.Shared.EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eveDirect.Services.Jobs.Migrations
{
    /// <summary>
    /// Расчет миграций персонажей между альянсами
    /// </summary>
    public class CharactersAllianceHistorу_Job : JobBase
    {
        IReadWrite _repoPublicCommon { get; }
        DbContextOptions<PublicContext> _options { get; }
        int Part_of { get; }
        IEventBus _eventBus { get; }

        public CharactersAllianceHistorу_Job(
            IReadWrite repoPublicCommon,
            DbContextOptions<PublicContext> options,
            ILogger<CharactersAllianceHistorу_Job> logger,
            IEventBus eventBus,
            int part_of = 3000
            )
            : base(logger)
        {
            _repoPublicCommon = repoPublicCommon 
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
            _options = options 
                ?? throw new ArgumentNullException(nameof(options));
            _eventBus = eventBus 
                ?? throw new ArgumentNullException(nameof(eventBus));
            Part_of = part_of;
        }

        public override void Execute()
        {
            

            var to_updating = _repoPublicCommon.Db_SelectColumn<EveOnlineCharacterCorpHistory, int>(
                x => x.character_id,
                x => x.allyComplete == false, true);

            _jobResult.subValues.Add(new JobResult.Item() { Name = "count", Value = to_updating.Count });

            var list = AttachProgressBarToList(to_updating);
            //foreach (var character_id in list)
            Parallel.ForEach(list, character_id =>
            {
                var b = SimpleCharacter(character_id);
                if (b)
                    _jobResult.Value++;
            });
        }

        public bool SimpleCharacter(int character_id)
        {
            using var dbContext = new PublicContext(_options);

            // Получение последней даты обновление истории корпораций персонажа
            var character_Info = dbContext.EveOnline_Characters
                .Select(x => new { x.character_id, x.lastupdate_corphistory })
                .FirstOrDefault(x => x.character_id == character_id);

            if (character_Info != null)
            {
                // Если у персонажа еще не обновлялась история корпораций
                if (character_Info.lastupdate_corphistory == null)
                {
                    _eventBus.Publish(new CharacterNeedUpdateCorporationHistoryIntehrationEvent(character_id));
                    return false;
                }

                // Получение записей истории корпораций у персонажа
                var char_CorpHistory = dbContext.EveOnline_CharactersCorporationHistory
                    .Where(x => x.character_id == character_id && x.allyComplete == false)
                    .ToList()
                    .OrderBy(x => x.record_id)
                    .ToList();

                // Ncp корпорации
                var ncp_corps = dbContext.EveOnline_Corporations
                            .Select(x => new { x.corporation_id, x.ncp })
                            .Where(x => x.ncp)
                            .Select(x => x.corporation_id)
                            .ToList();

                if (char_CorpHistory?.Any() ?? false)
                {
                    int count = 0;
                    // Перебираем историю корпораций у персонажа
                    foreach (EveOnlineCharacterCorpHistory char_CorpHistory_item in char_CorpHistory)
                    {
                        // Проверка на NCP
                        if (ncp_corps.Contains(char_CorpHistory_item.corporation_id))
                        {
                            char_CorpHistory_item.allyComplete = true;
                            dbContext.Entry(char_CorpHistory_item).Property(p => p.allyComplete).IsModified = true;
                            dbContext.SaveChanges();
                            count++;
                            continue;
                        }

                        if(Action(char_CorpHistory_item))
                            count++;
                    }

                    return count > 0;
                }
            }

            return false;
        }

        //public bool SimpleCorpHistoryItem(int record_Id)
        //{
        //    using var dbContext = new PublicContext(_options);
        //    var corpHistoryItem = dbContext.EveOnline_CharactersCorporationHistory
        //        .FirstOrDefault(x => x.record_id == record_Id);

        //    if(corpHistoryItem != null)
        //    {
        //        return Action(corpHistoryItem);
        //    }

        //    return false;
        //}

        bool Action(EveOnlineCharacterCorpHistory char_CorpHistory_item)
        {
            int character_id = char_CorpHistory_item.character_id;
            using var dbContext = new PublicContext(_options);

            // Данные корпорации
            var corpInfo = dbContext.EveOnline_Corporations
                .Select(x => new { x.corporation_id, x.lastUpdate_allianceHistory })
                .FirstOrDefault(x => x.corporation_id == char_CorpHistory_item.corporation_id);

            // Если нет такой корпорации
            if(corpInfo == null)
            {
                _eventBus.Publish(new CorporationAfterAddIntegrationEvent(char_CorpHistory_item.corporation_id));
                return false;
            }

            // Если корпорация еще ни разу не обновляла историю альянсов
            if (corpInfo.lastUpdate_allianceHistory == null)
            {
                _eventBus.Publish(new CorporationNeedUpdateAllianceHistoryIntegrationEvent(char_CorpHistory_item.corporation_id));
                return false;
            }

            // Если история альянсов обновлялась ДО этой записи истории корпораций персонажа
            if((corpInfo.lastUpdate_allianceHistory < char_CorpHistory_item.start_date) || (char_CorpHistory_item.end_date != null && corpInfo.lastUpdate_allianceHistory < char_CorpHistory_item.end_date))
            {
                _eventBus.Publish(new CorporationNeedUpdateAllianceHistoryIntegrationEvent(char_CorpHistory_item.corporation_id));
                return false;
            }

            // Получение истории альянсов у корпорации в этот период
            var corp_AllyHistory = dbContext.EveOnline_CorporationAllianceHistories
                .Where(x => x.corporation_id == char_CorpHistory_item.corporation_id 
                    && x.start_date <= (char_CorpHistory_item.end_date ?? DateTime.UtcNow)
                    //&& (x.end_date == null || x.end_date >= char_CorpHistory_item.start_date))
                    && (x.end_date ?? DateTime.UtcNow) >= char_CorpHistory_item.start_date)
                .OrderBy(x => x.start_date)
                .ToList();

            // Получение истории альянсов у персонажа по списку всерху
            var character_AllyHistory = dbContext.EveDirect_CharacterAllianceHistory
                .Where(x => x.character_id == character_id
                    && corp_AllyHistory.Select(x => x.record_id).Contains(x.allyHistory_recordId))
                .ToList();

            // Сравнение записей из двух списков
            foreach(var corp_AllyHistory_item in corp_AllyHistory)
            {
                var character_AllyHistoryItem = character_AllyHistory
                    .FirstOrDefault(x => x.corpHistory_recordId == char_CorpHistory_item.record_id);

                // Если есть в списке, то сверяем поля
                if (character_AllyHistoryItem != null)
                {
                    // Если персонаж вышел из корпорации
                    if (character_AllyHistoryItem.end != char_CorpHistory_item.end_date && char_CorpHistory_item.end_date != null)
                    {
                        character_AllyHistoryItem.end = char_CorpHistory_item.end_date;
                        dbContext.EveDirect_CharacterAllianceHistory.Update(character_AllyHistoryItem);
                    }
                    // Если корпорация вышла из альянса
                    else if (character_AllyHistoryItem.end != corp_AllyHistory_item.end_date && corp_AllyHistory_item.end_date != null)
                    {
                        character_AllyHistoryItem.end = corp_AllyHistory_item.end_date;
                        dbContext.EveDirect_CharacterAllianceHistory.Update(character_AllyHistoryItem);
                    }
                }
                // Если нет в списке
                else
                {
                    if (corp_AllyHistory_item.alliance_id > 0)
                    {
                        // Добавление записи истории альянсов персонажа в БД
                        var allyРistory_Item = new EveDirectCharacterAllianceHistory()
                        {
                            character_id = character_id,
                            alliance_id = corp_AllyHistory_item.alliance_id,
                            corporation_id = char_CorpHistory_item.corporation_id,
                            start = corp_AllyHistory_item.start_date > char_CorpHistory_item.start_date ? corp_AllyHistory_item.start_date : char_CorpHistory_item.start_date,
                            end = corp_AllyHistory_item.end_date == null || corp_AllyHistory_item.end_date > char_CorpHistory_item.end_date ? char_CorpHistory_item.end_date : corp_AllyHistory_item.end_date,
                            corpHistory_recordId = char_CorpHistory_item.record_id,
                            allyHistory_recordId = corp_AllyHistory_item.record_id
                        };
                        dbContext.EveDirect_CharacterAllianceHistory.Add(allyРistory_Item);
                    }
                }

                // Если корпорация уже вышла из альянса или персонаж из корпорации, то изменений больше здесь не будет
                // И если эта запись последняя у корпорации
                if(corp_AllyHistory.IndexOf(corp_AllyHistory_item) == corp_AllyHistory.Count - 1 && (corp_AllyHistory_item.end_date != null || char_CorpHistory_item.end_date != null))
                {
                    // Запись в БД, что данная запись не требует больше проверок
                    char_CorpHistory_item.allyComplete = true;
                    dbContext.EveOnline_CharactersCorporationHistory.Attach(char_CorpHistory_item);
                    dbContext.Entry(char_CorpHistory_item).Property(x => x.allyComplete).IsModified = true;
                }
            }

            if(dbContext.ChangeTracker.HasChanges())
                return dbContext.SaveChanges() > 0;

            return false;
        }
    }
}
