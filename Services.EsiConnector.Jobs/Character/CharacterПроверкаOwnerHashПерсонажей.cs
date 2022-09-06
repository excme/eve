using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterПроверкаOwnerHashПерсонажей : ConnectorJob
    {
        //static string l_reqName = "Sso_ПроверкаSsoТокенов";
        //public CharacterПроверкаOwnerHashПерсонажей() : base(l_reqName, ERequestFolder.Other, "", false) { }
        //public CharacterПроверкаOwnerHashПерсонажей(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 1000, int itemToUpdate = 0) : base(genericService, options, logger, l_reqName, ERequestFolder.Other, "", false) {
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = itemToUpdate;
        //}
        //public override void TaskJob()
        //{
        //    using (var _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        var toUpdate = _dbContext.Evezone_Ssos.Where(x=>x.Status == ESsoStatus.Active).OrderBy(x=>x.LastOwnerAndStatusUpdate).ToList();
        //        if (_maxCharactersToUpdate > 0)
        //            toUpdate = toUpdate.Take(_maxCharactersToUpdate).ToList();
        //        if(_itemToUpdate > 0)
        //            toUpdate = toUpdate.Where(x => x.character_id == _itemToUpdate).ToList();

        //        foreach (var sso in toUpdate)
        //        {
        //            if (sso?.RefreshToken != null)
        //            {
        //                AuthToken authToken = new AuthToken(sso.AccessToken, "", sso.RefreshToken);
        //                var c = GetEsiConnectorAuth(authToken);
        //                var result = c.SSO.ПроверкаToken().GetAwaiter().GetResult();

        //                if (result != null)
        //                {
        //                    if (sso.CharacterOwnerHash?.Length > 0 && sso.CharacterOwnerHash != result.CharacterOwnerHash)
        //                    {
        //                        //_logger.LogInformation($"{reqName}. Юзер {sso.EveOnlineAccountId} потерял владение персонажем {sso.character_id} по ownerHash");
        //                        AddSsoRequest(sso.character_id, ESsoRequestType.ssoOwnerCheck, -1, 1);

        //                        sso.Status = ESsoStatus.LosedOwner;
        //                        sso.LastOwnerAndStatusUpdate = DateTime.UtcNow;
        //                        _dbContext.Evezone_Ssos.Update(sso);
        //                    }
        //                    else if(sso.CharacterOwnerHash == result.CharacterOwnerHash)
        //                    {
        //                        _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.ssoOwnerCheck, 1);

        //                        sso.LastOwnerAndStatusUpdate = DateTime.UtcNow;
        //                        _dbContext.Evezone_Ssos.Update(sso);
        //                    }
        //                }
        //                else
        //                {
        //                    //_logger.LogError($"ПроверкаOwnerHashПерсонажей. Проверка токена {sso.Id} => null");
        //                    AddSsoRequest(sso.character_id, ESsoRequestType.ssoOwnerCheck, -2, 1);

        //                    sso.Status = ESsoStatus.Blocked;
        //                    sso.LastOwnerAndStatusUpdate = DateTime.UtcNow;
        //                    _dbContext.Evezone_Ssos.Update(sso);
        //                }
        //            }

        //        }
        //        _dbContext.SaveChanges();
        //    }
        //}
    }
}
