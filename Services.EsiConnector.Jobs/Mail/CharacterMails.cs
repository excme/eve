using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterMails : ConnectorJob
    {
        //static string l_reqName = "Character_Mails";
        //static string l_scope = Scope.Mail.ReadMail.Name;
        //static ERequestFolder l_folder = ERequestFolder.Mail;
        //public CharacterMails() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterMails(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        // Выполнение запрос контрактов
        //        var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData); int prevPage = 0; var isSuccess = false;
        //        (CharacterMailsResult value, bool success, DateTime expireOn, string message) tempConnectorResult;
        //        var ConnectorResult = new CharacterMailsResult();
        //        do
        //        {
        //            prevPage++;
        //            Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(authConnector.Character.Mail.GetHeaders(sso.character_id, prevPage).ExecuteAsync);
        //            tempConnectorResult = _eveOnlineGeneric.ExecuteRequest<CharacterMailsResult>(запросКоннектора1, folder, CharacterMailsResult.TimeExpire(), CharacterMailsResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();
        //            if (prevPage == 1) isSuccess = tempConnectorResult.success;

        //            if (tempConnectorResult.value?.Count > 0) ConnectorResult.AddRange(tempConnectorResult.value);
        //        } while (tempConnectorResult.value?.Count == 1000);

        //        _logger.LogInformation($"{jobName}. character {sso.character_id} success = {isSuccess}. # {ConnectorResult.Count}");

        //        if (isSuccess)
        //        {
        //            _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterMail, ConnectorResult.Count);

        //            foreach (var _mail in ConnectorResult)
        //            {
        //                // Добавление новых писем
        //                var db_value = _dbContext.Eveonline_Mails.FirstOrDefault(x => x.mail_id == _mail.mail_id);
        //                if (db_value == null)
        //                {
        //                    db_value = new EveOnlineMail() { character_id = sso.character_id };
        //                    db_value.UpdateProperties(_mail);
        //                    _dbContext.Eveonline_Mails.Add(db_value);
        //                    _dbContext.SaveChanges();

        //                    // Добавление получателей
        //                    foreach (var recept in _mail.recipients)
        //                    {
        //                        var db_receipt = new EveOnlineMailRecipient();
        //                        db_receipt.mail_id = _mail.mail_id;
        //                        db_receipt.recipient_id = recept.recipient_id;
        //                        _dbContext.Eveonline_MailRecipients.Add(db_receipt);
        //                        _dbContext.SaveChanges();
        //                    }

        //                    // Добавление тела письма
        //                    Func<Task<EsiResponse>> запросКоннектора2 = new Func<Task<EsiResponse>>(authConnector.Character.Mail.GetMail(sso.character_id, _mail.mail_id).ExecuteAsync);
        //                    var mailInfoResult = _eveOnlineGeneric.ExecuteRequest<CharacterMailInfoResult>(запросКоннектора2, folder, CharacterMailInfoResult.TimeExpire(), CharacterMailInfoResult.GetArgs(sso.character_id, _mail.mail_id)).GetAwaiter().GetResult();
        //                    if (mailInfoResult.success)
        //                    {
        //                        try
        //                        {
        //                            HtmlDocument htmlDoc = new HtmlDocument();
        //                            htmlDoc.LoadHtml(mailInfoResult.value.body);
        //                            db_value.body = htmlDoc.DocumentNode.InnerText;

        //                            _dbContext.Eveonline_Mails.Update(db_value);
        //                            _dbContext.SaveChanges();
        //                        } catch(Exception ex) { }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
