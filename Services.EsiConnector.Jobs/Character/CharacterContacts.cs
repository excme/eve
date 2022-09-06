using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterContacts : ConnectorJob
    {
        //static string l_reqName = "Character_Contacts";
        //static string l_scope = Scope.Characters.ReadContacts.Name;
        //static ERequestFolder l_folder = ERequestFolder.Character;
        //public CharacterContacts() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterContacts(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = characterToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    // Выполнение запрос контрактов
        //    var authConnector = GetEsiConnectorAuth(sso.eveOnlineSsoData); int prevPage = 0; var isSuccess = false;
        //    (CharacterContactsResult value, bool success, DateTime expireOn, string message) tempConnectorResult;
        //    var ConnectorResult = new CharacterContactsResult();
        //    do
        //    {
        //        prevPage++;
        //        Func<Task<EsiResponse>> запросКоннектора1 = new Func<Task<EsiResponse>>(authConnector.Character.Contacts.GetContacts(sso.character_id, prevPage).ExecuteAsync);
        //        tempConnectorResult = _eveOnlineGeneric.ExecuteRequest<CharacterContactsResult>(запросКоннектора1, folder, CharacterContactsResult.TimeExpire(), CharacterContactsResult.GetArgs(sso.character_id)).GetAwaiter().GetResult();
        //        if (prevPage == 1) isSuccess = tempConnectorResult.success;

        //        if (tempConnectorResult.value?.Count > 0) ConnectorResult.AddRange(tempConnectorResult.value);
        //    } while (tempConnectorResult.value?.Count == 1000);

        //    if (isSuccess)
        //    {
        //        _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterContacts, ConnectorResult.Count);

        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            foreach (var _contact in ConnectorResult)
        //            {
        //                var db_value = _dbContext.Eveonline_Contacts.FirstOrDefault(x => x.owner_id == sso.character_id && x.contact_id == _contact.contact_id);
        //                if (db_value == null)
        //                {
        //                    db_value = new EveOnlineContact() { owner_id = sso.character_id, contact_id = _contact.contact_id };
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
