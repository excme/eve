using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationContainersLogs : ConnectorJob
    {
        //static string l_reqName = "Corporation_ContainersLogs";
        //static string l_scope = Scope.Corporations.ReadContainerLogs.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //static string[] l_needed_roles = new string[] { "Director" };
        //public CorporationContainersLogs() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationContainersLogs(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<CorporationContainersLogResult, CorporationContainersLogResult.CorporationContainersLogItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetContainerLogs, sso.corporation_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCorporationContainersLog, bool>(x => x.corporation_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineCorporationContainersLog, bool>(x => !ConnectorResult.items.Any(xx => xx.container_id == x.container_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление текущих
        //            foreach (var containerLog in ConnectorResult.items ?? new CorporationContainersLogResult())
        //            {
        //                var predicate = new Func<EveOnlineCorporationContainersLog, bool>(x => x.container_id == containerLog.container_id);
        //                var newValue = new EveOnlineCorporationContainersLog() { corporation_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(containerLog, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationContainerLogs, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
