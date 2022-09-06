using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class IndustrySystems : ConnectorJob
    {
        //static string l_reqName = "Industry_Systems";
        //static ERequestFolder l_folder = ERequestFolder.Industry;
        //public IndustrySystems() : base(l_reqName, l_folder, _withSso: false) { }
        //public IndustrySystems(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30) : base(genericService, options, logger, l_reqName, l_folder, _withSso: false)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob()
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<IndustrySystemsResult, IndustrySystemsResult.IndustrySystemsItem>(connector.Industry.GetIndices, 0, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var connectorResultPrepared = ConnectorResult.items
        //                .SelectMany(xy => xy.cost_indices.Select(x => new EveOnlineIndustrySystemParent() { activity = x.activity, cost_index = x.cost_index, solar_system_id = xy.solar_system_id })).ToList();
        //            var dbPredicate = new Func<EveOnlineIndustrySystem, bool>(x => x.solar_system_id > 0);
        //            var toRemovePredicate = new Func<EveOnlineIndustrySystem, bool>(x => !connectorResultPrepared.Any(xx => xx.solar_system_id == x.solar_system_id && xx.activity == x.activity));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавлени и изменение из обновления
        //            foreach (var indSystem in connectorResultPrepared ?? new List<EveOnlineIndustrySystemParent>())
        //            {
        //                var predicate = new Func<EveOnlineIndustrySystem, bool>(x => x.solar_system_id == indSystem.solar_system_id && x.activity == indSystem.activity);
        //                var newValue = new EveOnlineIndustrySystem() { solar_system_id = indSystem.solar_system_id };

        //                GenericOperations.UpdateItem(indSystem, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    _eveOnlineGeneric.Sso_RequestStatistic(-255, ESsoRequestType.industrySystems, ConnectorResult.items?.Count ?? 0, dbChanges);
        //}
    }
}
