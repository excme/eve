using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationStructures : ConnectorJob
    {
        //static string l_reqName = "Corporation_Structures";
        //static string l_scope = Scope.Corporations.ReadStructures.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //static string[] l_needed_roles = new string[] { "Station_Manager" };
        //public CorporationStructures() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationStructures(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0, successResponces = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<CorporationStructuresResult, CorporationStructuresResult.CorporationStructuresItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetStructures, sso.corporation_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCorporationStructure, bool>(x => x.corporation_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineCorporationStructure, bool>(x => !ConnectorResult.items.Any(xx => xx.structure_id == x.structure_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление текущих
        //            foreach (var structure in ConnectorResult.items ?? new List<CorporationStructuresResult.CorporationStructuresItem>())
        //            {
        //                var predicate = new Func<EveOnlineCorporationStructure, bool>(x => x.structure_id == structure.structure_id);
        //                var newValue = new EveOnlineCorporationStructure() { corporation_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(structure, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += db_values.changes;

        //            // Проверка на наличие структур в БД
        //            Expression<Func<EveOnlineUniverseStructure, long>> exp = x => x.structure_id;
        //            _eveOnlineGeneric.Add_Items(
        //                ConnectorResult.items.Select(x => x.structure_id).ToArray(),
        //                exp,
        //                CacheKeysList.Universe_StructureIds,
        //                sso.corporation_id
        //            );
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationStructures, successResponces, dbChanges);
        //}
    }
}
