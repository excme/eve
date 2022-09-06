using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationCustomsOffices : ConnectorJob
    {
        //static string l_reqName = "Corporation_PlanetaryCustomsOffices";
        //static string l_scope = Scope.Planets.ReadCustomsOffices.Name;
        //static ERequestFolder l_folder = ERequestFolder.PlanetaryInteraction;
        //static string[] l_needed_roles = new string[] { "Director" };
        //public CorporationCustomsOffices() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationCustomsOffices(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<CorporationCustomOfficesResult, CorporationCustomOfficesResult.CorporationCustomOfficesItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Planetary.GetCustomsOffices, sso.corporation_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        // Сохранение
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCorporationCustomsOffice, bool>(x => x.corporation_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineCorporationCustomsOffice, bool>(x => !ConnectorResult.items.Any(xx => xx.office_id == x.office_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавлени и изменение из обновления
        //            foreach (var custom_office in ConnectorResult.items ?? new CorporationCustomOfficesResult())
        //            {
        //                var predicate = new Func<EveOnlineCorporationCustomsOffice, bool>(x => x.office_id == custom_office.office_id);
        //                var newValue = new EveOnlineCorporationCustomsOffice() { corporation_id = sso.corporation_id};
        //                GenericOperations.UpdateItem(custom_office, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationCorporationCustomsOffices, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
