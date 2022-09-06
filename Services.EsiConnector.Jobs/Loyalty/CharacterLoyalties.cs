using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterLoyalties : ConnectorJob
    {
        //static string l_reqName = "Character_Loyalties";
        //static string l_scope = Scope.Characters.ReadLoyalty.Name;
        //static ERequestFolder l_folder = ERequestFolder.Loyalty;
        //public CharacterLoyalties() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterLoyalties(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = characterToUpdate;
        //}

        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CharacterLoyaltyPointsResult, CharacterLoyaltyPointsResult.CharacterLoyaltyPointsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Loyalty.GetAll, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCharacterLoyalty, bool>(x => x.character_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineCharacterLoyalty, bool>(x => !ConnectorResult.items.Any(xx => xx.corporation_id == x.corporation_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            foreach (var loyalty in ConnectorResult.items)
        //            {
        //                // Добавление и изменение из обновления
        //                var predicate = new Func<EveOnlineCharacterLoyalty, bool>(x => x.corporation_id == loyalty.corporation_id);
        //                var newValue = new EveOnlineCharacterLoyalty() { character_id = sso.character_id };

        //                GenericOperations.UpdateItem(loyalty, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterLoyalties, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
