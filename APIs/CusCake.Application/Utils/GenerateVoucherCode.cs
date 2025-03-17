
namespace CusCake.Application.Utils;

public static class GenerateVoucherCode
{
    private static readonly string LETTERS = "abcdefghijklmnopqrstuvwxyz"; // Chữ cái
    private static readonly string CHARS = "abcdefghijklmnopqrstuvwxyz0123456789";


    public static string GenerateCode()
    {
        Random random = new();
        var firstChar = CHARS[random.Next(LETTERS.Length)];
        var otherChars = new string([.. Enumerable.Repeat(CHARS, 8).Select(s => s[random.Next(s.Length)])]);

        return firstChar + otherChars;
    }
}