using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterRoles : ConnectorJob
    {
        //static string l_reqName = "Character_Roles";
        //static string l_scope = Scope.Characters.ReadCorporationRoles.Name;
        //static ERequestFolder l_folder = ERequestFolder.Character;
        //public CharacterRoles() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterRoles(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;

        //    // Выкачивание
        //    var ConnectorResult = SsoOneItem<CharacterRolesResult>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.GetRoles, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var character_roles = _dbContext.Eveonline_CharacterRoles.FirstOrDefault(x => x.character_id == sso.character_id);

        //            if (character_roles == null)
        //            {
        //                character_roles = new EveOnlineCharacterRole() { character_id = sso.character_id };
        //                _dbContext.Eveonline_CharacterRoles.Add(character_roles);
        //                dbChanges += _dbContext.SaveChanges();
        //            }

        //            var updated = IUpdateCompareProperties.UpdateProperties(ref character_roles, ConnectorResult.item);
        //            if (updated)
        //            {
        //                _dbContext.Eveonline_CharacterRoles.Update(character_roles);
        //                dbChanges += _dbContext.SaveChanges();
        //            }
        //        }
        //    }

        //    _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterRoles, ConnectorResult.success ? 1 : 0, dbChanges);
        //}
    }
}
