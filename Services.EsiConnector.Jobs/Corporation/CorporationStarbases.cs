using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationStarbases : ConnectorJob
    {
        //static string l_reqName = "Corporation_Starbases";
        //static string l_scope = Scope.Corporations.ReadStarbases.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //static string[] l_needed_roles = new string[] { "Director" };
        //public CorporationStarbases() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationStarbases(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //int dbChanges = 0, successResponces = 0;
        //void StarBase_All(CharacterCorporationAuthSso sso)
        //{
        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<CorporationStarbasesResult, CorporationStarbasesResult.CorporationStarbasesItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetStarbases, sso.corporation_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        successResponces += ConnectorResult.items.Count;

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCorporationStarbase, bool>(x => x.corporation_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineCorporationStarbase, bool>(x => !ConnectorResult.items.Any(xx => xx.starbase_id == x.starbase_id && xx.system_id == x.system_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление текущих
        //            foreach (var starbase in ConnectorResult.items ?? new CorporationStarbasesResult())
        //            {
        //                var predicate = new Func<EveOnlineCorporationStarbase, bool>(x => x.starbase_id == starbase.starbase_id && x.system_id == starbase.system_id);
        //                var newValue = new EveOnlineCorporationStarbase() { corporation_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(starbase, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }
        //}
        //void StarBase_UpdateDetails(CharacterCorporationAuthSso sso)
        //{
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        var db_values = _dbContext.Eveonline_CorporationStarbases.Where(x => x.corporation_id == sso.corporation_id).ToList();
        //        if (db_values.Any())
        //        {
        //            for (int i = 0; i < db_values.Count; i++)
        //            {
        //                var db_value = db_values[i];

        //                // Обновление деталей
        //                if (DateTime.UtcNow - db_value.lastDetailsUpdate <= TimeSpan.FromHours(4))
        //                    continue;

        //                var source = SsoOneItem<CorporationStarbasesInfoResult, int, long>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetStarbaseInfo, sso.corporation_id, db_value.system_id, db_value.starbase_id, folder, jobName);

        //                if (source.success)
        //                {
        //                    successResponces++;
        //                    IUpdateCompareProperties.UpdateProperties(ref db_value, source.item);
        //                }

        //                db_value.lastDetailsUpdate = DateTime.UtcNow;
        //                _dbContext.Eveonline_CorporationStarbases.Update(db_value);
        //            }
        //        }

        //        dbChanges += _dbContext.SaveChanges();
        //    }
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    StarBase_All(sso);
        //    StarBase_UpdateDetails(sso);
        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationStarbases, successResponces, dbChanges);
        //}
    }
}
