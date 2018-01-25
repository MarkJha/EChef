using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace eCafe.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string ToUserFriendlyString(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static string GetDetailedDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToUserFriendlyString());
            var descriptionAttribute = fieldInfo
                                       .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                       .FirstOrDefault() as DescriptionAttribute;
            return descriptionAttribute == null ? value.ToUserFriendlyString() : descriptionAttribute.Description;
        }



        public enum EnumGrades
        {
            [Description("Passed")]
            Pass,
            [Description("Failed")]
            Failed,
            [Description("Promoted")]
            Promoted
        }

    }
}
