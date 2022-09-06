using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationFacilities : ConnectorJob
    {
        //static string l_reqName = "Corporation_Facilities";
        //static string l_scope = Scope.Corporations.ReadDivisions.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //static string[] l_needed_roles = new string[] { "Factory_Manager" };
        //public CorporationFacilities() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationFacilities(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CorporationFacilitiesResult, CorporationFacilitiesResult.CorporationFacilitiesItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.GetFacilities, sso.corporation_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCorporationFacility, bool>(x => x.corporation_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineCorporationFacility, bool>(x => !ConnectorResult.items.Any(xx => xx.facility_id == x.facility_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            foreach (var facility in ConnectorResult.items ?? new CorporationFacilitiesResult())
        //            {
        //                var predicate = new Func<EveOnlineCorporationFacility, bool>(x => x.facility_id == facility.facility_id);
        //                var newValue = new EveOnlineCorporationFacility() { corporation_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(facility, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationFacilities, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
