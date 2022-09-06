using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace eveDirect.Shared.Helper
{
    public static class EnumGeneric
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
        }
        public static T ToEnum<T>(this string str)
            where T : Enum
        {
            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var attrs = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true));
                if (attrs.Any())
                {
                    var enumMemberAttribute = attrs.Single();
                    if (enumMemberAttribute.Value == str)
                        return (T)Enum.Parse(enumType, name);
                }

                continue;
            }
            
            return (T)Enum.Parse(enumType, str);
        }
    }
}
