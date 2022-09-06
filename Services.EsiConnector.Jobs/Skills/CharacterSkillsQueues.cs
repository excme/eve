using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterSkillsQueues : ConnectorJob
    {
        //static string l_reqName = "Character_SkillQueues";
        //static string l_scope = Scope.Skills.ReadSkillQueue.Name;
        //static ERequestFolder l_folder = ERequestFolder.Skills;
        //public CharacterSkillsQueues() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterSkillsQueues(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CharacterSkillqueueResult, CharacterSkillqueueResult.CharacterSkillsItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Skills.GetQueue, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCharacterSkillQueue, bool>(x => x.character_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineCharacterSkillQueue, bool>(x => !ConnectorResult.items.Any(xx => xx.skill_id == x.skill_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate,  toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление текущих
        //            foreach (var skillQueue in ConnectorResult.items ?? new CharacterSkillqueueResult())
        //            {
        //                var predicate = new Func<EveOnlineCharacterSkillQueue, bool>(x => x.skill_id == skillQueue.skill_id);
        //                var newValue = new EveOnlineCharacterSkillQueue() { character_id = sso.character_id };

        //                GenericOperations.UpdateItem(skillQueue, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterSkillsQueues, ConnectorResult.items?.Count ?? 0, dbChanges);
        //}
    }
}
