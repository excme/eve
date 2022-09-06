using eveDirect.Services.Jobs.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace eveDirect.Services.Jobs.Identity
{
    /// <summary>
    /// Метод проверяет:
    /// 1. Наличие персонажей в корпорации
    /// 2. Проверка статуса active у хотя бы одного персонажа в корпорации
    /// 3. Проверка владения аккаунтом на eve-online.com персонажа (ownderhash) - проверяться другим методом, который првоеряет sso. Если там есть нужные события, то он ставит sso статус не active. Значит сработает пункт 2
    /// </summary>
    public class AccountRolesInCorporation : JobBase
    {
        public AccountRolesInCorporation() : base(null)
        {

        }
        //static string l_reqName = "Account_ПроверкаРолейВКорпорациях";
        //public AccountRolesInCorporation() : base(l_reqName, ERequestFolder.Other, "", false) { }
        //public AccountRolesInCorporation(IGenericService eveOnlineGeneric, DbContextOptionsBuilder<EveContextDbContext> options, ILogger logger) : base(eveOnlineGeneric, options, logger, l_reqName, ERequestFolder.Other, "", false)
        //{
        //    _eveOnlineGeneric = eveOnlineGeneric;
        //    _options = options;
        //}
        //public override void TaskJob()
        //{
        //    // Все sso с персонажами
        //    var sso_chars = _eveOnlineGeneric.Sso_GetActiveSsoCharacters();
        //    // Все юзеры
        //    List<EveControlAccount> all_users = _eveOnlineGeneric.UserManager_Users_All();

        //    foreach (var user in all_users)
        //    {
        //        // Проверка на удаление ролей корпорации
        //        //var user_roles = await _userManager.GetRolesAsync(user);
        //        var user_roles = _eveOnlineGeneric.UserManager_Roles_GetByUser(user);
        //        foreach (var role in user_roles)
        //        {
        //            if (role.corporation_id > 0 && role.Type == EIdentityRoleType.Corporation)
        //            {
        //                int corporation_id = role.corporation_id;

        //                // Если корпораций нет, с которой связана роль корпорации
        //                if (!sso_chars.Any(sso => sso.character_corporation_id == corporation_id && sso.owner_account_Guid == user.Id))
        //                {
        //                    _logger.LogInformation($"Юзер {user.UserName} больше не имеет персонажей в корпорации {corporation_id}. Удаление роли {role}");
        //                    _eveOnlineGeneric.UserManager_Role_RemoveUser(user, role.Name);
        //                    // await _userManager.RemoveFromRoleAsync(user, role);
        //                }
        //            }
        //        }

        //        // Проверка на добавление роли "Рекрута"
        //        var by_corporation = sso_chars.Where(xx => xx.owner_account_Guid == user.Id).GroupBy(x => x.character_corporation_id).ToList();
        //        // Роли юзера
        //        //user_roles = (await _eveOnlineGeneric.Account_GetRolesNames(user.Id)).ToList();
        //        user_roles = _eveOnlineGeneric.UserManager_Roles_GetByUser(user);
        //        foreach (var corporation in by_corporation)
        //        {
        //            int count_characters_inCorp = corporation.Count();
        //            string recruiteRoleName = CorporationRolesList.RecruitRole(corporation.Key);
        //            var recruiteRole = _eveOnlineGeneric.UserManager_Role_GetByName(recruiteRoleName);
        //            if (count_characters_inCorp > 0
        //                && recruiteRole != null
        //                //&& !user_roles.Any(x => x.Contains($"role_corporation_{corporation.Key}"))
        //                && !user_roles.Any(x => x.Type == EIdentityRoleType.Corporation && x.corporation_id > corporation.Key)
        //                )
        //            {
        //                _logger.LogInformation($"Юзер {user.UserName} был принят в корпорацию {corporation.Key}. Добавление роли {recruiteRoleName}");

        //                _eveOnlineGeneric.UserManager_Role_AddUser(user, recruiteRole);
        //            }
        //        }
        //    }
        //}
    }
}
