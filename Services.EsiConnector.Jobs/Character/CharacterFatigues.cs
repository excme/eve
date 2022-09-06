using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterFatigues : ConnectorJob
    {
        //static string l_reqName = "Character_Fatigues";
        //static string l_scope = Scope.Characters.ReadFatigue.Name;
        //static ERequestFolder l_folder = ERequestFolder.Character;
        //public CharacterFatigues() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterFatigues(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Запрос
        //    var ConnectorResult = SsoOneItem<CharacterFatigueResult>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.GetJumpFatigue, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var db_value = _dbContext.Eveonline_CharacterFatigues.FirstOrDefault(x => x.character_id == sso.character_id);

        //            if (db_value == null)
        //            {
        //                db_value = new EveOnlineCharacterFatigue() { character_id = sso.character_id };
        //                _dbContext.Eveonline_CharacterFatigues.Add(db_value);
        //                dbChanges += _dbContext.SaveChanges();
        //            }

        //            var updated = IUpdateCompareProperties.UpdateProperties(ref db_value, ConnectorResult.item);
        //            if (updated)
        //            {
        //                _dbContext.Eveonline_CharacterFatigues.Update(db_value);
        //                dbChanges += _dbContext.SaveChanges();
        //            }
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterFatigues, ConnectorResult.success ? 1 : 0, dbChanges);
        //}
    }
}
