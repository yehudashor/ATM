using System.ComponentModel;

namespace TransactionDataModel.Models.Entities.Enums;

public enum BillType
{
    [Description("עשרים שקלים חדשים")]
    Twenty = 20,

    [Description("חמישים שקלים חדשים")]
    Fifty = 50,

    [Description("מאה שקלים חדשים")]
    OneHundred = 100,

    [Description("מאתיים שקלים חדשים")]
    TwoHundred = 200
}

//static IEnumerable<string> GetEnumDescriptions<TEnum>() where TEnum : struct, Enum
//{
//    var enumType = typeof(TEnum);

//    IEnumerable<TEnum> enumValues = Enum.GetValues(enumType).Cast<TEnum>();

//    IEnumerable<string> descriptions = from enumValue in enumValues
//                                       let fieldInfo = enumType.GetField(enumValue.ToString())
//                                       let attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute
//                                       select attribute?.Description ?? enumValue.ToString();

//    return descriptions;
//}
