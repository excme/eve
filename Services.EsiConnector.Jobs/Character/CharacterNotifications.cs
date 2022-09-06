using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterNotifications : ConnectorJob
    {
        //static string l_reqName = "Character_AgentsResearches";
        //static string l_scope = Scope.Characters.ReadNotifications.Name;
        //static ERequestFolder l_folder = ERequestFolder.Character;
        //public CharacterNotifications() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterNotifications(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = characterToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CharacterNotificationsResult, CharacterNotificationsResult.CharacterNotificationsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.GetNotifications, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        // Сохранение
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCharacterNotification, bool>(x => x.character_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineCharacterNotification, bool>(x => !ConnectorResult.items.Any(xx => xx.notification_id == x.notification_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавлени и изменение из обновления
        //            foreach (var notification in ConnectorResult.items ?? new CharacterNotificationsResult())
        //            {
        //                var predicate = new Func<EveOnlineCharacterNotification, bool>(x => x.notification_id == notification.notification_id);
        //                var newValue = new EveOnlineCharacterNotification() { character_id = sso.character_id };
        //                GenericOperations.UpdateItem(notification, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterNotifications, ConnectorResult.items != null ? ConnectorResult.items.Count : 0, dbChanges);
        //}
    }
}
