using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterIndustryMining : ConnectorJob
    {
        //static string l_reqName = "Character_IndustryMining";
        //static string l_scope = Scope.Industry.ReadCharacterMining.Name;
        //static ERequestFolder l_folder = ERequestFolder.Industry;
        //public CharacterIndustryMining() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterIndustryMining(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int characterToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = characterToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Если первый раз, то выкачиваем историю
        //    var characterMining = new List<EveOnlineCharacterIndustryMining>();
        //    EveOnlineCharacterIndustryMining lastItem = null;
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        characterMining = _dbContext.Eveonline_CharacterIndustryMinings.Where(x => x.character_id == sso.character_id).ToList();
        //        lastItem = characterMining != null && characterMining.Any() ? characterMining.Last() : null;

        //        // Выполнение запроса
        //        var breakPredicate = lastItem != null ? new Func<CharacterIndustryMiningResult.CharacterIndustryMiningItem, bool>(x => x.date.Date < lastItem.date.Date) : null;
        //        var ConnectorResult = SsoPaged<CharacterIndustryMiningResult, CharacterIndustryMiningResult.CharacterIndustryMiningItem>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Industry.GetMiningLedger, sso.character_id, folder, jobName, 1000, breakPredicate);

        //        // Сохранение
        //        if (ConnectorResult.success)
        //        {
        //            if (ConnectorResult.items?.Count > 0)
        //            {
        //                foreach (var miningItem in ConnectorResult.items ?? new CharacterIndustryMiningResult())
        //                {
        //                    var predicate = new Func<EveOnlineCharacterIndustryMining, bool>(x => x.date.Date == miningItem.date.Date && x.solar_system_id == miningItem.solar_system_id && x.type_id == miningItem.type_id);
        //                    var newValue = new EveOnlineCharacterIndustryMining() { character_id = sso.character_id };

        //                    GenericOperations.UpdateItem(miningItem, characterMining, predicate, newValue, _dbContext);
        //                }
        //            }

        //            dbChanges = _dbContext.SaveChanges();
        //        }

        //        AddSsoRequest(sso.character_id, ESsoRequestType.characterIndustryMining, ConnectorResult.items?.Count ?? 0, dbChanges);
        //    }
        //}
    }
}
