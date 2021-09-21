using System;
using System.ComponentModel;

namespace QuizDesigner.Common
{
    internal static class EnumUtil
    {
        public static string GetDescription<T>(this T enumValue)
            where T : struct, IConvertible
        {
            var description = enumValue.ToString();
            if (!typeof(T).IsEnum || string.IsNullOrEmpty(description))
            {
                return string.Empty;
            }

            var fieldInfo = enumValue.GetType().GetField(description);
            if (fieldInfo == null)
            {
                return description;
            }

            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs.Length > 0)
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }

            return description;
        }
    }
}