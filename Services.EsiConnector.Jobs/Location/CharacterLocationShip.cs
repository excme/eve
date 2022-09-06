using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterShips : ConnectorJob
    {
        //static string l_reqName = "Character_LocationShip";
        //static string l_scope = Scope.Location.ReadLocation.Name;
        //static ERequestFolder l_folder = ERequestFolder.Location;
        //public CharacterShips() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterShips(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso) {
        //    // Выполнение запроса
        //    var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData);
        //    Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(authConnector.Character.Location.GetCurrentShip(sso.character_id).ExecuteAsync);
        //    var ConnectorResult = _eveOnlineGeneric.ExecuteRequest<CharacterShipResult>(запросКоннектора, folder, CharacterShipResult.TimeExpire(), CharacterShipResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();

        //    if (ConnectorResult.success)
        //    {
        //        _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterLocationShip, 1);

        //        var currentShip = ConnectorResult.value;
                    
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var lastCharacterShip = _dbContext.Eveonline_CharacterLocationShips.LastOrDefault(x => x.character_id == sso.character_id);
        //            if (lastCharacterShip == null || lastCharacterShip.ship_item_id != currentShip.ship_item_id || lastCharacterShip.ship_type_id != currentShip.ship_type_id)
        //            {
        //                EveOnlineCharacterLocationShip _newShip = new EveOnlineCharacterLocationShip() { onDateTime = DateTime.UtcNow.AddDays(-7), character_id = sso.character_id};
        //                _newShip.UpdateProperties(currentShip);
        //                _dbContext.Eveonline_CharacterLocationShips.Add(_newShip);
        //                _dbContext.SaveChanges();
        //            }
        //        }
        //    }
        //}
    }
}
