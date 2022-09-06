using System;
using eveDirect.Repo.ReadWrite;
using eveDirect.Services.Jobs.Core;
using Microsoft.Extensions.Logging;

namespace eveDirect.Services.Jobs.Identity
{
    public class AccountRolesCharacterOwners : JobBase
    {
        IReadWrite _repoPublicCommon { get; set; }
        public AccountRolesCharacterOwners(IReadWrite repoPublicCommon, ILogger<AccountRolesCharacterOwners> logger) : base(logger)
        {
            _repoPublicCommon = repoPublicCommon
                ?? throw new ArgumentNullException(nameof(repoPublicCommon));
        }
        
        //public override void TaskJob()
        //{
        //    // Все sso с персонажами
        //    var sso_chars = _eveOnlineGeneric.Sso_GetActiveSsoCharacters();
        //    // Все юзеры
        //    List<EveControlAccount> all_users = _eveOnlineGeneric.UserManager_Users_All();
        //    foreach (var user in all_users)
        //    {
        //        var user_roles = _eveOnlineGeneric.UserManager_Roles_GetByUser(user);

        //        // Проверка на удаление ролей. Если у юзера пропало владение персонажем
        //        foreach (var role in user_roles.Where(x => x.character_id > 0).ToList())
        //        {
        //            // Проверка роли корпорации
        //            int character_id = role.character_id;

        //            // Если нет sso, с которой связан персонаж
        //            if (!sso_chars.Any(x => x.owner_account_Guid == user.Id && x.character_id == character_id))
        //            {
        //                _logger.LogInformation($"{user.UserName} утерял владение персонажем {character_id}.");
        //                _eveOnlineGeneric.UserManager_Role_RemoveUser(user, role.Name);
        //            }
        //        }

        //        // Проверка на добавление ролей. Если у юзера появилось владение персонажем
        //        // Роли юзера
        //        user_roles = _eveOnlineGeneric.UserManager_Roles_GetByUser(user);
        //        var by_character_ssoes = sso_chars.Where(xx => xx.owner_account_Guid == user.Id).GroupBy(x => x.character_id).ToList();
        //        foreach (var character in by_character_ssoes)
        //        {
        //            var role = _eveOnlineGeneric.UserManager_Role_GetByName(CharacterRoleList.OwnerRole(character.Key));
        //            if (!user_roles.Any(x => x.character_id == character.Key && x.character_owner)
        //                && role != null)
        //            {
        //                _logger.LogInformation($"{user.UserName} получил владение персонажем {character.Key}.");
        //                //await _userManager.AddToRoleAsync(user, CharacterRoleList.OwnerRole(character.Key));
        //                _eveOnlineGeneric.UserManager_Role_AddUser(user, role);
        //            }
        //        }
        //    }
        //}
    }
}
