using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterIndustryJobs : ConnectorJob
    {
        //static string l_reqName = "Character_IndustryJobs";
        //static string l_scope = Scope.Industry.ReadCharacterJobs.Name;
        //static ERequestFolder l_folder = ERequestFolder.Industry;
        //public CharacterIndustryJobs() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterIndustryJobs(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        // Выполнение запроса
        //        var ConnectorResult = SsoOnePage<CharacterIndustryJobsResult, CharacterIndustryJobsResult.CharacterIndustryJobsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Industry.GetJobs, sso.character_id, folder, jobName);

        //        if (ConnectorResult.success)
        //        {
        //            var dbJobs = _dbContext.Eveonline_IndustryJobs.AsNoTracking().Where(x => x.owner_id == sso.character_id).ToList();
        //            foreach (var job in ConnectorResult.items)
        //            {
        //                var dbJob = dbJobs.FirstOrDefault(x => x.job_id == job.job_id);
        //                if (dbJob == null)
        //                {
        //                    dbJob = new EveOnlineIndustryJob() { owner_id = sso.character_id };
        //                    _dbContext.Eveonline_IndustryJobs.Add(dbJob);
        //                    _dbContext.SaveChanges();
        //                }

        //                var updated = IUpdateCompareProperties.UpdateProperties(ref dbJob, job);
        //                if (updated)
        //                    _dbContext.Eveonline_IndustryJobs.Update(dbJob);

        //            }

        //            dbChanges = _dbContext.SaveChanges();
        //        }

        //        AddSsoRequest(sso.character_id, ESsoRequestType.characterIndustryJobs, ConnectorResult.items?.Count ?? 0, dbChanges);
        //    }
        //}
    }
}
