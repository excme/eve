using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterCloneImplants : ConnectorJob
    {
        //static string l_reqName = "Character_CloneImplants";
        //static string l_scope = Scope.Clones.ReadImplants.Name;
        //static ERequestFolder l_folder = ERequestFolder.Clones;
        //public CharacterCloneImplants() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterCloneImplants(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) 
        //    : base(genericService, options, logger, l_reqName, l_folder, l_scope) {
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOneItem<CharacterImplantsResult>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Clones.GetImplants, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var character_cloneImplants = _dbContext.Eveonline_CharacterCloneImplants.FirstOrDefault(x => x.character_id == sso.character_id);

        //            if (character_cloneImplants == null)
        //            {
        //                character_cloneImplants = new EveOnlineCharacterCloneImplant() { character_id = sso.character_id };
        //                _dbContext.Eveonline_CharacterCloneImplants.Add(character_cloneImplants);
        //                dbChanges += _dbContext.SaveChanges();
        //            }

        //            CompareLogic compareLogic = new CompareLogic(new ComparisonConfig() { IgnoreObjectTypes = true, MaxMillisecondsDateDifference = 61 * 1000 });
        //            ComparisonResult result = compareLogic.Compare(ConnectorResult.item, character_cloneImplants.implants);
        //            if (!result.AreEqual)
        //            {
        //                character_cloneImplants.UpdateProperties(ConnectorResult.item);
        //                _dbContext.Eveonline_CharacterCloneImplants.Update(character_cloneImplants);
        //                dbChanges += _dbContext.SaveChanges();
        //            }
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterImplants, ConnectorResult.success ? 1 : 0, dbChanges);
        //}
    }
}
