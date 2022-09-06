using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterSkillsAttributes : ConnectorJob
    {
        //static string l_reqName = "Character_SkillsAttributes";
        //static string l_scope = Scope.Skills.ReadSkills.Name;
        //static ERequestFolder l_folder = ERequestFolder.Skills;
        //public CharacterSkillsAttributes() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterSkillsAttributes(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;

        //    // Выкачивание
        //    var ConnectorResult = SsoOneItem<CharacterAttributesResult>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Skills.GetAttributes, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var character_skillAttrs = _dbContext.Eveonline_CharacterAttributes.FirstOrDefault(x => x.character_id == sso.character_id);

        //            if (character_skillAttrs == null)
        //            {
        //                character_skillAttrs = new EveOnlineCharacterAttribute() { character_id = sso.character_id };
        //                _dbContext.Eveonline_CharacterAttributes.Add(character_skillAttrs);
        //                dbChanges += _dbContext.SaveChanges();
        //            }

        //            var updated = IUpdateCompareProperties.UpdateProperties(ref character_skillAttrs, ConnectorResult.item);
        //            if (updated)
        //            {
        //                _dbContext.Eveonline_CharacterAttributes.Update(character_skillAttrs);
        //                dbChanges += _dbContext.SaveChanges();
        //            }
        //        }
        //    }

        //    _eveOnlineGeneric.Sso_RequestStatistic(sso.character_id, ESsoRequestType.characterSkillsAttributes, ConnectorResult.success ?  1 : 0, dbChanges);
        //}
    }
}
