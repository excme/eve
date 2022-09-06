using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterFittings : ConnectorJob
    {
        //static string l_reqName = "Character_Fittings";
        //static string l_scope = Scope.Fittings.ReadFittings.Name;
        //static ERequestFolder l_folder = ERequestFolder.Fittings;
        //public CharacterFittings() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterFittings(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 30, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _itemToUpdate = characterToUpdate;
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CharacterFittingsResult, CharacterFittingsResult.CharacterFittingsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Fittings.GetAll, sso.character_id, folder, jobName);
        //    if (ConnectorResult.success)
        //    {
        //        // Сохранение
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCharacterFitting, bool>(x => x.character_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineCharacterFitting, bool>(x => !ConnectorResult.items.Any(xx => xx.fitting_id == x.fitting_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление и изменение из обновления
        //            foreach (var _fitting in ConnectorResult.items ?? new CharacterFittingsResult())
        //            {
        //                var predicate = new Func<EveOnlineCharacterFitting, bool>(x => x.fitting_id == _fitting.fitting_id);
        //                var newValue = new EveOnlineCharacterFitting() { character_id = sso.character_id };
        //                GenericOperations.UpdateItem(_fitting, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterFitting, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
