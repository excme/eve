using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationIndustryMiningExtractions : ConnectorJob
    {
        //static string l_reqName = "Corporation_IndustryMiningExtractions";
        //static string l_scope = Scope.Industry.ReadCorporationJobs.Name;
        //static ERequestFolder l_folder = ERequestFolder.Industry;
        //static string[] l_needed_roles = new string[] { "Station_Manager" };
        //public CorporationIndustryMiningExtractions() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationIndustryMiningExtractions(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { _maxCharactersToUpdate = maxCharactersToUpdate; }
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<CorporationMiningExtractionsResult, CorporationMiningExtractionsResult.CorporationMiningExtractionsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Industry.GetMoonExtractionTimers, sso.corporation_id, folder, jobName, 1000);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCorporationIndustryMinigExtraction, bool>(x => x.corporation_id == sso.corporation_id);
        //            var toRemovePredicate = new Func<EveOnlineCorporationIndustryMinigExtraction, bool>(x => !ConnectorResult.items.Any(xx => xx.moon_id == x.moon_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление текущих
        //            foreach (var miningExtr in ConnectorResult.items ?? new CorporationMiningExtractionsResult())
        //            {
        //                var predicate = new Func<EveOnlineCorporationIndustryMinigExtraction, bool>(x => x.moon_id == miningExtr.moon_id);
        //                var newValue = new EveOnlineCorporationIndustryMinigExtraction() { corporation_id = sso.corporation_id };
        //                GenericOperations.UpdateItem(miningExtr, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationIndustryMiningExtractions, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
