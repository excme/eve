using eveDirect.Databases.Contexts;
using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Repo.ReadWrite;
using eveDirect.Shared;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.GeneralTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace eveDirect.Services.Jobs.Migrations.Tests
{
    /// <summary>
    /// Тестирование миграций персонажей между альянсами
    /// </summary>
    public class CharactersAllianceHistorу : UnitTestCore
    {
        DateTime testOnDate = new DateTime(2020, 9, 14);
        public CharactersAllianceHistorу(ITestOutputHelper output) : base(output)
        {
            _repoPublicCommon = new ReadWriteRepo(_eventBus, _publicContextOptions);
        }

        void Execute_Job()
        {
            var job = new CharactersAllianceHistorу_Job(_repoPublicCommon, _publicContextOptions, null, _eventBus);
            job.SimpleCharacter(character_id);
        }

        void Accept_CharacterAllianceHistory(string json_result)
        {
            var json_list = JsonSerializer.Deserialize<List<EveDirectCharacterAllianceHistoryData>>(json_result);
            using var dbContext = new PublicContext(_publicContextOptions);
            var db_list = dbContext.EveDirect_CharacterAllianceHistory
                .Where(x => x.character_id == character_id)
                .Select(x => new EveDirectCharacterAllianceHistoryData()
                {
                    start = x.start,
                    alliance_id = x.alliance_id,
                    allyHistory_recordId = x.allyHistory_recordId,
                    character_id = x.character_id,
                    corpHistory_recordId = x.corpHistory_recordId,
                    corporation_id = x.corporation_id,
                    end = x.end
                })
                .OrderBy(x => x.start)
                .Take(json_list.Count)
                .ToList();

            var compareResult = db_list.UpdateProperties(json_list, false, collectionSpec: new Dictionary<Type, IEnumerable<string>>
            {
                [typeof(EveDirectCharacterAllianceHistoryData)] = new List<string>() { "corpHistory_recordId", "allyHistory_recordId" }
            });

            Assert.True(compareResult.AreEqual, compareResult.DifferencesString);
        }

        void Accept_CharacterCorporationHistory(string json_result)
        {
            var json_list = JsonSerializer.Deserialize<List<EveOnlineCharacterCorpHistory>>(json_result);
            using var dbContext = new PublicContext(_publicContextOptions);
            var db_list = dbContext.EveOnline_CharactersCorporationHistory
                .Where(x => x.character_id == character_id)
                .Take(json_list.Count)
                .ToList();

            var compareResult = db_list.UpdateProperties(json_list, false, collectionSpec: new Dictionary<Type, IEnumerable<string>>
            {
                [typeof(EveOnlineCharacterCorpHistory)] = new List<string>() { "record_id" }
            });
            Assert.True(compareResult.AreEqual, compareResult.DifferencesString);
        }

        void Generic_Prepare()
        {
            EF_TruncateTables(nameof(EveOnlineCharacter), nameof(EveOnlineCharacterCorpHistory), nameof(EveDirectCharacterAllianceHistory), nameof(EveOnlineCorporation), nameof(EveOnlineCorporationAllianceHistory));
            // Вставка начальных данных
            // Публичная информация персонажа
            CharacterInfoResult charInfo = Esi_ExecuteAndReturn(connector.Character.Information(character_id));
            _repoPublicCommon.Character_AddNew(character_id);
            _repoPublicCommon.Character_PublicInformation_Update(character_id, charInfo);

            // NPC корпорации
            CorporationNpccorpsResult npcCorps = Esi_ExecuteAndReturn(connector.Corporation.NpcCorps());
            _repoPublicCommon.Corporation_AddNew(true, npcCorps.ToArray());

            // История корпораций у персонажа
            var corpHistory = Esi_ExecuteAndReturn(connector.Character.CorporationHistory(character_id))
                .Where(x => x.start_date < testOnDate)
                .ToList();
            _repoPublicCommon.Character_UpdateCorporationHistory(character_id, corpHistory);

            // История альянсов у частных корпораций
            var private_corpIds = corpHistory.Where(x => !npcCorps.Contains(x.corporation_id)).Select(x => x.corporation_id).ToList();
            private_corpIds.ForEach(corp_id =>
            {
                var corpInfo = Esi_ExecuteAndReturn(connector.Corporation.Information(corp_id));
                _repoPublicCommon.Corporation_AddNew(corp_id);
                _repoPublicCommon.Corporation_Update_PublicInfo(corp_id, corpInfo);

                var corp_AllianceHistory = Esi_ExecuteAndReturn(connector.Corporation.AllianceHistory(corp_id));
                _repoPublicCommon.Corporation_UpdateAllianceHistory(corp_id, corp_AllianceHistory
                    .Where(x => x.start_date < testOnDate)
                    .ToList());
            });
        }

        /// <summary>
        /// Тест, когда первое заполнение
        /// </summary>
        [Fact]
        public void Test1()
        {
            /// Подготовка
            Generic_Prepare();

            #region
            /// Выполнение
            Execute_Job();

            // Проверка
            Accept_CharacterAllianceHistory(@"[{""id"":139,""character_id"":90522832,""alliance_id"":99000597,""allyHistory_recordId"":465787,""corpHistory_recordId"":16595491,""start"":""2011-07-01T21:28:00"",""end"":""2011-08-31T05:53:00"",""corporation_id"":98010376},{""id"":140,""character_id"":90522832,""alliance_id"":1350079892,""allyHistory_recordId"":485082,""corpHistory_recordId"":16595491,""start"":""2011-09-01T15:53:00"",""end"":""2011-10-27T06:41:00"",""corporation_id"":98010376},{""id"":141,""character_id"":90522832,""alliance_id"":99001011,""allyHistory_recordId"":504845,""corpHistory_recordId"":16595491,""start"":""2011-11-09T09:51:00"",""end"":""2012-04-09T14:15:00"",""corporation_id"":98010376},{""id"":142,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":556322,""corpHistory_recordId"":20250210,""start"":""2012-07-15T09:22:00"",""end"":""2012-08-02T10:46:00"",""corporation_id"":98010376},{""id"":143,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":589914,""corpHistory_recordId"":20392133,""start"":""2012-08-02T10:47:00"",""end"":""2013-05-22T10:15:00"",""corporation_id"":98129547},{""id"":144,""character_id"":90522832,""alliance_id"":1208295500,""allyHistory_recordId"":259685,""corpHistory_recordId"":28432479,""start"":""2013-09-27T21:11:00"",""end"":""2014-01-21T17:16:00"",""corporation_id"":238942032},{""id"":145,""character_id"":90522832,""alliance_id"":99008223,""allyHistory_recordId"":1185985,""corpHistory_recordId"":38261600,""start"":""2018-04-28T18:57:00"",""end"":""2019-01-19T18:12:00"",""corporation_id"":98043813},{""id"":146,""character_id"":90522832,""alliance_id"":99003500,""allyHistory_recordId"":1009908,""corpHistory_recordId"":38261600,""start"":""2015-12-28T15:00:00"",""end"":""2018-04-27T18:09:00"",""corporation_id"":98043813},{""id"":147,""character_id"":90522832,""alliance_id"":99005528,""allyHistory_recordId"":960351,""corpHistory_recordId"":38261600,""start"":""2015-05-31T14:44:00"",""end"":""2015-12-27T14:02:00"",""corporation_id"":98043813}]");
            Accept_CharacterCorporationHistory(@"[{""record_id"":49759532,""start_date"":""2019-04-22T20:42:00"",""corporation_id"":98377297,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":0,""end_date"":null,""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":false},
{""record_id"":16311177,""start_date"":""2011-03-11T19:29:00"",""corporation_id"":1000168,""prev_corp_id"":0,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98010376,""end_date"":""2011-04-16T09:51:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":16595491,""start_date"":""2011-04-16T09:52:00"",""corporation_id"":98010376,""prev_corp_id"":1000168,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2012-04-09T18:34:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":19431146,""start_date"":""2012-04-09T18:35:00"",""corporation_id"":1000107,""prev_corp_id"":98010376,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98010376,""end_date"":""2012-07-15T09:21:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":20250210,""start_date"":""2012-07-15T09:22:00"",""corporation_id"":98010376,""prev_corp_id"":1000107,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":98129547,""end_date"":""2012-08-02T10:46:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":20392133,""start_date"":""2012-08-02T10:47:00"",""corporation_id"":98129547,""prev_corp_id"":98010376,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2013-05-29T10:29:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":25686599,""start_date"":""2013-05-29T10:30:00"",""corporation_id"":1000107,""prev_corp_id"":98129547,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":238942032,""end_date"":""2013-09-27T21:10:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":28432479,""start_date"":""2013-09-27T21:11:00"",""corporation_id"":238942032,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2014-01-21T17:16:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":30596803,""start_date"":""2014-01-21T17:17:00"",""corporation_id"":1000107,""prev_corp_id"":238942032,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98138737,""end_date"":""2014-03-06T12:09:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":31558760,""start_date"":""2014-03-06T12:10:00"",""corporation_id"":98138737,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2014-08-01T14:19:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":34299708,""start_date"":""2014-08-01T14:20:00"",""corporation_id"":1000107,""prev_corp_id"":98138737,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98377297,""end_date"":""2015-02-09T16:06:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":37039504,""start_date"":""2015-02-09T16:07:00"",""corporation_id"":98377297,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98361101,""end_date"":""2015-02-22T19:51:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":37189554,""start_date"":""2015-02-22T19:52:00"",""corporation_id"":98361101,""prev_corp_id"":98377297,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98043813,""end_date"":""2015-05-30T12:31:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":38261600,""start_date"":""2015-05-30T12:32:00"",""corporation_id"":98043813,""prev_corp_id"":98361101,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2019-01-19T18:12:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":49190764,""start_date"":""2019-01-19T18:13:00"",""corporation_id"":1000107,""prev_corp_id"":98043813,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98377297,""end_date"":""2019-04-22T20:41:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true}]");
            #endregion
        }

        /// <summary>
        /// Дополнение к текущей истории события, когда корпорация ВЫШЛА из альянса
        /// </summary>
        [Fact]
        public void Test2()
        {
            /// Подготовка
            Generic_Prepare();
            Execute_Job();

            var corp_id = 98377297;
            // Корпорация ЗАШЛА В АЛЬЯНС
            List<CorporationAllianceHistoryResult.CorporationAllianceHistoryItem> corp_AllianceHistory = Esi_ExecuteAndReturn(connector.Corporation.AllianceHistory(corp_id)).ToList();
            corp_AllianceHistory.Insert(0, new CorporationAllianceHistoryResult.CorporationAllianceHistoryItem()
            {
                start_date = new DateTime(2020, 8, 13),
                alliance_id = 574100976,
                record_id = 19000000
            });
            // Симулированная история
            _repoPublicCommon.Corporation_UpdateAllianceHistory(corp_id, corp_AllianceHistory);
            Execute_Job();

            // Корпорация ВЫШЛА из альянса
            corp_AllianceHistory.Insert(0, new CorporationAllianceHistoryResult.CorporationAllianceHistoryItem()
            {
                start_date = new DateTime(2020, 9, 4),
                record_id = 19000100
            });
            // Симулированная история
            _repoPublicCommon.Corporation_UpdateAllianceHistory(corp_id, corp_AllianceHistory);

            #region 
            Execute_Job();

            Accept_CharacterAllianceHistory(@"[{""id"":269,""character_id"":90522832,""alliance_id"":99000597,""allyHistory_recordId"":465787,""corpHistory_recordId"":16595491,""start"":""2011-07-01T21:28:00"",""end"":""2011-08-31T05:53:00"",""corporation_id"":98010376},
{""id"":270,""character_id"":90522832,""alliance_id"":1350079892,""allyHistory_recordId"":485082,""corpHistory_recordId"":16595491,""start"":""2011-09-01T15:53:00"",""end"":""2011-10-27T06:41:00"",""corporation_id"":98010376},
{""id"":271,""character_id"":90522832,""alliance_id"":99001011,""allyHistory_recordId"":504845,""corpHistory_recordId"":16595491,""start"":""2011-11-09T09:51:00"",""end"":""2012-04-09T14:15:00"",""corporation_id"":98010376},
{""id"":272,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":556322,""corpHistory_recordId"":20250210,""start"":""2012-07-15T09:22:00"",""end"":""2012-08-02T10:46:00"",""corporation_id"":98010376},
{""id"":273,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":589914,""corpHistory_recordId"":20392133,""start"":""2012-08-02T10:47:00"",""end"":""2013-05-22T10:15:00"",""corporation_id"":98129547},
{""id"":274,""character_id"":90522832,""alliance_id"":1208295500,""allyHistory_recordId"":259685,""corpHistory_recordId"":28432479,""start"":""2013-09-27T21:11:00"",""end"":""2014-01-21T17:16:00"",""corporation_id"":238942032},
{""id"":275,""character_id"":90522832,""alliance_id"":99005528,""allyHistory_recordId"":960351,""corpHistory_recordId"":38261600,""start"":""2015-05-31T14:44:00"",""end"":""2015-12-27T14:02:00"",""corporation_id"":98043813},
{""id"":276,""character_id"":90522832,""alliance_id"":99003500,""allyHistory_recordId"":1009908,""corpHistory_recordId"":38261600,""start"":""2015-12-28T15:00:00"",""end"":""2018-04-27T18:09:00"",""corporation_id"":98043813},
{""id"":277,""character_id"":90522832,""alliance_id"":99008223,""allyHistory_recordId"":1185985,""corpHistory_recordId"":38261600,""start"":""2018-04-28T18:57:00"",""end"":""2019-01-19T18:12:00"",""corporation_id"":98043813},
{""id"":278,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":19000000,""corpHistory_recordId"":49759532,""start"":""2020-08-13T00:00:00"",""end"":""2020-09-03T23:59:00"",""corporation_id"":98377297}]");
            Accept_CharacterCorporationHistory(@"[{""record_id"":49759532,""start_date"":""2019-04-22T20:42:00"",""corporation_id"":98377297,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":0,""end_date"":null,""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":false},
{""record_id"":16311177,""start_date"":""2011-03-11T19:29:00"",""corporation_id"":1000168,""prev_corp_id"":0,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98010376,""end_date"":""2011-04-16T09:51:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":16595491,""start_date"":""2011-04-16T09:52:00"",""corporation_id"":98010376,""prev_corp_id"":1000168,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2012-04-09T18:34:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":19431146,""start_date"":""2012-04-09T18:35:00"",""corporation_id"":1000107,""prev_corp_id"":98010376,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98010376,""end_date"":""2012-07-15T09:21:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":20250210,""start_date"":""2012-07-15T09:22:00"",""corporation_id"":98010376,""prev_corp_id"":1000107,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":98129547,""end_date"":""2012-08-02T10:46:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":20392133,""start_date"":""2012-08-02T10:47:00"",""corporation_id"":98129547,""prev_corp_id"":98010376,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2013-05-29T10:29:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":25686599,""start_date"":""2013-05-29T10:30:00"",""corporation_id"":1000107,""prev_corp_id"":98129547,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":238942032,""end_date"":""2013-09-27T21:10:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":28432479,""start_date"":""2013-09-27T21:11:00"",""corporation_id"":238942032,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2014-01-21T17:16:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":30596803,""start_date"":""2014-01-21T17:17:00"",""corporation_id"":1000107,""prev_corp_id"":238942032,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98138737,""end_date"":""2014-03-06T12:09:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":31558760,""start_date"":""2014-03-06T12:10:00"",""corporation_id"":98138737,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2014-08-01T14:19:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":34299708,""start_date"":""2014-08-01T14:20:00"",""corporation_id"":1000107,""prev_corp_id"":98138737,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98377297,""end_date"":""2015-02-09T16:06:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":37039504,""start_date"":""2015-02-09T16:07:00"",""corporation_id"":98377297,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98361101,""end_date"":""2015-02-22T19:51:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":37189554,""start_date"":""2015-02-22T19:52:00"",""corporation_id"":98361101,""prev_corp_id"":98377297,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98043813,""end_date"":""2015-05-30T12:31:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":38261600,""start_date"":""2015-05-30T12:32:00"",""corporation_id"":98043813,""prev_corp_id"":98361101,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2019-01-19T18:12:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":49190764,""start_date"":""2019-01-19T18:13:00"",""corporation_id"":1000107,""prev_corp_id"":98043813,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98377297,""end_date"":""2019-04-22T20:41:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true}]");
            #endregion
        }

        /// <summary>
        /// Дополнение к текущей истории события, когда корпорация вошла в новый альянс
        /// </summary>
        [Fact]
        public void Test3()
        {
            /// Подготовка
            Generic_Prepare();
            Execute_Job();

            var corp_id = 98377297;
            List<CorporationAllianceHistoryResult.CorporationAllianceHistoryItem> corp_AllianceHistory = Esi_ExecuteAndReturn(connector.Corporation.AllianceHistory(corp_id)).ToList();
            corp_AllianceHistory.Insert(0, new CorporationAllianceHistoryResult.CorporationAllianceHistoryItem()
            {
                start_date = new DateTime(2020, 9, 13),
                alliance_id = 574100976,
                record_id = 19000000
            });
            // Симулированная история
            _repoPublicCommon.Corporation_UpdateAllianceHistory(corp_id, corp_AllianceHistory);

            #region
            /// Выполнение
            Execute_Job();

            // Результат
            var json_result = @"[{""id"":221,""character_id"":90522832,""alliance_id"":99000597,""allyHistory_recordId"":465787,""corpHistory_recordId"":16595491,""start"":""2011-07-01T21:28:00"",""end"":""2011-08-31T05:53:00"",""corporation_id"":98010376},{""id"":222,""character_id"":90522832,""alliance_id"":1350079892,""allyHistory_recordId"":485082,""corpHistory_recordId"":16595491,""start"":""2011-09-01T15:53:00"",""end"":""2011-10-27T06:41:00"",""corporation_id"":98010376},{""id"":223,""character_id"":90522832,""alliance_id"":99001011,""allyHistory_recordId"":504845,""corpHistory_recordId"":16595491,""start"":""2011-11-09T09:51:00"",""end"":""2012-04-09T14:15:00"",""corporation_id"":98010376},{""id"":224,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":556322,""corpHistory_recordId"":20250210,""start"":""2012-07-15T09:22:00"",""end"":""2012-08-02T10:46:00"",""corporation_id"":98010376},{""id"":225,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":589914,""corpHistory_recordId"":20392133,""start"":""2012-08-02T10:47:00"",""end"":""2013-05-22T10:15:00"",""corporation_id"":98129547},{""id"":226,""character_id"":90522832,""alliance_id"":1208295500,""allyHistory_recordId"":259685,""corpHistory_recordId"":28432479,""start"":""2013-09-27T21:11:00"",""end"":""2014-01-21T17:16:00"",""corporation_id"":238942032},{""id"":227,""character_id"":90522832,""alliance_id"":99005528,""allyHistory_recordId"":960351,""corpHistory_recordId"":38261600,""start"":""2015-05-31T14:44:00"",""end"":""2015-12-27T14:02:00"",""corporation_id"":98043813},{""id"":228,""character_id"":90522832,""alliance_id"":99003500,""allyHistory_recordId"":1009908,""corpHistory_recordId"":38261600,""start"":""2015-12-28T15:00:00"",""end"":""2018-04-27T18:09:00"",""corporation_id"":98043813},{""id"":229,""character_id"":90522832,""alliance_id"":99008223,""allyHistory_recordId"":1185985,""corpHistory_recordId"":38261600,""start"":""2018-04-28T18:57:00"",""end"":""2019-01-19T18:12:00"",""corporation_id"":98043813},{""id"":230,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":19000000,""corpHistory_recordId"":49759532,""start"":""2020-09-13T00:00:00"",""end"":null,""corporation_id"":98377297}]";
            Accept_CharacterAllianceHistory(json_result);
             json_result = @"[{""record_id"":49759532,""start_date"":""2019-04-22T20:42:00"",""corporation_id"":98377297,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":0,""end_date"":null,""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":false},
{""record_id"":16311177,""start_date"":""2011-03-11T19:29:00"",""corporation_id"":1000168,""prev_corp_id"":0,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98010376,""end_date"":""2011-04-16T09:51:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":16595491,""start_date"":""2011-04-16T09:52:00"",""corporation_id"":98010376,""prev_corp_id"":1000168,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2012-04-09T18:34:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":19431146,""start_date"":""2012-04-09T18:35:00"",""corporation_id"":1000107,""prev_corp_id"":98010376,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98010376,""end_date"":""2012-07-15T09:21:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":20250210,""start_date"":""2012-07-15T09:22:00"",""corporation_id"":98010376,""prev_corp_id"":1000107,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":98129547,""end_date"":""2012-08-02T10:46:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":20392133,""start_date"":""2012-08-02T10:47:00"",""corporation_id"":98129547,""prev_corp_id"":98010376,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2013-05-29T10:29:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":25686599,""start_date"":""2013-05-29T10:30:00"",""corporation_id"":1000107,""prev_corp_id"":98129547,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":238942032,""end_date"":""2013-09-27T21:10:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":28432479,""start_date"":""2013-09-27T21:11:00"",""corporation_id"":238942032,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2014-01-21T17:16:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":30596803,""start_date"":""2014-01-21T17:17:00"",""corporation_id"":1000107,""prev_corp_id"":238942032,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98138737,""end_date"":""2014-03-06T12:09:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":31558760,""start_date"":""2014-03-06T12:10:00"",""corporation_id"":98138737,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2014-08-01T14:19:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":34299708,""start_date"":""2014-08-01T14:20:00"",""corporation_id"":1000107,""prev_corp_id"":98138737,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98377297,""end_date"":""2015-02-09T16:06:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":37039504,""start_date"":""2015-02-09T16:07:00"",""corporation_id"":98377297,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98361101,""end_date"":""2015-02-22T19:51:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":37189554,""start_date"":""2015-02-22T19:52:00"",""corporation_id"":98361101,""prev_corp_id"":98377297,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98043813,""end_date"":""2015-05-30T12:31:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":38261600,""start_date"":""2015-05-30T12:32:00"",""corporation_id"":98043813,""prev_corp_id"":98361101,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2019-01-19T18:12:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true},
{""record_id"":49190764,""start_date"":""2019-01-19T18:13:00"",""corporation_id"":1000107,""prev_corp_id"":98043813,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98377297,""end_date"":""2019-04-22T20:41:00"",""alliance_id"":0,""instat"":false,""nb"":false,""next_ally_id"":0,""prev_ally_id"":0,""allyComplete"":true}]";
            Accept_CharacterCorporationHistory(json_result);
#endregion
        }

        /// <summary>
        /// Дополнение к текущей истории события, когда персонаж вышел из корпорации, которая была в альянсе
        /// </summary>
        [Fact]
        public void Test4()
        {
            /// Подготовка
            Generic_Prepare();
            Execute_Job();

            // В этом примере корп SELEBROS не состоит ни в каком альнсе, нужно добавить членство
            var corp_id = 98377297;
            List<CorporationAllianceHistoryResult.CorporationAllianceHistoryItem> corp_AllianceHistory = Esi_ExecuteAndReturn(connector.Corporation.AllianceHistory(corp_id)).ToList();
            corp_AllianceHistory.Insert(0, new CorporationAllianceHistoryResult.CorporationAllianceHistoryItem()
            {
                start_date = new DateTime(2020, 8, 13),
                alliance_id = 574100976,
                record_id = 19000000
            });
            // Симулированная история
            _repoPublicCommon.Corporation_UpdateAllianceHistory(corp_id, corp_AllianceHistory);
            Execute_Job();

            // Теперь персонаж вышел из корпорации
            List<CharacterCorporationHistoryResult.CharacterCorporationHistoryItem> char_CorpHistory = Esi_ExecuteAndReturn(connector.Character.CorporationHistory(character_id)).ToList();
            char_CorpHistory.Insert(0, new CharacterCorporationHistoryResult.CharacterCorporationHistoryItem()
            {
                corporation_id = 1000107,
                record_id = 149190764,
                start_date = new DateTime(2020, 9, 10)
            });
            // Симулированная история
            _repoPublicCommon.Character_UpdateCorporationHistory(character_id, char_CorpHistory);

            #region 
            Execute_Job();

            Accept_CharacterAllianceHistory(@"[{""id"":279,""character_id"":90522832,""alliance_id"":99000597,""allyHistory_recordId"":465787,""corpHistory_recordId"":16595491,""start"":""2011-07-01T21:28:00"",""end"":""2011-08-31T05:53:00"",""corporation_id"":98010376},{""id"":280,""character_id"":90522832,""alliance_id"":1350079892,""allyHistory_recordId"":485082,""corpHistory_recordId"":16595491,""start"":""2011-09-01T15:53:00"",""end"":""2011-10-27T06:41:00"",""corporation_id"":98010376},{""id"":281,""character_id"":90522832,""alliance_id"":99001011,""allyHistory_recordId"":504845,""corpHistory_recordId"":16595491,""start"":""2011-11-09T09:51:00"",""end"":""2012-04-09T14:15:00"",""corporation_id"":98010376},{""id"":283,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":589914,""corpHistory_recordId"":20392133,""start"":""2012-08-02T10:47:00"",""end"":""2013-05-22T10:15:00"",""corporation_id"":98129547},{""id"":285,""character_id"":90522832,""alliance_id"":99005528,""allyHistory_recordId"":960351,""corpHistory_recordId"":38261600,""start"":""2015-05-31T14:44:00"",""end"":""2015-12-27T14:02:00"",""corporation_id"":98043813},{""id"":286,""character_id"":90522832,""alliance_id"":99003500,""allyHistory_recordId"":1009908,""corpHistory_recordId"":38261600,""start"":""2015-12-28T15:00:00"",""end"":""2018-04-27T18:09:00"",""corporation_id"":98043813},{""id"":282,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":556322,""corpHistory_recordId"":20250210,""start"":""2012-07-15T09:22:00"",""end"":""2012-08-02T10:46:00"",""corporation_id"":98010376},{""id"":284,""character_id"":90522832,""alliance_id"":1208295500,""allyHistory_recordId"":259685,""corpHistory_recordId"":28432479,""start"":""2013-09-27T21:11:00"",""end"":""2014-01-21T17:16:00"",""corporation_id"":238942032},{""id"":287,""character_id"":90522832,""alliance_id"":99008223,""allyHistory_recordId"":1185985,""corpHistory_recordId"":38261600,""start"":""2018-04-28T18:57:00"",""end"":""2019-01-19T18:12:00"",""corporation_id"":98043813},{""id"":288,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":19000000,""corpHistory_recordId"":49759532,""start"":""2020-08-13T00:00:00"",""end"":""2020-09-09T23:59:00"",""corporation_id"":98377297}]");
            Accept_CharacterCorporationHistory(@"[{""record_id"":16311177,""start_date"":""2011-03-11T19:29:00"",""corporation_id"":1000168,""prev_corp_id"":0,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98010376,""end_date"":""2011-04-16T09:51:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":16595491,""start_date"":""2011-04-16T09:52:00"",""corporation_id"":98010376,""prev_corp_id"":1000168,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2012-04-09T18:34:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":19431146,""start_date"":""2012-04-09T18:35:00"",""corporation_id"":1000107,""prev_corp_id"":98010376,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98010376,""end_date"":""2012-07-15T09:21:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":20250210,""start_date"":""2012-07-15T09:22:00"",""corporation_id"":98010376,""prev_corp_id"":1000107,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":98129547,""end_date"":""2012-08-02T10:46:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":20392133,""start_date"":""2012-08-02T10:47:00"",""corporation_id"":98129547,""prev_corp_id"":98010376,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2013-05-29T10:29:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":25686599,""start_date"":""2013-05-29T10:30:00"",""corporation_id"":1000107,""prev_corp_id"":98129547,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":238942032,""end_date"":""2013-09-27T21:10:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":28432479,""start_date"":""2013-09-27T21:11:00"",""corporation_id"":238942032,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2014-01-21T17:16:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":30596803,""start_date"":""2014-01-21T17:17:00"",""corporation_id"":1000107,""prev_corp_id"":238942032,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98138737,""end_date"":""2014-03-06T12:09:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":31558760,""start_date"":""2014-03-06T12:10:00"",""corporation_id"":98138737,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2014-08-01T14:19:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":34299708,""start_date"":""2014-08-01T14:20:00"",""corporation_id"":1000107,""prev_corp_id"":98138737,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98377297,""end_date"":""2015-02-09T16:06:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":37039504,""start_date"":""2015-02-09T16:07:00"",""corporation_id"":98377297,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98361101,""end_date"":""2015-02-22T19:51:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":37189554,""start_date"":""2015-02-22T19:52:00"",""corporation_id"":98361101,""prev_corp_id"":98377297,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98043813,""end_date"":""2015-05-30T12:31:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":38261600,""start_date"":""2015-05-30T12:32:00"",""corporation_id"":98043813,""prev_corp_id"":98361101,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2019-01-19T18:12:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":49190764,""start_date"":""2019-01-19T18:13:00"",""corporation_id"":1000107,""prev_corp_id"":98043813,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98377297,""end_date"":""2019-04-22T20:41:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":49759532,""start_date"":""2019-04-22T20:42:00"",""corporation_id"":98377297,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2020-09-09T23:59:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":149190764,""start_date"":""2020-09-10T00:00:00"",""corporation_id"":1000107,""prev_corp_id"":98377297,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":0,""end_date"":null,""instat"":false,""nb"":false,""allyComplete"":true}]");
            #endregion
        }

        /// <summary>
        /// Дополнение к текущей истории события, когда персонаж вошел в новую корпорацию, которая была в альянсе
        /// </summary>
        [Fact]
        public void Test5()
        {
            /// Подготовка
            Generic_Prepare();
            Execute_Job();

            using var dbContext = new PublicContext(_publicContextOptions);
            // Выход из корпорации SELEBROS
            List<CharacterCorporationHistoryResult.CharacterCorporationHistoryItem> char_CorpHistory = Esi_ExecuteAndReturn(connector.Character.CorporationHistory(character_id)).ToList();
            char_CorpHistory.Insert(0, new CharacterCorporationHistoryResult.CharacterCorporationHistoryItem()
            {
                corporation_id = 1000107,
                record_id = 149190764,
                start_date = new DateTime(2020, 9, 8)
            });
            // Симулированная история
            _repoPublicCommon.Character_UpdateCorporationHistory(character_id, char_CorpHistory);
            Execute_Job();

            // Вход в корпораци, которая в альянсе
            var to_corpId = 238942032;
            char_CorpHistory.Insert(0, new CharacterCorporationHistoryResult.CharacterCorporationHistoryItem()
            {
                corporation_id = to_corpId,
                record_id = 149190784,
                start_date = new DateTime(2020, 9, 10)
            });
            // Симулированная история
            _repoPublicCommon.Character_UpdateCorporationHistory(character_id, char_CorpHistory);

            #region 
            Execute_Job();
            Accept_CharacterAllianceHistory(@"[{""id"":289,""character_id"":90522832,""alliance_id"":99000597,""allyHistory_recordId"":465787,""corpHistory_recordId"":16595491,""start"":""2011-07-01T21:28:00"",""end"":""2011-08-31T05:53:00"",""corporation_id"":98010376},
{""id"":290,""character_id"":90522832,""alliance_id"":1350079892,""allyHistory_recordId"":485082,""corpHistory_recordId"":16595491,""start"":""2011-09-01T15:53:00"",""end"":""2011-10-27T06:41:00"",""corporation_id"":98010376},
{""id"":291,""character_id"":90522832,""alliance_id"":99001011,""allyHistory_recordId"":504845,""corpHistory_recordId"":16595491,""start"":""2011-11-09T09:51:00"",""end"":""2012-04-09T14:15:00"",""corporation_id"":98010376},
{""id"":293,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":589914,""corpHistory_recordId"":20392133,""start"":""2012-08-02T10:47:00"",""end"":""2013-05-22T10:15:00"",""corporation_id"":98129547},
{""id"":295,""character_id"":90522832,""alliance_id"":99005528,""allyHistory_recordId"":960351,""corpHistory_recordId"":38261600,""start"":""2015-05-31T14:44:00"",""end"":""2015-12-27T14:02:00"",""corporation_id"":98043813},
{""id"":296,""character_id"":90522832,""alliance_id"":99003500,""allyHistory_recordId"":1009908,""corpHistory_recordId"":38261600,""start"":""2015-12-28T15:00:00"",""end"":""2018-04-27T18:09:00"",""corporation_id"":98043813},
{""id"":292,""character_id"":90522832,""alliance_id"":574100976,""allyHistory_recordId"":556322,""corpHistory_recordId"":20250210,""start"":""2012-07-15T09:22:00"",""end"":""2012-08-02T10:46:00"",""corporation_id"":98010376},
{""id"":294,""character_id"":90522832,""alliance_id"":1208295500,""allyHistory_recordId"":259685,""corpHistory_recordId"":28432479,""start"":""2013-09-27T21:11:00"",""end"":""2014-01-21T17:16:00"",""corporation_id"":238942032},
{""id"":297,""character_id"":90522832,""alliance_id"":99008223,""allyHistory_recordId"":1185985,""corpHistory_recordId"":38261600,""start"":""2018-04-28T18:57:00"",""end"":""2019-01-19T18:12:00"",""corporation_id"":98043813},
{""id"":298,""character_id"":90522832,""alliance_id"":99009547,""allyHistory_recordId"":1278111,""corpHistory_recordId"":149190784,""start"":""2020-09-10T00:00:00"",""end"":null,""corporation_id"":238942032}]");
            Accept_CharacterCorporationHistory(@"[{""record_id"":16595491,""start_date"":""2011-04-16T09:52:00"",""corporation_id"":98010376,""prev_corp_id"":1000168,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2012-04-09T18:34:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":19431146,""start_date"":""2012-04-09T18:35:00"",""corporation_id"":1000107,""prev_corp_id"":98010376,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98010376,""end_date"":""2012-07-15T09:21:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":20250210,""start_date"":""2012-07-15T09:22:00"",""corporation_id"":98010376,""prev_corp_id"":1000107,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":98129547,""end_date"":""2012-08-02T10:46:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":20392133,""start_date"":""2012-08-02T10:47:00"",""corporation_id"":98129547,""prev_corp_id"":98010376,""is_deleted"":true,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2013-05-29T10:29:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":25686599,""start_date"":""2013-05-29T10:30:00"",""corporation_id"":1000107,""prev_corp_id"":98129547,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":238942032,""end_date"":""2013-09-27T21:10:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":28432479,""start_date"":""2013-09-27T21:11:00"",""corporation_id"":238942032,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2014-01-21T17:16:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":30596803,""start_date"":""2014-01-21T17:17:00"",""corporation_id"":1000107,""prev_corp_id"":238942032,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98138737,""end_date"":""2014-03-06T12:09:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":31558760,""start_date"":""2014-03-06T12:10:00"",""corporation_id"":98138737,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2014-08-01T14:19:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":34299708,""start_date"":""2014-08-01T14:20:00"",""corporation_id"":1000107,""prev_corp_id"":98138737,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98377297,""end_date"":""2015-02-09T16:06:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":37039504,""start_date"":""2015-02-09T16:07:00"",""corporation_id"":98377297,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98361101,""end_date"":""2015-02-22T19:51:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":37189554,""start_date"":""2015-02-22T19:52:00"",""corporation_id"":98361101,""prev_corp_id"":98377297,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98043813,""end_date"":""2015-05-30T12:31:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":38261600,""start_date"":""2015-05-30T12:32:00"",""corporation_id"":98043813,""prev_corp_id"":98361101,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2019-01-19T18:12:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":49190764,""start_date"":""2019-01-19T18:13:00"",""corporation_id"":1000107,""prev_corp_id"":98043813,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98377297,""end_date"":""2019-04-22T20:41:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":49759532,""start_date"":""2019-04-22T20:42:00"",""corporation_id"":98377297,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":1000107,""end_date"":""2020-09-07T23:59:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":149190764,""start_date"":""2020-09-08T00:00:00"",""corporation_id"":1000107,""prev_corp_id"":98377297,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":238942032,""end_date"":""2020-09-09T23:59:00"",""instat"":false,""nb"":false,""allyComplete"":true},
{""record_id"":149190784,""start_date"":""2020-09-10T00:00:00"",""corporation_id"":238942032,""prev_corp_id"":1000107,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":0,""end_date"":null,""instat"":false,""nb"":false,""allyComplete"":false},
{""record_id"":16311177,""start_date"":""2011-03-11T19:29:00"",""corporation_id"":1000168,""prev_corp_id"":0,""is_deleted"":null,""character_id"":90522832,""next_corp_id"":98010376,""end_date"":""2011-04-16T09:51:00"",""instat"":false,""nb"":false,""allyComplete"":true}]");
            #endregion
        }
    }
}
