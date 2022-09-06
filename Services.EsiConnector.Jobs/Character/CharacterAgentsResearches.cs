using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterAgentsResearches : ConnectorJob
    {
        //static string l_reqName = "Character_AgentsResearches";
        //static string l_scope = Scope.Characters.ReadAgentsResearch.Name;
        //static ERequestFolder l_folder = ERequestFolder.Character;

        //public CharacterAgentsResearches() : base(l_reqName, l_folder, l_scope) {  }
        //public CharacterAgentsResearches(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = characterToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOnePage<CharacterAgentsResearchResult, CharacterAgentsResearchResult.CharacterAgentsResearchItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.GetAgentResearch, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        // Сохранение
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            // Удаление неактуальных
        //            var dbPredicate = new Func<EveOnlineCharacterAgentResearch, bool>(x => x.character_id == sso.character_id);
        //            var toRemovePredicate = new Func<EveOnlineCharacterAgentResearch, bool>(x => !ConnectorResult.items.Any(xx => xx.agent_id == x.agent_id));
        //            var db_values = GenericOperations.RemoveNotActual(dbPredicate, toRemovePredicate, _dbContext);
        //            dbChanges += db_values.changes;

        //            // Добавление и изменение из обновления
        //            foreach (var agent_research in ConnectorResult.items ?? new CharacterAgentsResearchResult())
        //            {
        //                var predicate = new Func<EveOnlineCharacterAgentResearch, bool>(x => x.agent_id == agent_research.agent_id);
        //                var newValue = new EveOnlineCharacterAgentResearch() { character_id = sso.character_id };
        //                GenericOperations.UpdateItem(agent_research, db_values.items, predicate, newValue, _dbContext);
        //            }

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterAgentResearches, ConnectorResult.items.Count, dbChanges);
        //}
    }
}
