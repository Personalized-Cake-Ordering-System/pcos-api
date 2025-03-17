
namespace CusCake.Application.Utils;

public static class GenerateRandomString
{
    public static string GenerateCode()
    {
        string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        string random = new Random().Next(1000, 9999).ToString();
        return $"{timestamp}{random}";
    }
}