using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumItem)
        {
            if (enumItem?.GetType().GetField(enumItem?.ToString()) is FieldInfo fieldInfo &&
                fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true)
                    .Cast<DescriptionAttribute>().FirstOrDefault() is DescriptionAttribute descriptionAttribute &&
                !string.IsNullOrEmpty(descriptionAttribute.Description)
            )
                return descriptionAttribute.Description;

            return string.Empty;
        }
    }
}
