using eveDirect.Services.Jobs.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Services.Jobs.Identity
{
    /// <summary>
    /// По прошествии 2x месяцев рекруты переводятся в пилоты
    /// </summary>
    public class AccountCorporationRecruitToPilot : JobBase
    {
        public AccountCorporationRecruitToPilot():base(null)
        {

        }
        //static string l_reqName = "Account_ПереводРолиИзРекрутаВПилоты";
        //public AccountCorporationRecruitToPilot() : base(l_reqName, ERequestFolder.Other, "", false) { }
        //public AccountCorporationRecruitToPilot(IGenericService eveOnlineGeneric, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger) : base(eveOnlineGeneric, options, logger, l_reqName, ERequestFolder.Other, "", false)
        //{
        //    _eveOnlineGeneric = eveOnlineGeneric;
        //    _options = options;
        //}
        //public override void TaskJob()
        //{
        //    using (EveContextDbContext _dbContext = new EveContextDbContext(_options.Options))
        //    {
        //        var recruitTemplateSplit = CorporationRolesList.RecruitRoleTemplate.Split("_");
        //        // Запрос всех групп-рекрутов в корпорациях
        //        var allRecruitRoles = _dbContext.Roles
        //            //.Where(x => x.Name.Contains(recruitTemplateSplit[0]) && x.Name.Contains(recruitTemplateSplit[1]) && x.Name.Contains(recruitTemplateSplit[3]))
        //            .Where(x => x.corporation_id > 0 && x.Name.Contains(recruitTemplateSplit[3]))
        //            .Select(xx => new { xx.Id, xx.Name, xx.corporation_id }).ToList();

        //        foreach (var recruitRole in allRecruitRoles)
        //        {
        //            // Запрос все связей с игроками по этой роли
        //            var currectRoleRefs = _dbContext.UserRoles.Where(x => x.RoleId == recruitRole.Id).ToList();
        //            //currectRoleRefs?.ForEach(roleRef =>
        //            foreach (var roleRef in currectRoleRefs)
        //            {
        //                // Если роль рекрута больше 2х месяцев, то переводим в пилоты
        //                if (DateTime.UtcNow - roleRef.AssignTime > TimeSpan.FromDays(60))
        //                {
        //                    //int corporation_id = recruitRole.Name.Split("_")[2].ToInt32();
        //                    int corporation_id = recruitRole.corporation_id;
        //                    if (corporation_id > 0)
        //                    {
        //                        // Берем роль пилота в этой корпорации
        //                        string pilotRoleName = string.Format(CorporationRolesList.PilotsRoleTemplate, corporation_id);
        //                        var pilotRole = _dbContext.Roles.Select(x => new { x.Name, x.Id }).FirstOrDefault(x => x.Name == pilotRoleName);
        //                        if (pilotRole != null)
        //                        {
        //                            // Редактируем связь роли и юзера. Переводим в пилоты
        //                            EveControlAccountRoleRef newRef = new EveControlAccountRoleRef()
        //                            {
        //                                WhenAssign = DateTime.UtcNow,
        //                                RoleId = pilotRole.Id,
        //                                UserId = roleRef.UserId
        //                            };

        //                            _dbContext.UserRoles.Remove(roleRef);
        //                            _dbContext.UserRoles.Add(newRef);
        //                            _dbContext.SaveChanges();

        //                            _logger.LogInformation($"{jobName}. {roleRef.UserId} в корпорации {corporation_id} переведен на роль {pilotRole}");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
