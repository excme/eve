using eveDirect.Databases.Contexts.Public.Models;
using eveDirect.Shared.EsiConnector.Models;
using eveDirect.Shared.EsiConnector.Models.SSO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eveDirect.Repo.ReadWrite
{
    /// <summary>
    /// Интерфейс реопзитория публичных данных eve online
    /// </summary>
    public interface IReadWrite
    {
        /// <summary>
        /// Ид всех альянсов
        /// </summary>
        /// <param name="order_by_datetime">Сортировка по какому времени</param>
        /// <param name="max_count">Ограничение размера результата</param>
        /// <returns></returns>
        List<int> Alliance_Ids(
            Expression<Func<EveOnlineAlliance, DateTime?>> order_by_datetime = null,
            Expression<Func<EveOnlineAlliance, bool>> where = null,
            int max_count = 0,
            int part_of = -1);
        void Universe_Types_RemoveBroked();

        /// <summary>
        /// Добавление нового альянса в базу
        /// </summary>
        /// <param name="alliance_id"></param>
        /// <returns></returns>
        void Alliance_AddNew(int alliance_id, bool is_active = false, bool newCreated = false);
        int Alliance_AddNew(List<int> alliance_ids);
        //bool Alliance_Exists(int alliance_id);
        //bool Alliance_CorporationsUpdated(int alliance_id);
        int Alliance_UpdateCorpations(int alliance_id, List<int> corporation_ids);

        void Characters_UpdateEndAllianceMembering(EveOnlineCorporationAllianceHistory allianceHistory_Item);
        void Character_AllianceHistoryInCorporation(int charater_id, EveOnlineCharacterCorpHistory corpHistoryItem, List<EveOnlineCorporationAllianceHistory> allianceHistoriesItems);

        void Market_UpdateActualOrders(int region_id, List<MarketsOrdersResult.MarketsOrdersItem> results);
        int Market_HistoryPrices(int region_id, int type_id, List<MarketsHistoryResult.MarketsHistoryItem> data);
        
        List<T2> Db_SelectColumn<T1, T2>(Expression<Func<T1, T2>> select, Expression<Func<T1, bool>> where = null, bool distinct = false)
            where T1 : class;
        T2 Db_SelectColumn_MaxMinValue<T1, T2>(
            Expression<Func<T1, T2>> max = null,
            Expression<Func<T1, T2>> min = null,
            Expression<Func<T1, bool>> where = null)
            where T1 : class;
        T1 Db_SelectRow<T1>(Expression<Func<T1, bool>> where)
            where T1 : class;

        int Db_CountRow<T1>(Expression<Func<T1, bool>> where)
            where T1 : class;


        void CheckPoint_Upsert(string name, int value = 0);

        void Contracts_Public_Update(List<EveOnlineContract> to_update);
        double Market_HistoryPrice(int type_id, DateTime date);
        List<int> Market_Group_Ids(Expression<Func<EveOnlineMarketGroup, bool>> where = null);
        List<int> Market_Groups_AddOrUpdate(List<EveOnlineMarketGroup> marketGroups);
        int Market_Groups_CalcChilds(int group_id);

        bool Alliance_Update(int alliance_id, AllianceInfoResult data, bool new_Created = false);
        void Alliance_Delete(int alliance_id);

        /// <summary>
        /// Установка связи c killmail
        /// </summary>
        void Alliance_SetLinkToKillmail(int killmail_id, int alliance_id);
        

        /// <summary>
        /// Установка связи killmail с группой alliances
        /// </summary>
        void Alliances_SetLinkToKillmail(int killmail_id, List<int> alliances_id);
        

        /// <summary>
        /// Обновление preview
        /// </summary>
        void Alliance_UpdatePreview(int alliance_id);

        /// <summary>
        /// Количество персонажей в альянсе по дате
        /// </summary>
        int Alliance_CountCharactersOnDate(int alliance_id, DateTime date);
        /// <summary>
        /// Персонажи в альянсе по дате
        /// </summary>
        List<int> Alliance_CharactersOnDate(int alliance_id, DateTime date);
        /// <summary>
        /// Корпорации в альянсе по дате
        /// </summary>
        List<int> Alliance_CorporationsOnDate(int alliance_id, DateTime date);
        /// <summary>
        /// Количество корпорация в альянсе на дату
        /// </summary>
        /// <returns></returns>
        int Alliance_CountCorporationsOnDate(int alliance_id, DateTime date);

        /// <summary>
        /// Ид всех корпораций
        /// </summary>
        /// <param name="order_by_datetime">Сортировка по какому времени</param>
        /// <param name="max_count">Ограничение размера результата</param>
        /// <returns></returns>
        List<int> Corporation_Ids(
            Expression<Func<EveOnlineCorporation, DateTime>> order_by_datetime = null,
            Expression<Func<EveOnlineCorporation, bool>> where = null,
            int skip = 0,
            int max_count = 0,
            int part_of = -1);
        bool Corporation_Update_PublicInfo(int corporation_id, CorporationInfoResult data, bool newCreated = false);
        bool Corporation_Update_Ncp(CorporationNpccorpsResult data);

        EveOnlineCorporationAllianceHistory Corporation_AllianceHistoryItem(int record_id);

        //void Corporation_UpdateAlliance(int corporatin_id, int alliance_id);
        void Corporation_AddNew(int corporatin_id, bool newCreated =false);
        void Corporation_AddNew(bool isNcp = false, params int[] corporatin_ids);
        void Corporation_Delete(int corporation_id);

        bool Corporation_UpdateAllianceHistory(int corporation_id, List<CorporationAllianceHistoryResult.CorporationAllianceHistoryItem> data);

        /// <summary>
        /// Члены корпорации по datetime
        /// </summary>
        List<int> Corporation_MembersOnDate(int corporation_id, DateTime date);
        /// <summary>
        /// Количество членов корпорации по datetime
        /// </summary>
        int Corporation_MembersCountOnDate(int corporation_id, DateTime date);
        /// <summary>
        /// Определение альянса корпорации по дате
        /// </summary>
        int Corporation_AllianceOnDate(int corporation_id, DateTime date);

        /// <summary>
        /// Установка связи killmail с группой corporations
        /// </summary>
        void Corporations_SetLinkToKillmail(int killmail_id, List<int> corporations_id);
        /// <summary>
        /// Установка связи c killmail
        /// </summary>
        void Corporation_SetLinkToKillmail(int killmail_id, int corporation_id);
        /// <summary>
        /// Обновление preview
        /// </summary>
        void Corporation_UpdatePreview(int corporation_id);

        /// <summary>
        /// Добавление нового персонажа
        /// </summary>
        void Character_AddNew(int character_id, bool newBorn = false);
        void Character_AddNew(List<int> character_ids);
        void Character_Delete(int character_id);
        List<int> Character_Ids(
            Expression<Func<EveOnlineCharacter, DateTime>> order_by_datetime = null,
            Expression<Func<EveOnlineCharacter, bool>> where = null,
            int max_count = 0,
            int skip = 0,
            int part_of = -1);
        bool Character_PublicInformation_Update(int character_id, CharacterInfoResult characterInfo);
        bool Character_UpdateCorporationHistory(int character_id, List<CharacterCorporationHistoryResult.CharacterCorporationHistoryItem> data);
        int Character_UpdateAffiliation(CharacterAffiliationResult data, DateTime on_date);
        /// <summary>
        /// Установка связи c killmail
        /// </summary>
        void Character_SetLinkToKillmail(int killmail_id, int character_id);
        /// <summary>
        /// Установка связи killmail с группой characters
        /// </summary>
        void Characters_SetLinkToKillmail(int killmail_id, List<int> characters_ids);
        /// <summary>
        /// Обновление preview
        /// </summary>
        void Character_UpdatePreview(int character_id);

        int Universe_Bloodlines_AddOrUpdate(List<EveOnlineUniverseBloodLine> bllodlines);
        List<int> Universe_Bloodlines_Ids(Expression<Func<EveOnlineUniverseBloodLine, bool>> where = null);

        List<int> Universe_Factions_Ids(Expression<Func<EveOnlineUniverseFaction, bool>> where = null);
        int Universe_Factions_AddOrUpdate(List<EveOnlineUniverseFaction> fractions);

        //void Universe_Races_AddNew(UniverseRacesResult data);
        List<int> Universe_Races_Ids(Expression<Func<EveOnlineUniverseRace, bool>> where = null);
        int Universe_Races_AddOrUpdate(List<EveOnlineUniverseRace> races);

        List<int> Universe_Ancestries_Ids(Expression<Func<EveOnlineUniverseAncestry, bool>> where = null);
        void Universe_Ancestries_AddOrUpdate(List<EveOnlineUniverseAncestry> ancestries);

        List<int> Universe_Categories_Ids(Expression<Func<EveOnlineUniverseCategory, bool>> where = null);
        int Universe_Categories_AddOrUpdate(List<EveOnlineUniverseCategory> categories);

        List<int> Universe_Graphics_Ids(Expression<Func<EveOnlineUniverseGraphic, bool>> where = null);
        
        void Universe_Graphics_AddOrUpdate(List<EveOnlineUniverseGraphic> graphics);

        List<int> Universe_Types_Ids(Expression<Func<EveOnlineUniverseType, bool>> where = null);
        //List<int> Universe_Types_Ids();
        int Universe_Types_AddOrUpdate(List<EveOnlineUniverseType> types);
        bool Universe_Types_IsPublished(int type_id);
        List<long> Universe_Structures_Ids(Expression<Func<EveOnlineUniverseLocation, bool>> where = null);
        void Universe_Structures_AddOrUpdate(long structure_id, UniverseStructureInfoResult data);
        //void Universe_Structures_AddOrUpdate(List<EveOnlineUniverseLocation> structures);
        List<int> Universe_Groups_Ids(Expression<Func<EveOnlineUniverseGroup, bool>> where = null);
        int Universe_Groups_AddOrUpdate(List<EveOnlineUniverseGroup> groups);
        List<long> Universe_Locations_Ids(
            EUniverseLocationType locationType = EUniverseLocationType.Unknown,
            Expression<Func<EveOnlineUniverseLocation, bool>> where = null);

        /// <summary>
        /// Новые регионы в космосе
        /// </summary>
        void Universe_Regions_AddNew(List<UniverseRegionInfoResult> new_regions);
        void Universe_Constellations_AddNew(List<UniverseConstellationInfoResult> new_constellations);
        void Universe_Systems_AddNew(List<UniverseSystemInfoResult> universeSystemInfoResults);
        List<UniverseSystemInfoResult> Universe_Systems_Infos();
        void Universe_Stars_AddNew(ConcurrentDictionary<int, UniverseStarInfoResult> stars);
        void Universe_Stargates_AddNew(ConcurrentDictionary<int, UniverseStargateInfoResult> stargates);
        void Universe_Stations_AddNew(ConcurrentDictionary<int, UniverseStationInfoResult> stations);
        void Universe_Planets_AddNew(ConcurrentDictionary<int, UniversePlanetInfoResult> planets);

        List<EveOnlineUniverseLocation> Universe_InnerLocations(long solar_system_id);

        void Universe_Moons_AddNew(ConcurrentDictionary<int, ValueTuple<int, UniverseMoonInfoResult>> list);
        void Universe_AsteroidBelts_AddNew(ConcurrentDictionary<int, ValueTuple<int, UniverseAsteroidBeltInfoResult>> list);

        /// <summary>
        /// Добавить новую войну
        /// </summary>
        /// <param name="war_id"></param>
        /// <returns></returns>
        void War_AddNew(int war_id);
        /// <summary>
        /// Обновление войны eveonline
        /// </summary>
        void War_Update(int war_id, WarInfoResult to_update);
        /// <summary>
        /// Получение последней войны по war_id
        /// </summary>
        int War_GetLast();
        /// <summary>
        /// Получение war_id
        /// </summary>
        /// <returns></returns>
        List<int> War_Ids(Expression<Func<EveOnlineWar, bool>> where = null);
        void War_Remove(int war_id);
        List<EveOnlineWar> War_Get(Expression<Func<EveOnlineWar, bool>> where);
        //bool Killmails_AnyToUpdate(Expression<Func<EveOnlineKillMail, bool>> where);

        /// <summary>
        /// Получение killmail_id из отрезка из БД
        /// </summary>
        /// <returns></returns>
        //List<int> Killmails_Ids(int from_id, int to_id);

        void Killmails_UpdateGetLocationId(EveOnlineKillMail killmail);

        //Task<Dictionary<int, string>> Killmails_GetDic(Expression<Func<EveOnlineKillMail, bool>> where, int limit);
        List<EveOnlineKillMail> Killmails_Get(Expression<Func<EveOnlineKillMail, bool>> where, int take = 0, int skip = 0, Expression<Func<EveOnlineKillMail, EveOnlineKillMail>> select = null);
        List<EveOnlineKillMail> Killmails_Get(Expression<Func<EveOnlineKillMail, bool>> where, int take = 0, int skip = 0, Expression<Func<EveOnlineKillMail, EveOnlineKillMail>> select = null, Expression<Func<EveOnlineKillMail, EveOnlineKillMailVictim>> include = null);
        Dictionary<string, int> Killmails_zKillBoardStats();
        EveOnlineKillMail Killmail_Get(int killmail_id);

        /// <summary>
        /// Самый последний killmail_id
        /// </summary>
        //Task<int> Killmails_LastId();
        //void Killmails_UpdateResult(int key, KillMailInfoResult data);

        //void Killmails_UpdateResult(EveOnlineKillMail killmail, KillMailInfoResult data);
        void Killmails_UpdateResults(ConcurrentDictionary<EveOnlineKillMail, KillMailInfoResult> updated);

        /// <summary>
        /// Добавление новго killmail_id
        /// </summary>
        void Killmails_Add(int killmail_id, string killmail_hash);
        int Killmails_Add(Dictionary<int, string> killmails);
        //void Killmails_Add(int killmail_id);
        //void Killmails_UpdateHash(int killmail_id, string killmail_hash);
        //void Killmails_UpdateHash(Dictionary<int, string> killmails);
        void Killmails_SearcInnerIds(EveOnlineKillMail killmail);
        void Killmails_zKillBoardStatItemUpdate(string key, int value);

        /// <summary>
        /// Обновление sso в БД
        /// </summary>
        void Character_UpdateSso(IdentitySso sso);
        /// <summary>
        /// Удаление привязки sso токена к аккаунту
        /// </summary>
        void Sso_RemoveFromAccount(ulong user_Id, ulong sso_Id);
        /// <summary>
        /// Удаляем из базы данных все sso токены пользователя по указанному статусу
        /// </summary>
        /// <param name="user_id">Ид пользователя</param>
        /// <param name="status">Статус токена в БД</param>
        void Account_RemoveSsosByStatus(ulong user_id, ESsoStatus status);
        /// <summary>
        /// Получение всех eve-online sso токенов
        /// </summary>
        /// <param name="user_id">Id юзера</param>
        /// <returns><list type="EveOnlineSso">Токены SSO</list></returns>
        List<IdentitySso> Account_GetSso(ulong user_id);

        /// <summary>
        /// Получение sso токенов, сгруппированных по корпорациям
        /// </summary>
        //Task<List<IGrouping<int, CharacterCorporationAuthSso>>> CorporationAuth_GetAllCharactersWithSso(string requiresScope);
        //Task<List<CharacterCorporationAuthSso>> CorporationAuth_GetSso(string requiresScope, params string[] neededRoles);
        /// <summary>
        /// Получение всех sso по указанному Scope
        /// </summary>
        /// <param name="requiresScope"></param>
        AuthorizedCharacterData CharacterAuth_GetSso(string requiresScope);
        List<Cached_Corporation> Sso_GetSsoCorporations();
        List<Cached_Character> Sso_GetActiveSsoCharacters();
        void Sso_RequestStatistic(int _owner_id, int _countUpdates = 0, int _dbChanges = 0);
        /// <summary>
        /// Получение токенов доступа к ЕО
        /// </summary>
        List<IdentitySso> Sso_Get(Expression<Func<IdentitySso, bool>> where);
        /// <summary>
        /// Обнолвение
        /// </summary>
        void Sso_UpdateAccessToken(AuthorizedCharacterData data);

        void ConnectionStr_Add();
        void ConnectionStr_Disable();
        string ConnectionStr_CurrentByOwner(int ownver_id);

        List<EveOnlineContract> Contracts_Get(Expression<Func<EveOnlineContract, bool>> where = null, Expression<Func<EveOnlineContract, EveOnlineContract>> select = null);
        List<int> Contracts_Public_Update(int region_id, List<ContractsResult.Contract> results);
        void Contracts_PublicBids_Update(int contract_id, List<ContractsBidsResult.ContractsBidsItem> list);
        void Contracts_PublicItems_Update(int contract_id, List<ContractsItemsResult.ContractsItem> list);

    }
}
