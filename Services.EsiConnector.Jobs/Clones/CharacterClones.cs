using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterClones : ConnectorJob
    {
        //static string l_reqName = "Character_Clones";
        //static string l_scope = Scope.Clones.ReadClones.Name;
        //static ERequestFolder l_folder = ERequestFolder.Clones;
        //public CharacterClones() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterClones(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int character_to_update = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope) {
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = character_to_update;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOneItem<CharacterClonesResult>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Clones.GetClones, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var character_clones = _dbContext.Eveonline_CharacterClones.FirstOrDefault(x => x.character_id == sso.character_id);

        //            if (character_clones == null)
        //            {
        //                character_clones = new EveOnlineCharacterClone() { character_id = sso.character_id };
        //                _dbContext.Eveonline_CharacterClones.Add(character_clones);
        //                dbChanges += _dbContext.SaveChanges();
        //            }

        //            var updated = IUpdateCompareProperties.UpdateProperties(ref character_clones, ConnectorResult.item);
        //            if (updated)
        //            {
        //                _dbContext.Eveonline_CharacterClones.Update(character_clones);
        //                dbChanges += _dbContext.SaveChanges();
        //            }
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterClones, ConnectorResult.success ? 1 : 0, dbChanges);
        //}
    }
}
