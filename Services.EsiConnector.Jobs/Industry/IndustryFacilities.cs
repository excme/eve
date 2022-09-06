using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class IndustryFacilities : ConnectorJob
    {
        //static string l_reqName = "Industry_Facilities";
        //static ERequestFolder l_folder = ERequestFolder.Industry;
        //public IndustryFacilities() : base(l_reqName, l_folder, _withSso:false) { }
        //public IndustryFacilities(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30) : base(genericService, options, logger, l_reqName, l_folder, _withSso: false)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob()
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<IndustryFacilitiesResult, IndustryFacilitiesResult.IndustryFacilitiesItem>(connector.Industry.GetFacilities, 0, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Очистка старых записей
        //            var dbPredicate = new Func<EveOnlineIndustryFacility, bool>(x => x.solar_system_id > 0);
        //            var toRemovePredicate = new Func<EveOnlineIndustryFacility, bool>(x => !ConnectorResult.items.Any(xx => xx.solar_system_id == x.solar_system_id && xx.facility_id == x.facility_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавлени и изменение из обновления
        //            foreach (var indFacility in ConnectorResult.items ?? new IndustryFacilitiesResult())
        //            {
        //                var predicate = new Func<EveOnlineIndustryFacility, bool>(x => x.solar_system_id == indFacility.solar_system_id && x.facility_id == indFacility.facility_id);
        //                var newValue = new EveOnlineIndustryFacility();

        //                GenericOperations.UpdateItem(indFacility, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(-255, ESsoRequestType.industryFacilities, ConnectorResult.items?.Count ?? 0, dbChanges);
        //}
    }
}
