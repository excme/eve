using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterMedals : ConnectorJob
    {
        //static string l_reqName = "Character_Medals";
        //static string l_scope = Scope.Characters.ReadMedals.Name;
        //static ERequestFolder l_folder = ERequestFolder.Character;
        //public CharacterMedals() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterMedals(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int character_to_update = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = character_to_update;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CharacterMedalsResult, CharacterMedalsResult.CharacterMedalsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.GetMedals, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        // Сохранение
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCharacterMedal, bool>(x => x.character_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineCharacterMedal, bool>(x => !ConnectorResult.items.Any(xx => xx.medal_id == x.medal_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавлени и изменение из обновления
        //            foreach (var medal in ConnectorResult.items ?? new CharacterMedalsResult())
        //            {
        //                var predicate = new Func<EveOnlineCharacterMedal, bool>(x => x.medal_id == medal.medal_id);
        //                var newValue = new EveOnlineCharacterMedal() { character_id = sso.character_id };
        //                GenericOperations.UpdateItem(medal, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterMedals, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
