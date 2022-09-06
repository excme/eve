using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterLocations : ConnectorJob
    {
        //static string l_reqName = "Character_Location";
        //static string l_scope = Scope.Location.ReadLocation.Name;
        //static ERequestFolder l_folder = ERequestFolder.Location;
        //public CharacterLocations() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterLocations(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
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
        //            var _lastLocation = _dbContext.Eveonline_CharacterLocations.LastOrDefault(x => x.character_id == sso.character_id);

        //            if (_lastLocation == null || _lastLocation.solar_system_id != location.solar_system_id || _lastLocation.station_id != location.station_id || _lastLocation.structure_id != location.structure_id)
        //            {
        //                EveOnlineCharacterLocation currentLocation = new EveOnlineCharacterLocation() { startDateTime = DateTime.UtcNow, character_id = sso.character_id };
        //                currentLocation.UpdateProperties(location);

        //                _dbContext.Eveonline_CharacterLocations.Add(currentLocation);

        //                // Проверка существования структуры
        //                if(currentLocation.structure_id > 0 && currentLocation.structure_id != _lastLocation.structure_id && !_dbContext.Eveonline_UniverseStructures.Any(x => x.structure_id == currentLocation.structure_id))
        //                {
        //                    Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(authConnector.Universe.GetStructureInfo(currentLocation.structure_id).ExecuteAsync);
        //                    var ConnectorResult1 = _eveOnlineGeneric.ExecuteRequest<UniverseStructureInfoResult>(запросКоннектора1, folder, UniverseStructureInfoResult.TimeExpire(), UniverseStructureInfoResult.GetArgs(currentLocation.structure_id)).GetAwaiter().GetResult();

        //                    var str = new EveOnlineUniverseStructure() { lastUpdate = new DateTime(1999,1,1), structure_id = currentLocation.structure_id};
        //                    if (ConnectorResult1.success)
        //                    {
        //                        str.UpdateProperties(ConnectorResult1.value);
        //                        str.lastUpdate = DateTime.UtcNow;
        //                    }
        //                    _dbContext.Eveonline_UniverseStructures.Add(str);
        //                }

        //                // Проверка существования станции
        //                if (currentLocation.station_id > 0 && currentLocation.station_id != _lastLocation.station_id && !_dbContext.Eveonline_UniverseStations.Any(x => x.station_id == currentLocation.station_id))
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
        //        }
        //    }
        //}
    }
}
