using System.ComponentModel;

namespace Ecommerce_2023.Shared.Enum
{
    public enum EImageType
    {
        [Description("USER")]
        USER,

        [Description("RESTAURANT")]
        RESTAURANT,

        [Description("PRODUCT")]
        PRODUCT
    }

    public static class EImageTypeExtensions
    {
        public static string GetDescription(this EImageType value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            return attributes != null && attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
