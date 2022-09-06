using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterBlueprints : ConnectorJob
    {
        //static string l_reqName = "Character_Blueprints";
        //static string l_scope = Scope.Characters.ReadBlueprints.Name;
        //static ERequestFolder l_folder = ERequestFolder.Character;
        //public CharacterBlueprints() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterBlueprints(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = characterToUpdate;
            
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        int dbChanges = 0;
        //        // Выкачивание
        //        var ConnectorResult = SsoPaged<BlueprintsResult, BlueprintsResult.BlueprintsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.GetBlueprints, sso.character_id, folder, jobName, 1000);

        //        if (ConnectorResult.success)
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineBlueprint, bool>(x => x.owner_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineBlueprint, bool>(x => !ConnectorResult.items.Any(xx => xx.item_id == x.item_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление текущих
        //            foreach (var _blueprint in ConnectorResult.items)
        //            {
        //                var predicate = new Func<EveOnlineBlueprint, bool>(x => x.item_id == _blueprint.item_id);
        //                var newValue = new EveOnlineBlueprint() { owner_id = sso.character_id };
        //                GenericOperations.UpdateItem(_blueprint, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }

        //        AddSsoRequest(sso.character_id, ESsoRequestType.characterBlueprints, ConnectorResult.items.Count, dbChanges);
        //    }
        //}
    }
}
