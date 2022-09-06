using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationRoles : ConnectorJob
    {
        //static string l_reqName = "Corporation_Roles";
        //static string l_scope = Scope.Corporations.ReadCorporationMembership.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //static string[] l_needed_roles = new string[] { "Personnel_Manager" };
        //public CorporationRoles() : base(l_reqName, l_folder, l_scope, false, l_needed_roles) { }
        //public CorporationRoles(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, false, l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob()
        //{
        //    List<int> successedCorps = new List<int>();
        //    // Получение всех sso корпораций
        //    var all_ssos = _eveOnlineGeneric.CorporationAuth_GetAllCharactersWithSso(l_scope);
            
        //    // Получение sso у которых еще нет персональных менеджеров для запросов
        //    foreach (var ssos in all_ssos)
        //    {
        //        // Если Ncp, то пропускаем 
        //        if (_eveOnlineGeneric.IsNpcCorporation(ssos.Key))
        //            continue;

        //        // Есть ли в списек ceo, у которого точно есть права
        //        var ceo_id = _eveOnlineGeneric.Corporation_CeoId(ssos.Key);
        //        if (ssos.Any(x => x.character_id == ceo_id))
        //        {
        //            var sso = ssos.First(x => x.character_id == ceo_id);
        //            bool r = ExecuteCommand(sso);
        //            if (r)
        //            {
        //                successedCorps.Add(sso.corporation_id);
        //                continue;
        //            }
        //        }

        //        // Првоерка на существование персонального менеджера в БД
        //        int personal_id_in_corp = personalManagerInCorp(ssos.Key, ssos.Select(x => x.character_id).ToList());
        //        if(personal_id_in_corp > 0)
        //        {
        //            var sso = ssos.First(x => x.character_id == personal_id_in_corp);
        //            bool r = ExecuteCommand(sso);
        //            if (r)
        //            {
        //                successedCorps.Add(sso.corporation_id);
        //                continue;
        //            }
        //        }

        //        // Проверка просто по участникам корпрации
        //        // Можно уменьшить колиество ошибок
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var r = _dbContext.Evezone_SsoRequests.FirstOrDefault(x => x.owner_id == ssos.Key);
        //            if (r != null && r.ssoRecordsUpdates == 0 && (DateTime.UtcNow - r.onDateTime).TotalDays == 0)
        //                continue;
        //        }

        //        foreach (var sso in ssos)
        //        {
        //            bool r = ExecuteCommand(sso);
        //            if (r) 
        //            {
        //                successedCorps.Add(sso.corporation_id);
        //                break;
        //            }
        //        }
        //    }

        //    // Удаление ролей неуспешных корпораций
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        _dbContext.Eveonline_CorporationMemberRoles.RemoveRange(_dbContext.Eveonline_CorporationMemberRoles.Where(x => !successedCorps.Contains(x.corporation_id)));
        //        _dbContext.SaveChanges();
        //    }

        //    int personalManagerInCorp(int corporation_id, List<int> chars)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var pm = _dbContext.Eveonline_CorporationMemberRoles.FirstOrDefault(x => x.corporation_id == corporation_id && chars.Contains(x.character_id) && x.roles.Contains(l_needed_roles[0]));
        //            return pm?.character_id ?? 0;
        //        }
        //    }

        //    bool ExecuteCommand(CharacterCorporationAuthSso sso)
        //    {
        //        int dbChanges = 0;
        //        // Проверка на npc корпорацию
        //        if (_eveOnlineGeneric.IsNpcCorporation(sso.corporation_id))
        //            return true;

        //        // Выполнение запроса
        //        var ConnectorResult = SsoOnePage<CorporationRolesResult, CorporationRolesResult.CorporationRolesItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetMemberRoles, sso.corporation_id, folder, jobName);

        //        if (ConnectorResult.success)
        //        {
        //            using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //            {
        //                // Удаление неактуальных
        //                var dbPredicate = new Func<EveOnlineCorporationMemberRoles, bool>(x => x.corporation_id == sso.corporation_id);
        //                var toRemovePredicate = new Func<EveOnlineCorporationMemberRoles, bool>(x => !ConnectorResult.items.Any(xx => xx.character_id == x.character_id));
        //                var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //                dbChanges += db_values.changes;

        //                // Добавление и обновление
        //                foreach (var roles in ConnectorResult.items ?? new CorporationRolesResult())
        //                {
        //                    var predicate = new Func<EveOnlineCorporationMemberRoles, bool>(x => x.character_id == roles.character_id);
        //                    var newValue = new EveOnlineCorporationMemberRoles() { corporation_id = sso.corporation_id };
        //                    GenericOperations.UpdateItem(roles, db_values.items, predicate, newValue, _dbContext);
        //                }

        //                dbChanges += _dbContext.SaveChanges();

        //                // Проверка на наличие в БД этих персонажей
        //                Expression<Func<EveOnlineCharacter, int>> exp = x => x.character_id;
        //                _eveOnlineGeneric.Add_Items(
        //                    ConnectorResult.items.Select(x => x.character_id).ToArray(),
        //                    exp,
        //                    CacheKeysList.Universe_CharacterIds
        //                );
        //            }
        //        }

        //        AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationRoles, ConnectorResult.items != null ? ConnectorResult.items.Count : 0, dbChanges);
        //        return ConnectorResult.success;
        //    }
        //}
    }
}
