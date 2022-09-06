using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationKillmails : ConnectorJob
    {
        //static string l_reqName = "Corporation_Killmails";
        //static string l_scope = Scope.Killmails.ReadCorporationKillmails.Name;
        //static ERequestFolder l_folder = ERequestFolder.Killmails;
        //static string[] l_needed_roles = new string[] { "Director" };
        //public CorporationKillmails() : base(l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles) { }
        //public CorporationKillmails(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope, _needed_roles: l_needed_roles)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    //using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    //{
        //    //    int prevPage = 0; var isSuccess = false;
        //    //    (KillmailsRecentResult value, bool success, DateTime expireOn, string message) tempConnectorResult;
        //    //    var ConnectorResult = new KillmailsRecentResult();
        //    //    do
        //    //    {
        //    //        prevPage++;
        //    //        Func<Task<EsiResponse>> запросКоннектора = new Func<Task<EsiResponse>>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Corporation.Killmails.GetRecent(sso.corporation_id, prevPage).ExecuteAsync);
        //    //        tempConnectorResult = _eveOnlineGeneric.ExecuteRequest<KillmailsRecentResult>(запросКоннектора, folder, KillmailsRecentResult.TimeExpire(), KillmailsRecentResult.GetArgsChar(sso.corporation_id)).GetAwaiter().GetResult();
        //    //        if (prevPage == 1) isSuccess = tempConnectorResult.success;

        //    //        if (tempConnectorResult.value?.Count > 0) ConnectorResult.AddRange(tempConnectorResult.value);
        //    //    } while (tempConnectorResult.value?.Count == 1000);

        //    //    _logger.LogInformation($"{reqName}. corporation {sso.corporation_id} success = {isSuccess}");

        //    //    if (isSuccess)
        //    //    {
        //    //        _eveOnlineGeneric.Sso_RequestStatistic(sso.corporation_id, ESsoRequestType.corporationKillmails, ConnectorResult.Count);

        //    //        foreach (var killmail in ConnectorResult)
        //    //        {
        //    //            // Проверка killmail
        //    //            var db_killmail = _dbContext.EveOnlineKillMails.FirstOrDefault(x => x.killmail_id == killmail.killmail_id && x.killmail_hash == killmail.killmail_hash);

        //    //            if (db_killmail == null)
        //    //            {
        //    //                // Добавление 
        //    //                EveOnlineKillMail eveOnlineKillMail = new EveOnlineKillMail() { killmail_hash = killmail.killmail_hash, killmail_id = killmail.killmail_id, killmail_time = new DateTime(2000,1,1)};
        //    //                _dbContext.EveOnlineKillMails.Add(eveOnlineKillMail);
        //    //                _dbContext.SaveChanges();
        //    //                _eveOnlineGeneric.Killmail_UpdateKillmailDetails(killmail.killmail_id, killmail.killmail_hash);
        //    //            }

        //    //            // Добавление связей
        //    //            var killmail_ref = _dbContext.EveOnlineKillMailsParticipantRefs.FirstOrDefault(x => x.killmail_id == killmail.killmail_id && x.participant_id == sso.corporation_id);
        //    //            if (killmail_ref == null)
        //    //            {
        //    //                killmail_ref = new EveOnlineKillMail.ParticipantRef() { killmail_id = killmail.killmail_id, participant_id = sso.corporation_id };
        //    //                _dbContext.EveOnlineKillMailsParticipantRefs.Add(killmail_ref);
        //    //                _dbContext.SaveChanges();
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //}
    }
}
