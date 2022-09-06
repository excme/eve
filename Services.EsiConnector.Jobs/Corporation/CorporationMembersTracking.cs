using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationMembersTracking : ConnectorJob
    {
        //static string l_reqName = "Corporation_MemberTracking";
        //static string l_scope = Scope.Corporations.TrackMembers.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //static string[] l_needed_roles = new string[] { "Director" };
        //public CorporationMembersTracking() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationMembersTracking(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выполнение запроса
        //    var ConnectorResult = SsoOnePage<CorporationMembertrackingResult, CorporationMembertrackingResult.CorporationMembertrackingItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetMemberTracking, sso.corporation_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCorporationMemberTrackingItem, bool>(x => x.corporation_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineCorporationMemberTrackingItem, bool>(x => !ConnectorResult.items.Any(xx => xx.character_id == x.character_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление и обновление
        //            foreach (var memberTracking in ConnectorResult.items ?? new CorporationMembertrackingResult())
        //            {
        //                var predicate = new Func<EveOnlineCorporationMemberTrackingItem, bool>(x => x.character_id == memberTracking.character_id);
        //                var newValue = new EveOnlineCorporationMemberTrackingItem() { corporation_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(memberTracking, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();

        //            // Проверка на наличие в БД этих персонажей
        //            Expression<Func<EveOnlineCharacter, int>> exp = x => x.character_id;
        //            _eveOnlineGeneric.Add_Items(
        //                ConnectorResult.items.Select(x => x.character_id).ToArray(),
        //                exp,
        //                CacheKeysList.Universe_CharacterIds
        //            );
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationMembertracking, ConnectorResult.items != null ? ConnectorResult.items.Count : 0, dbChanges);
        //}
    }
}
