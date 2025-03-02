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
}