using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterStats : ConnectorJob
    {
        //static string l_reqName = "Character_Stats";
        //static string l_scope = Scope.Characters.ReadStats.Name;
        //static ERequestFolder l_folder = ERequestFolder.Character;
        //public CharacterStats() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterStats(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int character_to_update = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = character_to_update;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CharacterStatResult, CharacterStatResult.CharacterStatItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.GetStats, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        // Сохранение
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCharacterStatsItem, bool>(x => x.character_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineCharacterStatsItem, bool>(x => !ConnectorResult.items.Any(xx => xx.year == x.year));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавлени и изменение из обновления
        //            foreach (var stat in ConnectorResult.items ?? new CharacterStatResult())
        //            {
        //                var predicate = new Func<EveOnlineCharacterStatsItem, bool>(x => x.year == stat.year);
        //                var newValue = new EveOnlineCharacterStatsItem() { character_id = sso.character_id };
        //                GenericOperations.UpdateItem(stat, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterStats, ConnectorResult.items!= null ? ConnectorResult.items.Count : 0, dbChanges);
        //}
    }
}
