using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CorporationContacts : ConnectorJob
    {
        //static string l_reqName = "Corporation_Starbases";
        //static string l_scope = Scope.Corporations.ReadBlueprints.Name;
        //static ERequestFolder l_folder = ERequestFolder.Corporation;
        //public CorporationContacts() : base(l_reqName, l_folder, l_scope) { }
        //public CorporationContacts(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    // Выполнение запрос контрактов
        //    var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData); int prevPage = 0; var isSuccess = false;
        //    (CorporationContactsResult value, bool success, DateTime expireOn, string message) tempConnectorResult;
        //    var ConnectorResult = new CorporationContactsResult();

        //    do
        //    {
        //        prevPage++;
        //        Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(authConnector.Corporation.Contacts.GetContacts(sso.corporation_id, prevPage).ExecuteAsync);
        //        tempConnectorResult = _eveOnlineGeneric.ExecuteRequest<CorporationContactsResult>(запросКоннектора1, folder, CorporationContactsResult.TimeExpire(), CorporationContactsResult.GetArgs(sso.corporation_id)).GetAwaiter().GetResult();
        //        if (prevPage == 1) isSuccess = tempConnectorResult.success;

        //        if (tempConnectorResult.value?.Count > 0) ConnectorResult.AddRange(tempConnectorResult.value);
        //    } while (tempConnectorResult.value?.Count == 1000);

        //    if (isSuccess)
        //    {
        //        _eveOnlineGeneric.Sso_RequestStatistic(sso.corporation_id, ESsoRequestType.corporationContacts, ConnectorResult.Count);

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Сохранение контрактов в бд
        //            foreach (var _contact in ConnectorResult)
        //            {
        //                // Добавление и обновление
        //                var db_value = _dbContext.Eveonline_Contacts.FirstOrDefault(x => x.owner_id == sso.corporation_id && x.contact_id == _contact.contact_id);
        //                if (db_value == null)
        //                {
        //                    db_value = new EveOnlineContact() { owner_id = sso.corporation_id};
        //                    db_value.UpdateProperties(_contact);
        //                    _dbContext.Eveonline_Contacts.Add(db_value);

        //                    // Добавление персонажа/корпорации/альянса/факции/другого
        //                    _eveOnlineGeneric.Generic_AddById(db_value.contact_id);
        //                }
        //                else
        //                {
        //                    db_value.UpdateProperties(_contact);
        //                    _dbContext.Eveonline_Contacts.Update(db_value);
        //                }

        //                _dbContext.SaveChanges();
        //            }
        //        }
        //    }
        //}
    }
}
