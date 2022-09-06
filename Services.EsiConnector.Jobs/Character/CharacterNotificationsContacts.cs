using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterNotificationsContacts : ConnectorJob
    {
        //static string l_reqName = "Character_NotificationsContacts";
        //static string l_scope = Scope.Characters.ReadNotifications.Name;
        //static ERequestFolder l_folder = ERequestFolder.Character;
        //public CharacterNotificationsContacts() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterNotificationsContacts(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = characterToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CharacterNotificationsContactsResult, CharacterNotificationsContactsResult.CharacterNotificationsContactsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.GetContactNotifications, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        // Сохранение
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCharacterNotificationContact, bool>(x => x.character_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineCharacterNotificationContact, bool>(x => !ConnectorResult.items.Any(xx => xx.notification_id == x.notification_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавлени и изменение из обновления
        //            foreach (var notification_contact in ConnectorResult.items ?? new CharacterNotificationsContactsResult())
        //            {
        //                var predicate = new Func<EveOnlineCharacterNotificationContact, bool>(x => x.notification_id == notification_contact.notification_id);
        //                var newValue = new EveOnlineCharacterNotificationContact() { character_id = sso.character_id };
        //                GenericOperations.UpdateItem(notification_contact, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterNotificationsContacts, ConnectorResult.items != null ? ConnectorResult.items.Count : 0, dbChanges);
        //}
    }
}
