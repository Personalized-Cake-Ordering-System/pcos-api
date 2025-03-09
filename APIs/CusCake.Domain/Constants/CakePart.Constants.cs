using CusCake.Domain.Enums;

namespace CusCake.Domain.Constants;

public static class CakePartTypeConstants
{
    public static readonly Dictionary<CakePartTypeEnum, (bool IsRequired, string DisplayName)> CakePartTypes =
        new()
        {
            { CakePartTypeEnum.Size, (true, "Cake Size") },
            { CakePartTypeEnum.Sponge, (true, "Sponge Type") },
            { CakePartTypeEnum.Filling, (false, "Cake Filling") },
            { CakePartTypeEnum.Icing, (true, "Icing Type") },
            { CakePartTypeEnum.Goo, (false, "Goo Topping") },
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
