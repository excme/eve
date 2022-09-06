using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterOnlines : ConnectorJob
    {
        //static string l_reqName = "Character_LocationOnline";
        //static string l_scope = Scope.Location.ReadOnline.Name;
        //static ERequestFolder l_folder = ERequestFolder.Location;

        //public CharacterOnlines() : base(l_reqName, l_folder, l_scope)
        //{
        //}
        //public CharacterOnlines(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope) 
        //{
        //    _itemToUpdate = characterToUpdate;
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    // Проверка, можно ли обновлять онлайн
        //    bool canRequest = false;
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        // Если сейчас персонаж онлайн или оффлайн, но с последней проверкой больше 10 мин.
        //        var last_online = _dbContext.Eveonline_CharacterLocationOnlines.LastOrDefault(x => x.character_id == sso.character_id);
        //        if (last_online == null || last_online.online || (!last_online.online && (DateTime.UtcNow - last_online.last_update).TotalMinutes >= 10))
        //        {
        //            canRequest = true;
        //        }
        //    }

        //    if (canRequest)
        //    {
        //        // Выполнение запроса
        //        var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData);
        //        Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Character.Location.IsOnline(sso.character_id).ExecuteAsync);
        //        var ConnectorResult = _eveOnlineGeneric.ExecuteRequest<CharacterOnlineResult>(запросКоннектора, folder, CharacterOnlineResult.TimeExpire(), CharacterOnlineResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();

        //        if (ConnectorResult.success)
        //        {
        //            _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterLocationOnline, 1);
        //            var now_online = ConnectorResult.value;
        //            EveOnlineCharacterLocationOnline last_login_item = new EveOnlineCharacterLocationOnline();

        //            // character/online
        //            using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //            {
        //                last_login_item = _dbContext.Eveonline_CharacterLocationOnlines.LastOrDefault(x => x.character_id == sso.character_id);

        //                if (last_login_item != null)
        //                {
        //                    // Если пришел обновленный и завершенный
        //                    if (last_login_item.online && !now_online.online && last_login_item.logins == now_online.logins)
        //                    {
        //                        last_login_item.UpdateProperties(now_online);
        //                        last_login_item.last_update = DateTime.UtcNow;
        //                        _dbContext.Eveonline_CharacterLocationOnlines.Update(last_login_item);
        //                    }
        //                    else if (last_login_item.online && last_login_item.logins < now_online.logins)
        //                    {
        //                        // Если пришел новый и старый еще не завершен
        //                        last_login_item.online = false;
        //                        last_login_item.last_update = DateTime.UtcNow;
        //                        if (now_online.online)
        //                            last_login_item.last_logout = now_online.last_logout;
        //                        _dbContext.Eveonline_CharacterLocationOnlines.Update(last_login_item);
        //                    }
        //                    else if (last_login_item.online && now_online.online && now_online.logins == last_login_item.logins)
        //                    {
        //                        // Пришло просто обновление последней записи
        //                        last_login_item.last_logout = DateTime.UtcNow;
        //                        last_login_item.last_update = DateTime.UtcNow;
        //                        _dbContext.Eveonline_CharacterLocationOnlines.Update(last_login_item);
        //                    }
        //                    else if (last_login_item != null && last_login_item.online == false && now_online.online == false && last_login_item.logins == now_online.logins)
        //                    {
        //                        // Если старое и закрытое
        //                        last_login_item.last_update = DateTime.UtcNow;
        //                        _dbContext.Eveonline_CharacterLocationOnlines.Update(last_login_item);
        //                    }
        //                }

        //                // Если новое
        //                if (last_login_item == null || last_login_item.logins < now_online.logins)
        //                {
        //                    EveOnlineCharacterLocationOnline _online = new EveOnlineCharacterLocationOnline() { character_id = sso.character_id };
        //                    _online.UpdateProperties(now_online);
        //                    _online.last_update = DateTime.UtcNow;
        //                    _dbContext.Eveonline_CharacterLocationOnlines.Add(_online);
        //                }

        //                _dbContext.SaveChanges();
        //            }

        //            if (now_online.online)
        //            {
        //                // Если true, то обновляем location и ship
        //                Character_UpdateShip(sso, now_online.logins);
        //                Character_UpdateLocation(sso, now_online.logins);
        //            }
        //            else if (now_online.online == false && now_online.logins > 0)
        //            {
        //                // Если false, то обновляем location пока isInOpenSpace == false
        //                using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //                {
        //                    var last_location = _dbContext.Eveonline_CharacterLocations.LastOrDefault(x => x.character_id == sso.character_id && x.login_num == now_online.logins);
        //                    if (last_location != null && last_location.IsInOpenSpace())
        //                    {
        //                        // Получаем место где персонаж остался на оффлайн
        //                        Character_UpdateLocation(sso, now_online.logins);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //void Character_UpdateShip(General.Data.CharacterCorporationAuthSso sso, int login_id) {
        //    string reqName = "Character_LocationShip";
        //    ERequestFolder folder = ERequestFolder.Location;
        //    var s = connector.Character.Location.GetCurrentShip(0).RequiresScope;

        //    // Выполнение запроса
        //    var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData);
        //    Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Character.Location.GetCurrentShip(sso.character_id).ExecuteAsync);
        //    var ConnectorResult = _eveOnlineGeneric.ExecuteRequest<CharacterShipResult>(запросКоннектора, folder, CharacterShipResult.TimeExpire(), CharacterShipResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();

        //    if (ConnectorResult.success) {
        //        _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterLocationShip, 1);

        //        var currentShip = ConnectorResult.value;

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Добавляем начальный корабль в кажду авторизацию
        //            var lastCharacterShip = _dbContext.Eveonline_CharacterLocationShips.LastOrDefault(x => x.character_id == sso.character_id && x.login_num == login_id);
        //            if (lastCharacterShip == null || lastCharacterShip.ship_item_id != currentShip.ship_item_id || lastCharacterShip.ship_type_id != currentShip.ship_type_id)
        //            {
        //                EveOnlineCharacterLocationShip _newShip = new EveOnlineCharacterLocationShip() { onDateTime = DateTime.UtcNow, character_id = sso.character_id, login_num = login_id };
        //                _newShip.UpdateProperties(currentShip);
        //                _dbContext.Eveonline_CharacterLocationShips.Add(_newShip);
        //                _dbContext.SaveChanges();
        //            }
        //        }
        //    }
        //}
        //void Character_UpdateLocation(General.Data.CharacterCorporationAuthSso sso, int login_id) {
        //    string reqName = "Character_Location";
        //    ERequestFolder folder = ERequestFolder.Location;
        //    var s = connector.Character.Location.GetLocation(0).RequiresScope;

        //    // Выполнение запроса
        //    var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData);
        //    Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Character.Location.GetLocation(sso.character_id).ExecuteAsync);
        //    var ConnectorResult = _eveOnlineGeneric.ExecuteRequest<CharacterLocationResult>(запросКоннектора, folder, CharacterLocationResult.TimeExpire(), CharacterLocationResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();

        //    if (ConnectorResult.success)
        //    {
        //        _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterLocationCurrent, 1);

        //        var location = ConnectorResult.value;
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var _lastLocation = _dbContext.Eveonline_CharacterLocations.LastOrDefault(x => x.character_id == sso.character_id && x.login_num == login_id);

        //            if (_lastLocation == null || _lastLocation.solar_system_id != location.solar_system_id || _lastLocation.station_id != location.station_id || _lastLocation.structure_id != location.structure_id)
        //            {
        //                EveOnlineCharacterLocation currentLocation = new EveOnlineCharacterLocation() { startDateTime = DateTime.UtcNow, character_id = sso.character_id, login_num = login_id, endDateTime = DateTime.UtcNow.AddMinutes(1) };
        //                currentLocation.UpdateProperties(location);
        //                _dbContext.Eveonline_CharacterLocations.Add(currentLocation);

        //                if (_lastLocation != null)
        //                {
        //                    _lastLocation.endDateTime = DateTime.UtcNow;
        //                    _dbContext.Eveonline_CharacterLocations.Update(_lastLocation);
        //                }

        //                // Проверка существования структуры
        //                if (currentLocation.structure_id > 0 && !_dbContext.Eveonline_UniverseStructures.Any(x => x.structure_id == currentLocation.structure_id))
        //                {
        //                    Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(authConnector.Universe.GetStructureInfo(currentLocation.structure_id).ExecuteAsync);
        //                    var ConnectorResult1 = _eveOnlineGeneric.ExecuteRequest<UniverseStructureInfoResult>(запросКоннектора1, folder, UniverseStructureInfoResult.TimeExpire(), UniverseStructureInfoResult.GetArgs(currentLocation.structure_id)).GetAwaiter().GetResult();

        //                    var str = new EveOnlineUniverseStructure() { lastUpdate = new DateTime(1999, 1, 1), structure_id = currentLocation.structure_id };
        //                    if (ConnectorResult1.success)
        //                    {
        //                        str.UpdateProperties(ConnectorResult1.value);
        //                        str.lastUpdate = DateTime.UtcNow;
        //                    }
        //                    _dbContext.Eveonline_UniverseStructures.Add(str);
        //                }

        //                // Проверка существования станции
        //                if (currentLocation.station_id > 0 && !_dbContext.Eveonline_UniverseStations.Any(x => x.station_id == currentLocation.station_id))
        //                {
        //                    Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(authConnector.Universe.GetStationInfo(currentLocation.station_id).ExecuteAsync);
        //                    var ConnectorResult1 = _eveOnlineGeneric.ExecuteRequest<UniverseStationInfoResult>(запросКоннектора1, folder, UniverseStationInfoResult.TimeExpire(), UniverseStationInfoResult.GetArgs(currentLocation.station_id)).GetAwaiter().GetResult();

        //                    var station = new EveOnlineUniverseStation() { station_id = currentLocation.station_id };
        //                    if (ConnectorResult1.success)
        //                    {
        //                        station.UpdateProperties(ConnectorResult1.value);
        //                        _dbContext.Eveonline_UniverseStations.Add(station);
        //                    }
        //                }

        //                _dbContext.SaveChanges();
        //            }
        //            else if (_lastLocation != null)
        //            {
        //                // Обновление последнего времени находждения
        //                _lastLocation.endDateTime = DateTime.UtcNow;
        //                _dbContext.Eveonline_CharacterLocations.Update(_lastLocation);
        //                _dbContext.SaveChanges();
        //            }
        //        }
        //    }
        //}
    }
}
