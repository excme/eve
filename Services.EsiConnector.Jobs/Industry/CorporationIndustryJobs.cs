using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationIndustryJobs : ConnectorJob
    {
        //static string l_reqName = "Corporation_IndustryJobs";
        //static string l_scope = Scope.Industry.ReadCorporationJobs.Name;
        //static ERequestFolder l_folder = ERequestFolder.Industry;
        //static string[] l_needed_roles = new string[] { "Factory_Manager" };
        //public CorporationIndustryJobs() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationIndustryJobs(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { _maxCharactersToUpdate = maxCharactersToUpdate; }
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoPaged<CorporationIndustryJobsResult, CorporationIndustryJobsResult.CorporationIndustryJobsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Industry.GetJobs, sso.corporation_id, folder, jobName, 1000);

        //    // Сохранение
        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var db_jobs = _dbContext.Eveonline_IndustryJobs.Where(x => x.owner_id == sso.corporation_id);
        //            foreach (var job in ConnectorResult.items)
        //            {
        //                var dbJob = db_jobs.FirstOrDefault(x => x.job_id == job.job_id);

        //                if (dbJob == null)
        //                {
        //                    dbJob = new EveOnlineIndustryJob() { owner_id = sso.corporation_id };
        //                    _dbContext.Eveonline_IndustryJobs.Add(dbJob);
        //                    _dbContext.SaveChanges();
        //                }

        //                var updated = IUpdateCompareProperties.UpdateProperties(ref dbJob, job);
        //                if (updated)
        //                    _dbContext.Eveonline_IndustryJobs.Update(dbJob);
        //            }

        //            dbChanges = _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.corporation_id, ESsoRequestType.corporationIndustryJobs, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
