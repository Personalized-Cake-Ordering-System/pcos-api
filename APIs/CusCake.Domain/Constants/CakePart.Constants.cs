using CusCake.Domain.Enums;

namespace CusCake.Domain.Constants;

public static class CakePartTypeConstants
{
    public static readonly Dictionary<CakePartTypeEnum, (bool IsRequired, string DisplayName)> CakePartTypes =
        new()
        {
            { CakePartTypeEnum.Size, (true, "Cake Size") },
            { CakePartTypeEnum.Sponge, (true, "Sponge Type") },
            { CakePartTypeEnum.Filling, (true, "Cake Filling") },
            { CakePartTypeEnum.Icing, (true, "Icing Type") },
            { CakePartTypeEnum.Goo, (true, "Goo Topping") },
            { CakePartTypeEnum.Extras, (false, "Extra Decorations") }
        };
    public static bool IsRequired(CakePartTypeEnum type)
    {
        return CakePartTypes[type].IsRequired;
    }

    public static string GetDisplayName(CakePartTypeEnum type)
    {
        return CakePartTypes[type].DisplayName;
    }
}
public static class CakeMessageTypeConstants
{
    public static readonly Dictionary<CakeMessageTypeEnum, double> CakeMessageTypes =
        new()
        {
            { CakeMessageTypeEnum.NONE, 0},
            { CakeMessageTypeEnum.TEXT, 20000},
            { CakeMessageTypeEnum.IMAGE, 20000 },
        };
    public static double GetPrice(CakeMessageTypeEnum type)
    {
        return CakeMessageTypes.TryGetValue(type, out double value) ? value : 0;
    }
}
