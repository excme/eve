using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eveDirect.Services.EsiConnector.Jobs
{
    public class CharacterSkills : ConnectorJob
    {
        //static string l_reqName = "Character_Skills";
        //static string l_scope = Scope.Skills.ReadSkills.Name;
        //static ERequestFolder l_folder = ERequestFolder.Skills;
        //public CharacterSkills() : base(l_reqName, l_folder, l_scope) { }
        //public CharacterSkills(IGenericService genericService, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger, int maxCharactersToUpdate = 0, int charToUpdate = 0) : base(genericService, options, logger, l_reqName, l_folder, l_scope)
        //{
        //    _maxCharactersToUpdate = maxCharactersToUpdate;
        //    _itemToUpdate = charToUpdate;
        //}
        //public override void TaskJob(CharacterCorporationAuthSso sso)
        //{
        //    int dbChanges = 0;
        //    // Выкачивание
        //    var ConnectorResult = SsoOneItem<CharacterSkillsResult>(GetEsiConnectorAuth(sso.eveOnlineSsoData).Character.Skills.GetSkills, sso.character_id, folder, jobName);

        //    if (ConnectorResult.success)
        //    {
        //        using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //        {
        //            var character_skills = _dbContext.Eveonline_CharacterSkills.FirstOrDefault(x => x.character_id == sso.character_id);
        //            if (character_skills == null)
        //            {
        //                character_skills = new EveOnlineCharacterSkill() { character_id = sso.character_id };
        //                _dbContext.Eveonline_CharacterSkills.Add(character_skills);
        //                dbChanges += _dbContext.SaveChanges();
        //            }

        //            // Отчет
        //            var old_total_skills = character_skills.total_sp;
        //            var updated = IUpdateCompareProperties.UpdateProperties(ref character_skills, ConnectorResult.item);
        //            if (updated)
        //            {
        //                character_skills.onDateTime = DateTime.UtcNow;
        //                _dbContext.Eveonline_CharacterSkills.Update(character_skills);
        //            }

        //            // Добавление отчета по опыту
        //            if (old_total_skills != character_skills.total_sp)
        //                Character_UpdateSkillsReport(sso.character_id, ConnectorResult.item.total_sp, _dbContext);

        //            dbChanges += _dbContext.SaveChanges();
        //        }
        //    }

        //    AddSsoRequest(sso.character_id, ESsoRequestType.characterSkills, ConnectorResult.success ? 1 : 0, dbChanges);
        //}
        //void Character_UpdateSkillsReport(int character_id, int total_sp, EveContextDbContext _dbContext)
        //{
        //    int minTotalSkill = 5000000;
        //    int skillCanSell = total_sp - minTotalSkill;
        //    int sellUnit = 500000;

        //    if (skillCanSell >= sellUnit)
        //    {
        //        double smallExtractorPrice = _eveOnlineGeneric.Market_PriceByRegion(40519, DateTime.UtcNow, 0);
        //        double largeSkillInjectorPrice = _eveOnlineGeneric.Market_PriceByRegion(40520, DateTime.UtcNow, 0);

        //        double sellUnitPrice = largeSkillInjectorPrice - smallExtractorPrice;
        //        var canSellUnits = Convert.ToInt64(Math.Truncate(Convert.ToDouble(skillCanSell / sellUnit)));
        //        var characterPrice = (long)Math.Round(sellUnitPrice * canSellUnits);

        //        EveOnlineCharacterSkillReport skillReport = new EveOnlineCharacterSkillReport() { character_id = character_id, skillCost = characterPrice, OnDateTime = DateTime.UtcNow };
        //        _dbContext.Eveonline_CharacterSkillReports.Add(skillReport);
        //    }
        //}
    }
}
