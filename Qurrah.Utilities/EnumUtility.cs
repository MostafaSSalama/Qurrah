using System.ComponentModel;
using System.Reflection;

namespace Qurrah.Utilities
{
    public static class EnumUtility
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes?.Any() == true)
                return attributes.First().Description;

            return value.ToString();
        }
    }
}