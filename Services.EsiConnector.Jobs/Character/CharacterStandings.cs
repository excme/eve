using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterStandings : ConnectorJob
    {
        //static string l_reqName = "Character_Standings";
        //static string l_scope = Scope.Characters.ReadStandings.Name;
        //static ERequestFolder l_folder = ERequestFolder.Character;
        //public CharacterStandings() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterStandings(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Запрос 
        //    var ConnectorResult = SsoOnePage<StandingsResult, StandingsResult.StandingsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.GetStandings, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineStanding, bool>(x => x.owner_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineStanding, bool>(x => !ConnectorResult.items.Any(xx => xx.from_id == x.from_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            foreach (var standing in ConnectorResult.items)
        //            {
        //                var predicate = new Func<EveOnlineStanding, bool>(x => x.from_id == standing.from_id);
        //                var newValue = new EveOnlineStanding() { owner_id = sso.character_id };
        //                GenericOperations.UpdateItem(standing, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterStandings, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
