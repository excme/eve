//using eveDirect.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eveDirect.Databases.Contexts.Public.Models
{
    public class AccountRole : IdentityRole<ulong>, IEveControlRoleCorporationProperties,
        //IUpdateProperties<IEveControlRoleCorporationProperties>, 
        IEveControlRoleCharacterProperties
        //, IUpdateProperties<IEveControlRoleCharacterProperties>
    {
        public override ulong Id { get; set; }
        /// <summary>
        /// Время экспирации роли
        /// </summary>
        public DateTime? expire { get; set; }

        /// <summary>
        /// Пользовательское название роли
        /// </summary>
        public string caption { get; set; }

        /// <summary>
        /// Можно ли пользователю удалить роль
        /// </summary>
        public bool can_remove { get; set; }

        public EIdentityRoleType type { get; set; }

        /// <summary>
        /// Руководитель корпорации
        /// </summary>
        public bool corporation_ceo { get; set; }
        public int corporation_id { get; set; }

        #region Corporation
        /// <summary>
        /// Группа. Имущество
        /// </summary>
        public bool corporation_assets { get; set; }
        /// <summary>
        /// Группа. Бухгалтерия
        /// </summary>
        public bool corporation_accounting { get; set; }
        /// <summary>
        /// Группа. Контакты
        /// </summary>
        public bool corporation_contacts { get; set; }
        /// <summary>
        /// Группа. Фракционные войны
        /// </summary>
        public bool corporation_fwars { get; set; }
        /// <summary>
        /// Группа. Приватная информация
        /// </summary>
        public bool corporation_privateInfo { get; set; }
        public bool corporation_manufacture { get; set; }
        /// <summary>
        /// Группа. Killmails
        /// </summary>
        public bool corporation_killmails { get; set; }
        /// <summary>
        /// Группа. Члены корпорации
        /// </summary>
        public bool corporation_members { get; set; }

        #endregion

        #region Corporation members

        /// <summary>
        /// Группа. Бухгалтерия
        /// </summary>
        public bool corpMembers_accounting { get; set; }
        /// <summary>
        /// Группа. Имущество
        /// </summary>
        public bool corpMembers_assets { get; set; }
        /// <summary>
        /// Группа. Календарь
        /// </summary>
        public bool corpMembers_calendar { get; set; }
        /// <summary>
        /// Группа. История
        /// </summary>
        public bool corpMembers_history { get; set; }
        /// <summary>
        /// Группа. Производство
        /// </summary>
        public bool corpMembers_manufacture { get; set; }
        /// <summary>
        /// Группа. Роли
        /// </summary>
        public bool corpMembers_roles { get; set; }
        /// <summary>
        /// Группа. Фракционные войны
        /// </summary>
        public bool corpMembers_fwars { get; set; }
        /// <summary>
        /// Группа. Контакты
        /// </summary>
        public bool corpMembers_contacts { get; set; }
        /// <summary>
        /// Группа. Настройки и статистика запросов sso персонажа
        /// </summary>
        public bool corpMembers_settings { get; set; }
        /// <summary>
        /// Группа. Флот
        /// </summary>
        public bool corpMembers_fleet { get; set; }
        /// <summary>
        /// Группа. Информация
        /// </summary>
        public bool corpMembers_info { get; set; }
        /// <summary>
        /// Группа. Killmails
        /// </summary>
        public bool corpMembers_killmails { get; set; }
        /// <summary>
        /// Группа. Почта
        /// </summary>
        public bool corpMembers_mail { get; set; }

        #endregion

        public int character_id { get; set; }
        /// <summary>
        /// Владелец персонажа
        /// </summary>
        public bool character_owner { get; set; }

        #region Character

        /// <summary>
        /// Группа. Бухгалтерия
        /// </summary>
        public bool character_accounting { get; set; }
        /// <summary>
        /// Группа. История
        /// </summary>
        public bool character_history { get; set; }
        /// <summary>
        /// Группа. История
        /// </summary>
        public bool character_fleet { get; set; }
        /// <summary>
        /// Группа. Имущество
        /// </summary>
        public bool character_assets { get; set; }
        /// <summary>
        /// Группа. Производство
        /// </summary>
        public bool character_manufacture { get; set; }
        /// <summary>
        /// Группа. Календарь
        /// </summary>
        public bool character_calendar { get; set; }
        /// <summary>
        /// Группа. Роли
        /// </summary>
        public bool character_roles { get; set; }
        /// <summary>
        /// Группа. Фракционные войны
        /// </summary>
        public bool character_fwars { get; set; }
        /// <summary>
        /// Группа. Контакты
        /// </summary>
        public bool character_contacts { get; set; }
        /// <summary>
        /// Группа. Информация
        /// </summary>
        public bool character_info { get; set; }
        public bool character_killmails { get; set; }
        public bool character_mail { get; set; }
        /// <summary>
        /// Настройки и статистика запросов
        /// </summary>
        public bool character_settings { get; set; }

        #endregion


        //public void GenerateNewRoleName(int ownder_id)
        //{
        //    switch (Type)
        //    {
        //        case EIdentityRoleType.Character:
        //            Name = string.Format(CharacterRoleList.templateRoleName, ownder_id, "custom-" + Caption.Replace("_", ""));
        //            break;
        //        case EIdentityRoleType.Corporation:
        //            Name = string.Format(CorporationRolesList.templateRoleName, ownder_id, "custom-" + Caption.Replace("_", ""));
        //            break;
        //        case EIdentityRoleType.Alliance:
        //            break;
        //        case EIdentityRoleType.User:
        //            break;
        //        default:
        //            break;
        //    }
        //}

        /// <summary>
        /// Установка настроек на true для руководителя корпорации
        /// </summary>
        public void AllCorporationPropertiesToTrue()
        {
            corporation_ceo = true;

            var interfaceProperties = typeof(IEveControlRoleCorporationProperties).GetProperties();
            foreach (var interfaceProperty in interfaceProperties)
            {
                if (interfaceProperty.PropertyType != typeof(bool)) continue;

                var propertyInfo = GetType().GetProperty(interfaceProperty.Name);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(this, Convert.ChangeType(true, propertyInfo.PropertyType));
                }
            }

        }

        public void AllCharacterPropertiesToTrue()
        {
            character_owner = true;
            var interfaceProperties = typeof(IEveControlRoleCharacterProperties).GetProperties();
            foreach (var interfaceProperty in interfaceProperties)
            {
                if (interfaceProperty.PropertyType != typeof(bool)) continue;

                var propertyInfo = GetType().GetProperty(interfaceProperty.Name);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(this, Convert.ChangeType(true, propertyInfo.PropertyType));
                }
            }
        }

        public void UpdateProperties(IEveControlRoleCorporationProperties connectionResult)
        {
            type = EIdentityRoleType.Corporation;

            corporation_assets = connectionResult.corporation_assets;
            corporation_accounting = connectionResult.corporation_accounting;
            corporation_contacts = connectionResult.corporation_contacts;
            corporation_privateInfo = connectionResult.corporation_privateInfo;
            corporation_manufacture = connectionResult.corporation_manufacture;
            corporation_fwars = connectionResult.corporation_fwars;
            corporation_killmails = connectionResult.corporation_killmails;
            corporation_members = connectionResult.corporation_members;

            corpMembers_assets = connectionResult.corpMembers_assets;
            corpMembers_accounting = connectionResult.corpMembers_accounting;
            corpMembers_calendar = connectionResult.corpMembers_calendar;
            corpMembers_history = connectionResult.corpMembers_history;
            corpMembers_roles = connectionResult.corpMembers_roles;
            corpMembers_manufacture = connectionResult.corpMembers_manufacture;
            corpMembers_mail = connectionResult.corpMembers_mail;
            corpMembers_killmails = connectionResult.corpMembers_killmails;
            corpMembers_fwars = connectionResult.corpMembers_fwars;
            corpMembers_fleet = connectionResult.corpMembers_fleet;
            corpMembers_info = connectionResult.corpMembers_info;
            corpMembers_contacts = connectionResult.corpMembers_contacts;
            corpMembers_settings = connectionResult.corpMembers_settings;
        }

        public void UpdateProperties(IEveControlRoleCharacterProperties connectionResult)
        {
            character_accounting = connectionResult.character_accounting;
            character_assets = connectionResult.character_assets;
            character_history = connectionResult.character_history;
            character_roles = connectionResult.character_roles;
            character_manufacture = connectionResult.character_manufacture;
            character_mail = connectionResult.character_mail;
            character_killmails = connectionResult.character_killmails;
            character_fwars = connectionResult.character_fwars;
            character_fleet = connectionResult.character_fleet;
            character_contacts = connectionResult.character_contacts;
            character_info = connectionResult.character_info;
            character_calendar = connectionResult.character_calendar;
        }
    }

    /// <summary>
    /// Свойства доступа корпораций
    /// </summary>
    public interface IEveControlRoleCorporationProperties
    {
        int corporation_id { get; set; }

        #region Corporation

        bool corporation_assets { get; set; }
        bool corporation_accounting { get; set; }
        bool corporation_contacts { get; set; }
        bool corporation_fwars { get; set; }
        bool corporation_killmails { get; set; }
        bool corporation_manufacture { get; set; }
        bool corporation_members { get; set; }
        bool corporation_privateInfo { get; set; }

        #endregion

        #region Corporation members

        bool corpMembers_assets { get; set; }
        bool corpMembers_accounting { get; set; }
        bool corpMembers_calendar { get; set; }
        bool corpMembers_history { get; set; }
        bool corpMembers_roles { get; set; }
        bool corpMembers_manufacture { get; set; }
        bool corpMembers_mail { get; set; }
        bool corpMembers_killmails { get; set; }
        bool corpMembers_fwars { get; set; }
        bool corpMembers_fleet { get; set; }
        bool corpMembers_info { get; set; }
        bool corpMembers_contacts { get; set; }
        bool corpMembers_settings { get; set; }

        #endregion
    }

    public interface IEveControlRoleCharacterProperties
    {
        int character_id { get; set; }

        #region Characters

        bool character_accounting { get; set; }
        bool character_assets { get; set; }
        bool character_history { get; set; }
        bool character_roles { get; set; }
        bool character_manufacture { get; set; }
        bool character_mail { get; set; }
        bool character_killmails { get; set; }
        bool character_fwars { get; set; }
        bool character_fleet { get; set; }
        bool character_contacts { get; set; }
        bool character_info { get; set; }
        bool character_calendar { get; set; }
        bool character_settings { get; set; }

        #endregion
    }

    public enum EIdentityRoleType : byte
    {
        Character = 1,
        Corporation = 2,
        Alliance = 3,
        User = 4
    }

    public class AccountRoleRef : IdentityUserRole<ulong>
    {
        public AccountRoleRef()
        {
            WhenAssign = DateTime.UtcNow;
        }

        public ulong id { get; set; }
        public DateTime assign { get; set; }
        [NotMapped]
        public DateTime WhenAssign
        {
            get
            {
                if (assign.Year < 2000)
                {
                    assign = DateTime.UtcNow;
                }

                return assign;
            }
            set { assign = value; }
        }

    }
}
