using Microsoft.AspNetCore.Http;

namespace CusCake.Application.Validators;

public static class ValidationUtils
{
    public static bool BeAValidImage(IFormFile? file)
    {
        if (file == null) return true;

        const long maxSizeInBytes = 5 * 1024 * 1024;
        if (file.Length > maxSizeInBytes) return false;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName)?.ToLower();
        return allowedExtensions.Contains(extension);
    }

    public static bool BeUniqueId(List<Guid> ids)
    {
        return ids.Distinct().Count() == ids.Count;
    }
    public static bool BeValidEnumValue<TEnum>(int value) where TEnum : struct, Enum
    {
        return Enum.IsDefined(typeof(TEnum), value);
    }
}